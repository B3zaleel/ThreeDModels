using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_draco_mesh_compression : IGltfProperty
{
    /// <summary>
    /// The index of the bufferView.
    /// </summary>
    public required int BufferView { get; set; }
    /// <summary>
    /// A dictionary object, where each key corresponds to an attribute and its unique attribute id stored in the compressed geometry.
    /// </summary>
    public required IntegerMap Attributes { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class KhrDracoMeshCompressionExtension : IGltfExtension
{
    public string Name => nameof(KHR_draco_mesh_compression);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("KHR_draco_mesh_compression must be used in a MeshPrimitive.");
        }
        int? bufferView = null;
        IntegerMap? attributes = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
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
            if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(attributes))
            {
                attributes = IntegerMapSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_draco_mesh_compression>(ref jsonReader, context);
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
        if (bufferView == null || attributes == null)
        {
            throw new InvalidDataException("KHR_draco_mesh_compression.bufferView and KHR_draco_mesh_compression.attributes are required properties.");
        }
        return new KHR_draco_mesh_compression()
        {
            BufferView = (int)bufferView!,
            Attributes = attributes!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
