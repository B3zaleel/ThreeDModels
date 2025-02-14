using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the parameters for the volume of a material.
/// </summary>
public class KHR_materials_volume : IGltfProperty
{
    /// <summary>
    /// The thickness of the volume beneath the surface.
    /// </summary>
    public float ThicknessFactor { get; set; }
    /// <summary>
    /// Texture that defines the thickness of the volume, stored in the G channel.
    /// </summary>
    public TextureInfo? ThicknessTexture { get; set; }
    /// <summary>
    /// Average distance that light travels in the medium before interacting with a particle.
    /// </summary>
    public float AttenuationDistance { get; set; }
    /// <summary>
    /// Color that white light turns into due to absorption when reaching the attenuation distance.
    /// </summary>
    public required float[] AttenuationColor { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsVolumeExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_volume);
    public const float Default_ThicknessFactor = 0.0f;
    public const float Default_AttenuationDistance = 0.01f;
    public static readonly float[] Default_AttenuationColor = [1.0f, 1.0f, 1.0f];

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_volume must be used in a Material.");
        }
        float? thicknessFactor = null;
        TextureInfo? thicknessTexture = null;
        float? attenuationDistance = null;
        float[]? attenuationColor = null;
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
            if (propertyName == nameof(thicknessFactor))
            {
                thicknessFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(thicknessTexture))
            {
                thicknessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(attenuationDistance))
            {
                attenuationDistance = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(attenuationColor))
            {
                attenuationColor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_volume>(ref jsonReader, context);
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
        if (attenuationColor != null && attenuationColor.Length != 3)
        {
            throw new InvalidDataException("KHR_materials_volume.attenuationColor must have 3 elements.");
        }
        return new KHR_materials_volume()
        {
            ThicknessFactor = thicknessFactor ?? Default_ThicknessFactor,
            ThicknessTexture = thicknessTexture,
            AttenuationDistance = attenuationDistance ?? Default_AttenuationDistance,
            AttenuationColor = attenuationColor ?? Default_AttenuationColor,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_volume must be used in a Material.");
        }
        var khrMaterialsVolume = (KHR_materials_volume?)element;
        if (khrMaterialsVolume == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (khrMaterialsVolume.AttenuationColor.Length != 3)
        {
            throw new InvalidDataException("KHR_materials_volume.attenuationColor must have 3 elements.");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_volume.ThicknessFactor);
        jsonWriter.WriteNumberValue(khrMaterialsVolume.ThicknessFactor);
        if (khrMaterialsVolume.ThicknessTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_volume.ThicknessTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, khrMaterialsVolume.ThicknessTexture);
        }
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_volume.AttenuationDistance);
        jsonWriter.WriteNumberValue(khrMaterialsVolume.AttenuationDistance);
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_volume.AttenuationColor);
        WriteFloatList(ref jsonWriter, context, khrMaterialsVolume.AttenuationColor.ToList());
        if (khrMaterialsVolume.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KHR_materials_volume>(ref jsonWriter, context, khrMaterialsVolume.Extensions);
        }
        if (khrMaterialsVolume.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsVolume.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
