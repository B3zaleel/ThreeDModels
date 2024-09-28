using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that adjusts the strength of emissive material properties.
/// </summary>
public class KHR_materials_emissive_strength : IGltfProperty
{
    /// <summary>
    /// The strength adjustment to be multiplied with the material's emissive value.
    /// </summary>
    public float EmissiveStrength { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsEmissiveStrengthExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_emissive_strength);
    public const float Default_EmissiveStrength = 1.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_emissive_strength must be used in a Material.");
        }
        float? emissiveStrength = null;
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
            if (propertyName == nameof(emissiveStrength))
            {
                emissiveStrength = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_emissive_strength>(ref jsonReader, context);
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
        if (emissiveStrength != null && emissiveStrength < 0)
        {
            throw new InvalidDataException("KHR_materials_emissive_strength.emissiveStrength must be greater than or equal to 0.");
        }
        return new KHR_materials_emissive_strength()
        {
            EmissiveStrength = emissiveStrength ?? Default_EmissiveStrength,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
