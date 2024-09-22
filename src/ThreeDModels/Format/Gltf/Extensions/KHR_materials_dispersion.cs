using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the strength of dispersion.
/// </summary>
public class KHR_materials_dispersion : IGltfProperty
{
    /// <summary>
    /// A value that defines dispersion in terms of the 20/Abbe number formulation.
    /// </summary>
    public float Dispersion { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsDispersionExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_dispersion);
    public const float Default_Dispersion = 0.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_dispersion must be used in a Material.");
        }
        float? dispersion = null;
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
            if (propertyName == nameof(dispersion))
            {
                dispersion = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_dispersion>(ref jsonReader, context);
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
        if (dispersion != null && dispersion < 0.0f)
        {
            throw new InvalidDataException("KHR_materials_dispersion.dispersion must be greater than or equal to 0.0.");
        }
        return new KHR_materials_dispersion()
        {
            Dispersion = dispersion ?? Default_Dispersion,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
