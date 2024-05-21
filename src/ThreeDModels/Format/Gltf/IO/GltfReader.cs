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
        var context = new GltfReaderContext(new Utf8JsonReader(jsonChunk), _extensions);
        context.JsonReader.Read();
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
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
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.ExtensionsUsed)))
            {
                extensionsUsed = ReadStringList(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.ExtensionsRequired)))
            {
                extensionsRequired = ReadStringList(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Accessors)))
            {
                accessors = ReadList(context, JsonTokenType.StartObject, reader => AccessorSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Animations)))
            {
                animations = ReadList(context, JsonTokenType.StartObject, reader => AnimationSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Asset)))
            {
                asset = AssetSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Buffers)))
            {
                buffers = ReadList(context, JsonTokenType.StartObject, reader => BufferSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.BufferViews)))
            {
                bufferViews = ReadList(context, JsonTokenType.StartObject, reader => BufferViewSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Cameras)))
            {
                cameras = ReadList(context, JsonTokenType.StartObject, reader => CameraSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Images)))
            {
                images = ReadList(context, JsonTokenType.StartObject, reader => ImageSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Materials)))
            {
                materials = ReadList(context, JsonTokenType.StartObject, reader => MaterialSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Meshes)))
            {
                meshes = ReadList(context, JsonTokenType.StartObject, reader => MeshSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Nodes)))
            {
                nodes = ReadList(context, JsonTokenType.StartObject, reader => NodeSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Samplers)))
            {
                samplers = ReadList(context, JsonTokenType.StartObject, reader => SamplerSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Scene)))
            {
                scene = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Scenes)))
            {
                scenes = ReadList(context, JsonTokenType.StartObject, reader => SceneSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Skins)))
            {
                skins = ReadList(context, JsonTokenType.StartObject, reader => SkinSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Gltf.Textures)))
            {
                textures = ReadList(context, JsonTokenType.StartObject, reader => TextureSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Gltf>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
