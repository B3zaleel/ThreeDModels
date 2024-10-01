using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class ADOBE_materials_clearcoat_specular : IGltfProperty
{
    /// <summary>
    /// The clearcoat layer's index of refraction.
    /// </summary>
    public required float ClearcoatIor { get; set; }
    /// <summary>
    /// The clearcoat layer's specular intensity.
    /// </summary>
    public required float ClearcoatSpecularFactor { get; set; }
    /// <summary>
    /// The clearcoat layer specular intensity texture.
    /// </summary>
    public TextureInfo? ClearcoatSpecularTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class AdobeMaterialsClearcoatSpecularExtension : IGltfExtension
{
    public string Name => nameof(ADOBE_materials_clearcoat_specular);
    public static readonly float Default_ClearcoatIor = 1.5f;
    public static readonly float Default_ClearcoatSpecularFactor = 1.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("ADOBE_materials_clearcoat_specular must be used in a Gltf root.");
        }
        float? clearcoatIor = null;
        float? clearcoatSpecularFactor = null;
        TextureInfo? clearcoatSpecularTexture = null;
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
            if (propertyName == nameof(clearcoatIor))
            {
                clearcoatIor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(clearcoatSpecularFactor))
            {
                clearcoatSpecularFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(clearcoatSpecularTexture))
            {
                clearcoatSpecularTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<ADOBE_materials_clearcoat_tint>(ref jsonReader, context);
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
        return new ADOBE_materials_clearcoat_specular()
        {
            ClearcoatIor = clearcoatIor ?? Default_ClearcoatIor,
            ClearcoatSpecularFactor = clearcoatSpecularFactor ?? Default_ClearcoatSpecularFactor,
            ClearcoatSpecularTexture = clearcoatSpecularTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("ADOBE_materials_clearcoat_specular must be used in a Gltf root.");
        }
        var adobeMaterialsClearcoatSpecular = (ADOBE_materials_clearcoat_specular?)element;
        if (adobeMaterialsClearcoatSpecular == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.Extensions.ADOBE_materials_clearcoat_specular.ClearcoatIor, adobeMaterialsClearcoatSpecular.ClearcoatIor);
        jsonWriter.WriteNumber(ElementName.Extensions.ADOBE_materials_clearcoat_specular.ClearcoatSpecularFactor, adobeMaterialsClearcoatSpecular.ClearcoatSpecularFactor);
        if (adobeMaterialsClearcoatSpecular.ClearcoatSpecularTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.ADOBE_materials_clearcoat_specular.ClearcoatSpecularTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, adobeMaterialsClearcoatSpecular.ClearcoatSpecularTexture);
        }
        if (adobeMaterialsClearcoatSpecular.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<ADOBE_materials_clearcoat_specular>(ref jsonWriter, context, adobeMaterialsClearcoatSpecular.Extensions);
        }
        if (adobeMaterialsClearcoatSpecular.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, adobeMaterialsClearcoatSpecular.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
