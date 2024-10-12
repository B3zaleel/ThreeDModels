using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the optical transmission of a material.
/// </summary>
public class KHR_materials_transmission : IGltfProperty
{
    /// <summary>
    /// The base percentage of non-specularly reflected light that is transmitted through the surface.
    /// </summary>
    public float TransmissionFactor { get; set; }
    /// <summary>
    /// A texture that defines the transmission percentage of the surface, sampled from the R channel. These values are linear, and will be multiplied by transmissionFactor.
    /// </summary>
    public TextureInfo? TransmissionTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsTransmissionExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_transmission);
    public const float Default_TransmissionFactor = 0.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_transmission must be used in a Material.");
        }
        float? transmissionFactor = null;
        TextureInfo? transmissionTexture = null;
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
            if (propertyName == nameof(transmissionFactor))
            {
                transmissionFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(transmissionTexture))
            {
                transmissionTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_transmission>(ref jsonReader, context);
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
        if (transmissionFactor != null && (transmissionFactor < 0.0f || transmissionFactor > 1.0f))
        {
            throw new InvalidDataException("KHR_materials_transmission.transmissionFactor must be between 0.0 and 1.0.");
        }
        return new KHR_materials_transmission()
        {
            TransmissionFactor = transmissionFactor ?? Default_TransmissionFactor,
            TransmissionTexture = transmissionTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_transmission must be used in a Material.");
        }
        var khrMaterialsTransmission = (KHR_materials_transmission?)element;
        if (khrMaterialsTransmission == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (khrMaterialsTransmission.TransmissionFactor < 0.0f || khrMaterialsTransmission.TransmissionFactor > 1.0f)
        {
            throw new InvalidDataException("KHR_materials_transmission.transmissionFactor must be between 0.0 and 1.0.");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_transmission.TransmissionFactor);
        jsonWriter.WriteNumberValue(khrMaterialsTransmission.TransmissionFactor);
        if (khrMaterialsTransmission.TransmissionTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_transmission.TransmissionTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsTransmission.TransmissionTexture);
        }
        if (khrMaterialsTransmission.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KHR_materials_transmission>(ref jsonWriter, context, khrMaterialsTransmission.Extensions);
        }
        if (khrMaterialsTransmission.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsTransmission.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
