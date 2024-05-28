using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines an iridescence effect.
/// </summary>
public class KHR_materials_iridescence : IGltfProperty
{
    /// <summary>
    /// The iridescence intensity factor.
    /// </summary>
    public float IridescenceFactor { get; set; }
    /// <summary>
    /// The iridescence intensity texture. The values are sampled from the R channel. These values are linear.
    /// </summary>
    public TextureInfo? IridescenceTexture { get; set; }
    /// <summary>
    /// The index of refraction of the dielectric thin-film layer.
    /// </summary>
    public float IridescenceIor { get; set; }
    /// <summary>
    /// The minimum thickness of the thin-film layer given in nanometers.
    /// </summary>
    public float IridescenceThicknessMinimum { get; set; }
    /// <summary>
    /// The maximum thickness of the thin-film layer given in nanometers.
    /// </summary>
    public float IridescenceThicknessMaximum { get; set; }
    /// <summary>
    /// The thickness texture of the thin-film layer to linearly interpolate between the minimum and 
    /// maximum thickness given by the corresponding properties, where a sampled value of `0.0` represents 
    /// the minimum thickness and a sampled value of `1.0` represents the maximum thickness.
    /// </summary>
    public TextureInfo? IridescenceThicknessTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class KhrMaterialsIridescenceExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_iridescence);
    public const float Default_IridescenceFactor = 0.0f;
    public const float Default_IridescenceIor = 1.3f;
    public const float Default_IridescenceThicknessMinimum = 100.0f;
    public const float Default_IridescenceThicknessMaximum = 400.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_iridescence must be used in a Material.");
        }
        float? iridescenceFactor = null;
        TextureInfo? iridescenceTexture = null;
        float? iridescenceIor = null;
        float? iridescenceThicknessMinimum = null;
        float? iridescenceThicknessMaximum = null;
        TextureInfo? iridescenceThicknessTexture = null;
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
            if (propertyName == nameof(iridescenceFactor))
            {
                iridescenceFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(iridescenceTexture))
            {
                iridescenceTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(iridescenceIor))
            {
                iridescenceIor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(iridescenceThicknessMinimum))
            {
                iridescenceThicknessMinimum = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(iridescenceThicknessMaximum))
            {
                iridescenceThicknessMaximum = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(iridescenceThicknessTexture))
            {
                iridescenceThicknessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_iridescence>(ref jsonReader, context);
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
        if (iridescenceFactor != null && (iridescenceFactor < 0.0f || iridescenceFactor > 1.0f))
        {
            throw new InvalidDataException("KHR_materials_iridescence.iridescenceFactor must be in the range [0.0, 1.0].");
        }
        if (iridescenceIor != null && iridescenceIor < 1.0f)
        {
            throw new InvalidDataException("KHR_materials_iridescence.iridescenceIor must be greater than or equal to 1.0.");
        }
        if (iridescenceThicknessMinimum != null && iridescenceThicknessMinimum < 0.0f)
        {
            throw new InvalidDataException("KHR_materials_iridescence.iridescenceThicknessMinimum must be greater than or equal to 0.0.");
        }
        if (iridescenceThicknessMaximum != null && iridescenceThicknessMaximum < 0.0f)
        {
            throw new InvalidDataException("KHR_materials_iridescence.iridescenceThicknessMaximum must be greater than or equal to 0.0.");
        }

        return new KHR_materials_iridescence()
        {
            IridescenceFactor = iridescenceFactor ?? Default_IridescenceFactor,
            IridescenceTexture = iridescenceTexture,
            IridescenceIor = iridescenceIor ?? Default_IridescenceIor,
            IridescenceThicknessMinimum = iridescenceThicknessMinimum ?? Default_IridescenceThicknessMinimum,
            IridescenceThicknessMaximum = iridescenceThicknessMaximum ?? Default_IridescenceThicknessMaximum,
            IridescenceThicknessTexture = iridescenceThicknessTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
