using System.Text.Json;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

public class GltfWriter
{
    private readonly Dictionary<string, ExtensionElementWriter> _extensions = [];

    public void Execute(string path, Gltf gltf)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Write);
        Execute(fs, gltf);
    }

    public void Execute(Stream stream, Gltf gltf)
    {
        using var binaryWriter = new BinaryWriter(stream);
        var jsonWriter = new Utf8JsonWriter(stream);
        var context = new GltfWriterContext(_extensions);
        jsonWriter.WriteStartObject();

        if (gltf.ExtensionsUsed != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.ExtensionsUsed);
            WriteStringList(ref jsonWriter, context, gltf.ExtensionsUsed);
        }
        if (gltf.ExtensionsRequired != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.ExtensionsRequired);
            WriteStringList(ref jsonWriter, context, gltf.ExtensionsRequired);
        }
        if (gltf.Accessors != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Accessors);
            WriteList(ref jsonWriter, context, gltf.Accessors, AccessorSerialization.Write);
        }
        if (gltf.Animations != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Animations);
            WriteList(ref jsonWriter, context, gltf.Animations, AnimationSerialization.Write);
        }
        jsonWriter.WritePropertyName(ElementName.Gltf.Asset);
        AssetSerialization.Write(ref jsonWriter, context, gltf.Asset);
        if (gltf.Buffers != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Buffers);
            WriteList(ref jsonWriter, context, gltf.Buffers, BufferSerialization.Write);
        }
        if (gltf.BufferViews != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.BufferViews);
            WriteList(ref jsonWriter, context, gltf.BufferViews, BufferViewSerialization.Write);
        }
        if (gltf.Cameras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Cameras);
            WriteList(ref jsonWriter, context, gltf.Cameras, CameraSerialization.Write);
        }
        if (gltf.Images != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Images);
            WriteList(ref jsonWriter, context, gltf.Images, ImageSerialization.Write);
        }
        if (gltf.Materials != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Materials);
            WriteList(ref jsonWriter, context, gltf.Materials, MaterialSerialization.Write);
        }
        if (gltf.Meshes != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Meshes);
            WriteList(ref jsonWriter, context, gltf.Meshes, MeshSerialization.Write);
        }
        if (gltf.Nodes != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Nodes);
            WriteList(ref jsonWriter, context, gltf.Nodes, NodeSerialization.Write);
        }
        if (gltf.Samplers != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Samplers);
            WriteList(ref jsonWriter, context, gltf.Samplers, SamplerSerialization.Write);
        }
        jsonWriter.WritePropertyName(ElementName.Gltf.Scene);
        WriteInteger(ref jsonWriter, gltf.Scene);
        if (gltf.Scenes != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Scenes);
            WriteList(ref jsonWriter, context, gltf.Scenes, SceneSerialization.Write);
        }
        if (gltf.Skins != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Skins);
            WriteList(ref jsonWriter, context, gltf.Skins, SkinSerialization.Write);
        }
        if (gltf.Textures != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Textures);
            WriteList(ref jsonWriter, context, gltf.Textures, TextureSerialization.Write);
        }
        if (gltf.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Gltf>(ref jsonWriter, context, gltf.Extensions);
        }
        if (gltf.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, gltf.Extras);
        }
        jsonWriter.WriteEndObject();
        var jsonChunkSize = binaryWriter.BaseStream.Position;
        for (int i = 0; i < gltf.AdditionalChunks.Count; i++)
        {
            binaryWriter.Write(gltf.AdditionalChunks[i].Type);
            binaryWriter.Write(gltf.AdditionalChunks[i].Length);
            binaryWriter.Write(gltf.AdditionalChunks[i].Data);
        }
        binaryWriter.BaseStream.Seek(0, SeekOrigin.Begin);
        binaryWriter.Write(Constants.Magic);
        binaryWriter.Write(Constants.SupportedVersion);
        binaryWriter.Write((uint)jsonChunkSize);
    }

    public GltfWriter RegisterExtension(IGltfExtension extension)
    {
        if (!_extensions.ContainsKey(extension.Name))
        {
            _extensions.Add(extension.Name, extension.Write);
        }
        return this;
    }
}
