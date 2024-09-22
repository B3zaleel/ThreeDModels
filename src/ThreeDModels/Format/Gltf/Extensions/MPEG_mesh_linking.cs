using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies a logical link between two meshes.
/// </summary>
public class MPEG_mesh_linking : IGltfProperty
{
    /// <summary>
    /// A reference to the accessor's index that describes the buffer where the correspondence values between the dependent mesh and its associated shadow mesh will be made available.
    /// </summary>
    public required int Correspondence { get; set; }
    /// <summary>
    /// A reference to the shadow mesh's index associated to the dependent mesh for which the correspondence values are established.
    /// </summary>
    public required int Mesh { get; set; }
    /// <summary>
    /// A reference to the accessor's index that describe the buffer where the transformation of the nodes associated to the dependent mesh will be made available.
    /// </summary>
    public required int Pose { get; set; }
    /// <summary>
    /// A reference to the accessor's index that describe the buffer where the “weights” to be applied to the morph targets of the shadow mesh associated to the dependent mesh will be made available.
    /// </summary>
    public int? Weights { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegMeshLinkingExtension : IGltfExtension
{
    public string Name => nameof(MPEG_mesh_linking);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        int? correspondence = null;
        int? mesh = null;
        int? pose = null;
        int? weights = null;
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
            if (propertyName == nameof(correspondence))
            {
                correspondence = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(mesh))
            {
                mesh = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(pose))
            {
                pose = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(weights))
            {
                weights = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_mesh_linking>(ref jsonReader, context);
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
        if (correspondence == null || mesh == null || pose == null)
        {
            throw new InvalidDataException("MPEG_mesh_linking.correspondence, MPEG_mesh_linking.mesh, and MPEG_mesh_linking.pose are required properties.");
        }
        return new MPEG_mesh_linking()
        {
            Correspondence = (int)correspondence!,
            Mesh = (int)mesh!,
            Pose = (int)pose!,
            Weights = weights,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
