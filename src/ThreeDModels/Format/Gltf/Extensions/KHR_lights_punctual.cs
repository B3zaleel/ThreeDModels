using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_lights_punctual : IGltfProperty
{
    public required List<KhrLightsPunctualLight> Lights { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents a directional, point, or spot light.
/// </summary>
public class KhrLightsPunctualLight : IGltfRootProperty
{
    /// <summary>
    /// Color of the light source.
    /// </summary>
    public required float[] Color { get; set; }
    /// <summary>
    /// Intensity of the light source.
    /// </summary>
    public float Intensity { get; set; }
    public KhrLightsPunctualLightSpot? Spot { get; set; }
    /// <summary>
    /// Specifies the light type.
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// A distance cutoff at which the light's intensity may be considered to have reached zero.
    /// </summary>
    public float? Range { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrLightsPunctualLightSpot : IGltfProperty
{
    /// <summary>
    /// Angle in radians from centre of spotlight where falloff begins.
    /// </summary>
    public float InnerConeAngle { get; set; }
    /// <summary>
    /// ngle in radians from centre of spotlight where falloff ends.
    /// </summary>
    public float OuterConeAngle { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrLightsPunctualNode : IGltfProperty
{
    /// <summary>
    /// The id of the light referenced by this node.
    /// </summary>
    public required int Light { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrLightsPunctualExtension : IGltfExtension
{
    public string Name => nameof(KHR_lights_punctual);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<KhrLightsPunctualLight>? lights = null;
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
                    lights = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext context) => KhrLightsPunctualLightSerialization.Read(ref reader, context)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<KHR_lights_punctual>(ref jsonReader, context);
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
            if (lights == null || lights.Count < 1)
            {
                throw new InvalidDataException("KHR_lights_punctual.lights must have at least one light.");
            }
            return new KHR_lights_punctual()
            {
                Lights = lights!,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Node))
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
                    extensions = ExtensionsSerialization.Read<KhrLightsPunctualNode>(ref jsonReader, context);
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
                throw new InvalidDataException("KHR_lights_punctual.light is a required property.");
            }
            return new KhrLightsPunctualNode()
            {
                Light = (int)light,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("KHR_lights_punctual must be used in a either a Node or the Gltf root.");
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType == typeof(Gltf))
        {
            var khrLightsPunctual = (KHR_lights_punctual?)element;
            if (khrLightsPunctual == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Lights);
            WriteList(ref jsonWriter, context, khrLightsPunctual.Lights, KhrLightsPunctualLightSerialization.Write);
            if (khrLightsPunctual.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<KHR_lights_punctual>(ref jsonWriter, context, khrLightsPunctual.Extensions);
            }
            if (khrLightsPunctual.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, khrLightsPunctual.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else if (parentType == typeof(Node))
        {
            var khrLightsPunctualNode = (KhrLightsPunctualNode?)element;
            if (khrLightsPunctualNode == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Light);
            jsonWriter.WriteNumberValue(khrLightsPunctualNode.Light);
            if (khrLightsPunctualNode.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<KhrLightsPunctualNode>(ref jsonWriter, context, khrLightsPunctualNode.Extensions);
            }
            if (khrLightsPunctualNode.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, khrLightsPunctualNode.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else
        {
            throw new InvalidDataException("KHR_lights_punctual must be used in a either a Node or the Gltf root.");
        }
    }
}

public class KhrLightsPunctualLightSerialization
{
    public static readonly float[] Default_Color = [1f, 1f, 1f];
    public static readonly float Default_Intensity = 1f;

    public static KhrLightsPunctualLight? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float[]? color = null;
        float? intensity = null;
        KhrLightsPunctualLightSpot? spot = null;
        string? type = null;
        float? range = null;
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
            if (propertyName == nameof(color))
            {
                color = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(intensity))
            {
                intensity = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(spot))
            {
                spot = KhrLightsPunctualLightSpotSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(type))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(range))
            {
                range = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KhrLightsPunctualLight>(ref jsonReader, context);
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
        if (color == null || color.Length != 3)
        {
            throw new InvalidDataException("KHR_lights_punctual.lights[i].color must have 3 numbers.");
        }
        if (range != null && range < 0.0f)
        {
            throw new InvalidDataException("KHR_lights_punctual.lights[i].range must be at least 0.");
        }
        return new()
        {
            Color = color ?? Default_Color,
            Intensity = intensity ?? Default_Intensity,
            Spot = spot,
            Type = type,
            Range = range,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, KhrLightsPunctualLight? khrLightsPunctualLight)
    {
        if (khrLightsPunctualLight == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Color);
        WriteFloatList(ref jsonWriter, context, khrLightsPunctualLight.Color.ToList());
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_lights_punctual.KhrLightsPunctualLight.Intensity);
        jsonWriter.WriteNumberValue(khrLightsPunctualLight.Intensity);
        if (khrLightsPunctualLight.Spot != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_lights_punctual.KhrLightsPunctualLight.Spot);
            KhrLightsPunctualLightSpotSerialization.Write(ref jsonWriter, context, khrLightsPunctualLight.Spot);
        }
        if (khrLightsPunctualLight.Type != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Type);
            jsonWriter.WriteStringValue(khrLightsPunctualLight.Type);
        }
        if (khrLightsPunctualLight.Range != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_lights_punctual.KhrLightsPunctualLight.Range);
            jsonWriter.WriteNumberValue((float)khrLightsPunctualLight.Range);
        }
        if (khrLightsPunctualLight.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(khrLightsPunctualLight.Name);
        }
        if (khrLightsPunctualLight.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KhrLightsPunctualLight>(ref jsonWriter, context, khrLightsPunctualLight.Extensions);
        }
        if (khrLightsPunctualLight.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrLightsPunctualLight.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}

public class KhrLightsPunctualLightSpotSerialization
{
    public const float Maximum_Angle = 1.5707963267948966f;
    public const float Default_InnerConeAngle = 0;
    public const float Default_OuterConeAngle = 0.7853981633974483f;

    public static KhrLightsPunctualLightSpot? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float? innerConeAngle = null;
        float? outerConeAngle = null;
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
            if (propertyName == nameof(innerConeAngle))
            {
                innerConeAngle = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(outerConeAngle))
            {
                outerConeAngle = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KhrLightsPunctualLightSpot>(ref jsonReader, context);
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
        if (innerConeAngle != null && (innerConeAngle < 0.0 || innerConeAngle > Maximum_Angle))
        {
            throw new InvalidDataException($"KHR_lights_punctual.lights[i].spot.innerConeAngle must be in the range [0, {Maximum_Angle}].");
        }
        if (outerConeAngle != null && (outerConeAngle < 0.0 || outerConeAngle > Maximum_Angle))
        {
            throw new InvalidDataException($"KHR_lights_punctual.lights[i].spot.outerConeAngle must be in the range [0, {Maximum_Angle}].");
        }
        return new()
        {
            InnerConeAngle = innerConeAngle ?? Default_InnerConeAngle,
            OuterConeAngle = outerConeAngle ?? Default_OuterConeAngle,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, KhrLightsPunctualLightSpot? khrLightsPunctualLightSpot)
    {
        if (khrLightsPunctualLightSpot == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (khrLightsPunctualLightSpot.InnerConeAngle < 0.0 || khrLightsPunctualLightSpot.InnerConeAngle > Maximum_Angle)
        {
            throw new InvalidDataException($"KHR_lights_punctual.lights[i].spot.innerConeAngle must be in the range [0, {Maximum_Angle}].");
        }
        if (khrLightsPunctualLightSpot.OuterConeAngle < 0.0 || khrLightsPunctualLightSpot.OuterConeAngle > Maximum_Angle)
        {
            throw new InvalidDataException($"KHR_lights_punctual.lights[i].spot.outerConeAngle must be in the range [0, {Maximum_Angle}].");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_lights_punctual.KhrLightsPunctualLightSpot.InnerConeAngle);
        jsonWriter.WriteNumberValue(khrLightsPunctualLightSpot.InnerConeAngle);
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_lights_punctual.KhrLightsPunctualLightSpot.OuterConeAngle);
        jsonWriter.WriteNumberValue(khrLightsPunctualLightSpot.OuterConeAngle);
        if (khrLightsPunctualLightSpot.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KhrLightsPunctualLightSpot>(ref jsonWriter, context, khrLightsPunctualLightSpot.Extensions);
        }
        if (khrLightsPunctualLightSpot.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrLightsPunctualLightSpot.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
