using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies a packing of occlusion, roughness, and metallic in a single texture and a two channel normal map.
/// </summary>
public class MSFT_packing_occlusionRoughnessMetallic : IGltfProperty
{
    /// <summary>
    /// A texture with packing Occlusion (R), Roughness (G), Metallic (B).
    /// </summary>
    public MsftTextureIndex? OcclusionRoughnessMetallicTexture { get; set; }
    /// <summary>
    /// A texture with packing Roughness (R), Metallic (G), Occlusion (B).
    /// </summary>
    public MsftTextureIndex? RoughnessMetallicOcclusionTexture { get; set; }
    /// <summary>
    /// A texture which contains two channel (RG) normal map.
    /// </summary>
    public MsftTextureIndex? NormalTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MsftTextureIndex
{
    /// <summary>
    /// The index of the texture.
    /// </summary>
    public int? Index { get; set; }
}

public class MsftPackingOcclusionRoughnessMetallicExtension : IGltfExtension
{
    public string Name => nameof(MSFT_packing_occlusionRoughnessMetallic);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("MSFT_packing_occlusionRoughnessMetallic must be used in a Gltf root.");
        }
        MsftTextureIndex? occlusionRoughnessMetallicTexture = null;
        MsftTextureIndex? roughnessMetallicOcclusionTexture = null;
        MsftTextureIndex? normalTexture = null;
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
            if (propertyName == nameof(occlusionRoughnessMetallicTexture))
            {
                occlusionRoughnessMetallicTexture = MsftTextureIndexSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(roughnessMetallicOcclusionTexture))
            {
                roughnessMetallicOcclusionTexture = MsftTextureIndexSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(normalTexture))
            {
                normalTexture = MsftTextureIndexSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MSFT_packing_occlusionRoughnessMetallic>(ref jsonReader, context);
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
        return new MSFT_packing_occlusionRoughnessMetallic()
        {
            OcclusionRoughnessMetallicTexture = occlusionRoughnessMetallicTexture,
            RoughnessMetallicOcclusionTexture = roughnessMetallicOcclusionTexture,
            NormalTexture = normalTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}

public class MsftTextureIndexSerialization
{
    public static MsftTextureIndex? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? index = null;
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
            if (propertyName == nameof(index))
            {
                index = ReadInteger(ref jsonReader);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new MsftTextureIndex()
        {
            Index = index,
        };
    }
}
