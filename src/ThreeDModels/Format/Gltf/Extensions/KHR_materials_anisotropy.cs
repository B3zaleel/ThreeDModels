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
    public object? Extras { get; set; }
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
}
