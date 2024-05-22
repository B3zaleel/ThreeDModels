using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

public class GltfReader
{
    private readonly Dictionary<string, ExtensionElementReader> _extensions = [];

    public Gltf? Execute(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return Execute(fs);
    }

    public Gltf? Execute(Stream stream)
    {
        using var binaryReader = new BinaryReader(stream);
        byte[] jsonChunk = [];
        List<Chunk>? additionalChunks = [];
        if (binaryReader.ReadUInt32() == Constants.Magic)
        {
            var version = binaryReader.ReadUInt32();
            var length = binaryReader.ReadUInt32();
            if (version > Constants.SupportedVersion)
            {
                throw new InvalidDataException("Unsupported version");
            }
            if (length != binaryReader.BaseStream.Length)
            {
                throw new InvalidDataException("Unexpected length");
            }
            var loadedJsonChunk = false;
            var loadedBinChunk = false;
            while (binaryReader.BaseStream.Position < binaryReader.BaseStream.Length)
            {
                var chunkType = binaryReader.ReadUInt32();
                var chunkLength = binaryReader.ReadUInt32();
                var chunkData = binaryReader.ReadBytes((int)chunkLength);

                if ((binaryReader.BaseStream.Position & 3) != 0)
                {
                    binaryReader.BaseStream.Seek(4 - (binaryReader.BaseStream.Position & 3), SeekOrigin.Current);
                }
                if (chunkType == Constants.Chunk_Type_JSON)
                {
                    if (loadedJsonChunk)
                    {
                        throw new InvalidDataException("Unexpected JSON chunk");
                    }
                    jsonChunk = chunkData;
                }
                else
                {
                    if (chunkType == Constants.Chunk_Type_BIN && loadedBinChunk)
                    {
                        throw new InvalidDataException("Unexpected BIN chunk");
                    }
                    additionalChunks.Add(new() { Type = chunkType, Length = chunkLength, Data = chunkData });
                }
            }
            if (!loadedJsonChunk)
            {
                throw new InvalidDataException("Failed to find JSON chunk");
            }
        }
        else
        {
            binaryReader.BaseStream.Seek(0, SeekOrigin.Begin);
            jsonChunk = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
        }
        var jsonReader = new Utf8JsonReader(jsonChunk);
        var context = new GltfReaderContext(_extensions);
        jsonReader.Read();
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException($"Failed to find {nameof(Gltf)} property");
        }
        List<string>? extensionsUsed = null;
        List<string>? extensionsRequired = null;
        List<Accessor>? accessors = null;
        List<Animation>? animations = null;
        Asset? asset = null;
        List<Elements.Buffer>? buffers = null;
        List<BufferView>? bufferViews = null;
        List<Camera>? cameras = null;
        List<Image>? images = null;
        List<Material>? materials = null;
        List<Mesh>? meshes = null;
        List<Node>? nodes = null;
        List<Sampler>? samplers = null;
        int? scene = null;
        List<Scene>? scenes = null;
        List<Skin>? skins = null;
        List<Texture>? textures = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = jsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.ExtensionsUsed)))
            {
                extensionsUsed = ReadStringList(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.ExtensionsRequired)))
            {
                extensionsRequired = ReadStringList(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Accessors)))
            {
                accessors = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AccessorSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Animations)))
            {
                animations = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AnimationSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Asset)))
            {
                asset = AssetSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Buffers)))
            {
                buffers = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => BufferSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.BufferViews)))
            {
                bufferViews = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => BufferViewSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Cameras)))
            {
                cameras = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => CameraSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Images)))
            {
                images = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => ImageSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Materials)))
            {
                materials = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MaterialSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Meshes)))
            {
                meshes = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MeshSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Nodes)))
            {
                nodes = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => NodeSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Samplers)))
            {
                samplers = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => SamplerSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Scene)))
            {
                scene = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Scenes)))
            {
                scenes = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => SceneSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Skins)))
            {
                skins = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => SkinSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Textures)))
            {
                textures = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => TextureSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Gltf>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (asset == null)
        {
            throw new InvalidDataException("Gltf.asset is a required property.");
        }
        return new()
        {
            ExtensionsUsed = extensionsUsed,
            ExtensionsRequired = extensionsRequired,
            Accessors = accessors,
            Animations = animations,
            Asset = asset!,
            Buffers = buffers,
            BufferViews = bufferViews,
            Cameras = cameras,
            Images = images,
            Materials = materials,
            Meshes = meshes,
            Nodes = nodes,
            Samplers = samplers,
            Scene = scene,
            Scenes = scenes,
            Skins = skins,
            Textures = textures,
            Extensions = extensions,
            Extras = extras,
            AdditionalChunks = additionalChunks
        };
    }

    public GltfReader RegisterExtension(IGltfExtension extension)
    {
        if (!_extensions.ContainsKey(extension.Name))
        {
            _extensions.Add(extension.Name, extension.Read);
        }
        return this;
    }
}
