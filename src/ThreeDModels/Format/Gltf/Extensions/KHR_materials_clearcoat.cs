using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the clearcoat material layer.
/// </summary>
public class KHR_materials_clearcoat : IGltfProperty
{
    /// <summary>
    /// The clearcoat layer intensity (aka opacity) of the material.
    /// </summary>
    public float ClearcoatFactor { get; set; }
    /// <summary>
    /// The clearcoat layer intensity texture. These values are sampled from the R channel.
    /// </summary>
    public TextureInfo? ClearcoatTexture { get; set; }
    /// <summary>
    /// The clearcoat layer roughness of the material.
    /// </summary>
    public float ClearcoatRoughnessFactor { get; set; }
    /// <summary>
    /// The clearcoat layer roughness texture. These values are sampled from the G channel.
    /// </summary>
    public TextureInfo? ClearcoatRoughnessTexture { get; set; }
    /// <summary>
    /// A tangent space normal map for the clearcoat layer.
    /// </summary>
    public MaterialNormalTextureInfo? ClearcoatNormalTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsClearcoatExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_clearcoat);
    public const float Default_ClearcoatFactor = 0.0f;
    public const float Default_ClearcoatRoughnessFactor = 0.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_clearcoat must be used in a Material.");
        }
        float? clearcoatFactor = null;
        TextureInfo? clearcoatTexture = null;
        float? clearcoatRoughnessFactor = null;
        TextureInfo? clearcoatRoughnessTexture = null;
        MaterialNormalTextureInfo? clearcoatNormalTexture = null;
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
            if (propertyName == nameof(clearcoatFactor))
            {
                clearcoatFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(clearcoatTexture))
            {
                clearcoatTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(clearcoatRoughnessFactor))
            {
                clearcoatRoughnessFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(clearcoatRoughnessTexture))
            {
                clearcoatRoughnessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(clearcoatNormalTexture))
            {
                clearcoatNormalTexture = MaterialNormalTextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_clearcoat>(ref jsonReader, context);
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
        if (clearcoatFactor != null && (clearcoatFactor < 0.0f || clearcoatFactor > 1.0f))
        {
            throw new InvalidDataException("KHR_materials_clearcoat.clearcoatFactor must be in the range [0.0, 1.0].");
        }
        if (clearcoatRoughnessFactor != null && (clearcoatRoughnessFactor < 0.0f || clearcoatRoughnessFactor > 1.0f))
        {
            throw new InvalidDataException("KHR_materials_clearcoat.clearcoatRoughnessFactor must be in the range [0.0, 1.0].");
        }
        return new KHR_materials_clearcoat()
        {
            ClearcoatFactor = clearcoatFactor ?? Default_ClearcoatFactor,
            ClearcoatTexture = clearcoatTexture,
            ClearcoatRoughnessFactor = clearcoatRoughnessFactor ?? Default_ClearcoatRoughnessFactor,
            ClearcoatRoughnessTexture = clearcoatRoughnessTexture,
            ClearcoatNormalTexture = clearcoatNormalTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_clearcoat must be used in a Material.");
        }
        var khrMaterialsClearcoat = (KHR_materials_clearcoat?)element;
        if (khrMaterialsClearcoat == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (khrMaterialsClearcoat.ClearcoatFactor < 0.0f || khrMaterialsClearcoat.ClearcoatFactor > 1.0f)
        {
            throw new InvalidDataException("KHR_materials_clearcoat.clearcoatFactor must be in the range [0.0, 1.0].");
        }
        if (khrMaterialsClearcoat.ClearcoatRoughnessFactor < 0.0f || khrMaterialsClearcoat.ClearcoatRoughnessFactor > 1.0f)
        {
            throw new InvalidDataException("KHR_materials_clearcoat.clearcoatRoughnessFactor must be in the range [0.0, 1.0].");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_clearcoat.ClearcoatFactor);
        jsonWriter.WriteNumberValue(khrMaterialsClearcoat.ClearcoatFactor);
        if (khrMaterialsClearcoat.ClearcoatTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_clearcoat.ClearcoatTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsClearcoat.ClearcoatTexture);
        }
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_clearcoat.ClearcoatRoughnessFactor);
        jsonWriter.WriteNumberValue(khrMaterialsClearcoat.ClearcoatRoughnessFactor);
        if (khrMaterialsClearcoat.ClearcoatRoughnessTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_clearcoat.ClearcoatRoughnessTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsClearcoat.ClearcoatRoughnessTexture);
        }
        if (khrMaterialsClearcoat.ClearcoatNormalTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_clearcoat.ClearcoatNormalTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsClearcoat.ClearcoatNormalTexture);
        }
        if (khrMaterialsClearcoat.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KHR_materials_clearcoat>(ref jsonWriter, context, khrMaterialsClearcoat.Extensions);
        }
        if (khrMaterialsClearcoat.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsClearcoat.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
