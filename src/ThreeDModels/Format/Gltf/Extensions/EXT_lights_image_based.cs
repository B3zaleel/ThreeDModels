using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines instance attributes for a node with a mesh.
/// </summary>
public class EXT_lights_image_based : IGltfProperty
{
    public required List<ExtIightsImageBasedLight> Lights { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an image-based lighting environment.
/// </summary>
public class ExtIightsImageBasedLight : IGltfRootProperty
{
    /// <summary>
    /// Quaternion that represents the rotation of the IBL environment.
    /// </summary>
    public float[]? Rotation { get; set; }
    /// <summary>
    /// Brightness multiplier for environment.
    /// </summary>
    public float Intensity { get; set; }
    /// <summary>
    /// A 9x3 list that declares spherical harmonic coefficients for irradiance up to l=2.
    /// </summary>
    public required float[][] IrradianceCoefficients { get; set; }
    /// <summary>
    /// An N*6 list of the first N mips of the prefiltered cubemap.
    /// </summary>
    public required List<int[]> SpecularImages { get; set; }
    /// <summary>
    /// The dimension (in pixels) of the first specular mip.
    /// </summary>
    public required int SpecularImageSize { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtIightsImageBasedScene : IGltfProperty
{
    /// <summary>
    /// The id of the light referenced by this scene.
    /// </summary>
    public required int Light { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtIightsImageBasedExtension : IGltfExtension
{
    public string Name => nameof(EXT_lights_image_based);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<ExtIightsImageBasedLight>? lights = null;
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
                if (propertyName == nameof(lights))
                {
                    lights = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => ExtIightsImageBasedLightSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<EXT_lights_image_based>(ref jsonReader, context);
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
            if (lights == null || lights.Count == 0)
            {
                throw new InvalidDataException("EXT_lights_ies.lights must contain at least 1 light profile.");
            }
            return new EXT_lights_image_based()
            {
                Lights = lights,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Scene))
        {
            int? light = null;
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
                if (propertyName == nameof(light))
                {
                    light = ReadInteger(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<ExtIightsImageBasedScene>(ref jsonReader, context);
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
            if (light == null)
            {
                throw new InvalidDataException("scenes[i].EXT_lights_image_based.light is a required property.");
            }
            return new ExtIightsImageBasedScene()
            {
                Light = (int)light,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("EXT_lights_image_based must be used in either a Gltf root or a Scene.");
    }
}

public class ExtIightsImageBasedLightSerialization
{
    public static readonly float[] Default_Rotation = [0.0f, 0.0f, 0.0f, 1.0f];
    public const float Default_Intensity = 1.0f;

    public static ExtIightsImageBasedLight? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float[]? rotation = null;
        float? intensity = null;
        float[][]? irradianceCoefficients = null;
        List<int[]>? specularImages = null;
        int? specularImageSize = null;
        string? name = null;
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
            if (propertyName == nameof(rotation))
            {
                rotation = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(intensity))
            {
                intensity = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(irradianceCoefficients))
            {
                irradianceCoefficients = ReadListList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => ReadFloatList(ref reader, ctx)!.ToArray())?.ToArray();
            }
            else if (propertyName == nameof(specularImages))
            {
                specularImages = ReadListList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => ReadIntegerList(ref reader, ctx)!.ToArray());
            }
            else if (propertyName == nameof(specularImageSize))
            {
                specularImageSize = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<ExtIightsImageBasedLight>(ref jsonReader, context);
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
        if (irradianceCoefficients == null || specularImages == null || specularImageSize == null)
        {
            throw new InvalidDataException("EXT_lights_image_based.lights[i].irradianceCoefficients, EXT_lights_image_based.lights[i].specularImages, and EXT_lights_image_based.lights[i].specularImageSize are required properties.");
        }
        return new()
        {
            Rotation = rotation ?? Default_Rotation,
            Intensity = intensity ?? Default_Intensity,
            IrradianceCoefficients = irradianceCoefficients,
            SpecularImages = specularImages,
            SpecularImageSize = (int)specularImageSize,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
