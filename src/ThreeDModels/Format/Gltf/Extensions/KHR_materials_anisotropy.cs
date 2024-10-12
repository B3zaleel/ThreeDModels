using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines anisotropy.
/// </summary>
public class KHR_materials_anisotropy : IGltfProperty
{
    /// <summary>
    /// The anisotropy strength. When anisotropyTexture is present, this value is multiplied by the blue channel.
    /// </summary>
    public float AnisotropyStrength { get; set; }
    /// <summary>
    /// The rotation of the anisotropy in tangent, bitangent space, measured in radians counter-clockwise from the tangent.
    /// </summary>
    public float AnisotropyRotation { get; set; }
    /// <summary>
    /// The anisotropy texture. Red and green channels represent the anisotropy direction in [-1, 1] tangent, bitangent space, to be rotated by anisotropyRotation.
    /// </summary>
    public TextureInfo? AnisotropyTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsAnisotropyExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_anisotropy);
    public const float Default_AnisotropyStrength = 0.0f;
    public const float Default_AnisotropyRotation = 0.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_anisotropy must be used in a Material.");
        }
        float? anisotropyStrength = null;
        float? anisotropyRotation = null;
        TextureInfo? anisotropyTexture = null;
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
            if (propertyName == nameof(anisotropyStrength))
            {
                anisotropyStrength = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(anisotropyRotation))
            {
                anisotropyRotation = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(anisotropyTexture))
            {
                anisotropyTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_anisotropy>(ref jsonReader, context);
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
        return new KHR_materials_anisotropy()
        {
            AnisotropyStrength = anisotropyStrength ?? Default_AnisotropyStrength,
            AnisotropyRotation = anisotropyRotation ?? Default_AnisotropyRotation,
            AnisotropyTexture = anisotropyTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_anisotropy must be used in a Material.");
        }
        var khrMaterialsAnisotropy = (KHR_materials_anisotropy?)element;
        if (khrMaterialsAnisotropy == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_anisotropy.AnisotropyStrength);
        jsonWriter.WriteNumberValue(khrMaterialsAnisotropy.AnisotropyStrength);
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_anisotropy.AnisotropyRotation);
        jsonWriter.WriteNumberValue(khrMaterialsAnisotropy.AnisotropyRotation);
        if (khrMaterialsAnisotropy.AnisotropyTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_anisotropy.AnisotropyTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsAnisotropy.AnisotropyTexture);
        }
        if (khrMaterialsAnisotropy.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KHR_materials_anisotropy>(ref jsonWriter, context, khrMaterialsAnisotropy.Extensions);
        }
        if (khrMaterialsAnisotropy.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsAnisotropy.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
