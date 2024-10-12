using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines instance attributes for a node with a mesh.
/// </summary>
public class EXT_mesh_gpu_instancing : IGltfProperty
{
    /// <summary>
    /// A dictionary object, where each key corresponds to instance attribute and each value is the index of the accessor containing attribute's data.
    /// </summary>
    public IntegerMap? Attributes { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtMeshGpuInstancingExtension : IGltfExtension
{
    public string Name => nameof(EXT_mesh_gpu_instancing);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("EXT_mesh_gpu_instancing must be used in a Gltf root.");
        }
        IntegerMap? attributes = null;
        Dictionary<string, object?>? extensions = null;
        Elements.JsonElement? extras = null;
        if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.Read())
        {
        }
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = jsonReader.GetString();
            if (propertyName == nameof(attributes))
            {
                attributes = IntegerMapSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<EXT_mesh_gpu_instancing>(ref jsonReader, context);
            }
            else if (propertyName == nameof(extras))
            {
                extras = JsonSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new EXT_mesh_gpu_instancing()
        {
            Attributes = attributes,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("EXT_mesh_gpu_instancing must be used in a Gltf root.");
        }
        var extMeshGpuInstancing = (EXT_mesh_gpu_instancing?)element;
        if (extMeshGpuInstancing == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (extMeshGpuInstancing.Attributes != null)
        {
            jsonWriter.WritePropertyName(ElementName.MeshPrimitive.Attributes);
            IntegerMapSerialization.Write(ref jsonWriter, context, extMeshGpuInstancing.Attributes);
        }
        if (extMeshGpuInstancing.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<EXT_mesh_gpu_instancing>(ref jsonWriter, context, extMeshGpuInstancing.Extensions);
        }
        if (extMeshGpuInstancing.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, extMeshGpuInstancing.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
