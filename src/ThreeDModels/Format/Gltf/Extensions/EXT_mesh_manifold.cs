using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines manifoldness for a mesh.
/// </summary>
public class EXT_mesh_manifold : IGltfProperty
{
    public required MeshPrimitive ManifoldPrimitive { get; set; }
    /// <summary>
    /// The index of the accessor that contains the vertex sparse indices for merging into a manifold.
    /// </summary>
    public int? MergeIndices { get; set; }
    /// <summary>
    /// The index of the accessor that contains the vertex sparse values for merging into a manifold.
    /// </summary>
    public int? MergeValues { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtMeshManifoldExtension : IGltfExtension
{
    public string Name => nameof(EXT_mesh_manifold);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Mesh))
        {
            throw new InvalidDataException("EXT_mesh_manifold must be used in a Mesh.");
        }
        MeshPrimitive? manifoldPrimitive = null;
        int? mergeIndices = null;
        int? mergeValues = null;
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
            if (propertyName == nameof(manifoldPrimitive))
            {
                manifoldPrimitive = MeshPrimitiveSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(mergeIndices))
            {
                mergeIndices = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(mergeValues))
            {
                mergeValues = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<EXT_mesh_manifold>(ref jsonReader, context);
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
        if (manifoldPrimitive == null)
        {
            throw new InvalidDataException("EXT_mesh_manifold.manifoldPrimitive is a required property.");
        }
        return new EXT_mesh_manifold()
        {
            ManifoldPrimitive = manifoldPrimitive,
            MergeIndices = mergeIndices,
            MergeValues = mergeValues,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
