using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies the packing of normal, roughness and metallic in a single texture.
/// </summary>
public class MSFT_packing_normalRoughnessMetallic : IGltfProperty
{
    /// <summary>
    /// A texture with the packing Normal (RG), Roughness (B), Metallic (A).
    /// </summary>
    public MsftTextureIndex? NormalRoughnessMetallicTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class MsftPackingNormalRoughnessMetallicExtension : IGltfExtension
{
    public string Name => nameof(MSFT_packing_normalRoughnessMetallic);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("MSFT_packing_normalRoughnessMetallic must be used in a Gltf root.");
        }
        MsftTextureIndex? normalRoughnessMetallicTexture = null;
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
            if (propertyName == nameof(normalRoughnessMetallicTexture))
            {
                normalRoughnessMetallicTexture = MsftTextureIndexSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MSFT_packing_normalRoughnessMetallic>(ref jsonReader, context);
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
        return new MSFT_packing_normalRoughnessMetallic()
        {
            NormalRoughnessMetallicTexture = normalRoughnessMetallicTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
