using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines instance attributes for a node with a mesh.
/// </summary>
public class EXT_lights_ies : IGltfProperty
{
    public List<ExtLightsIesLightProfile>? Lights { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an IES light profile.
/// </summary>
public class ExtLightsIesLightProfile : IGltfRootProperty
{
    /// <summary>
    /// The URI (or IRI) of the light profile.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// The light profile's media type.
    /// </summary>
    public string? MimeType { get; set; }
    /// <summary>
    /// The index of the bufferView that contains the IES light profile.
    /// </summary>
    public int? BufferView { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtLightsIesNode : IGltfProperty
{
    /// <summary>
    /// The id of the light profile referenced by this node.
    /// </summary>
    public required int Light { get; set; }
    /// <summary>
    /// Non-negative factor to scale the light's intensity.
    /// </summary>
    public float Multiplier { get; set; }
    /// <summary>
    /// RGB value for the light's color in linear space.
    /// </summary>
    public float[]? Color { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtLightsIesExtension : IGltfExtension
{
    public string Name => nameof(EXT_lights_ies);
    public const float Default_Multiplier = 1.0f;
    public static readonly float[] Default_Color = [1.0f, 1.0f, 1.0f];

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<ExtLightsIesLightProfile>? lights = null;
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
                    lights = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => ExtLightsIesLightProfileSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<EXT_lights_ies>(ref jsonReader, context);
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
            return new EXT_lights_ies()
            {
                Lights = lights,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Node))
        {
            int? light = null;
            float? multiplier = null;
            float[]? color = null;
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
                else if (propertyName == nameof(multiplier))
                {
                    multiplier = ReadFloat(ref jsonReader);
                }
                else if (propertyName == nameof(color))
                {
                    color = ReadFloatList(ref jsonReader, context)?.ToArray();
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<ExtLightsIesNode>(ref jsonReader, context);
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
                throw new InvalidDataException("nodes[i].EXT_lights_ies.light is a required property.");
            }
            if (color != null && color.Length != 3)
            {
                throw new InvalidDataException("nodes[i].EXT_lights_ies.color must have 3 elements.");
            }
            return new ExtLightsIesNode()
            {
                Light = (int)light,
                Multiplier = multiplier ?? Default_Multiplier,
                Color = color ?? Default_Color,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("EXT_lights_ies must be used in either a Gltf root or a Node.");
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType == typeof(Gltf))
        {
            var extLightsIes = (EXT_lights_ies?)element;
            if (extLightsIes == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Lights);
            WriteList(ref jsonWriter, context, extLightsIes.Lights, ExtLightsIesLightProfileSerialization.Write);
            if (extLightsIes.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<EXT_lights_ies>(ref jsonWriter, context, extLightsIes.Extensions);
            }
            if (extLightsIes.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, extLightsIes.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else if (parentType == typeof(Node))
        {
            var extLightsIesNode = (ExtLightsIesNode?)element;
            if (extLightsIesNode == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Light);
            jsonWriter.WriteNumberValue(extLightsIesNode.Light);
            jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Multiplier);
            jsonWriter.WriteNumberValue(extLightsIesNode.Multiplier);
            if (extLightsIesNode.Color != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.EXT_lights_ies.Color);
                WriteFloatList(ref jsonWriter, context, extLightsIesNode.Color.ToList());
            }
            if (extLightsIesNode.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<ExtLightsIesNode>(ref jsonWriter, context, extLightsIesNode.Extensions);
            }
            if (extLightsIesNode.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, extLightsIesNode.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else
        {
            throw new InvalidDataException("EXT_lights_ies must be used in either a Gltf root or a Node.");
        }
    }
}

public class ExtLightsIesLightProfileSerialization
{
    public static ExtLightsIesLightProfile? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        string? mimeType = null;
        int? bufferView = null;
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
            if (propertyName == nameof(uri))
            {
                uri = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(mimeType))
            {
                mimeType = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<ExtLightsIesLightProfile>(ref jsonReader, context);
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
        if (bufferView != null && mimeType == null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].bufferView requires EXT_lights_ies.lights[i].mimeType.");
        }
        if (uri != null && bufferView != null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].uri cannot be defined when EXT_lights_ies.lights[i].bufferView is defined.");
        }
        if (uri == null && bufferView == null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].uri or EXT_lights_ies.lights[i].bufferView is a required property.");
        }
        return new()
        {
            Uri = uri,
            MimeType = mimeType,
            BufferView = bufferView,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, ExtLightsIesLightProfile? extLightsIesLightProfile)
    {
        if (extLightsIesLightProfile == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (extLightsIesLightProfile.BufferView != null && extLightsIesLightProfile.MimeType == null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].bufferView requires EXT_lights_ies.lights[i].mimeType.");
        }
        if (extLightsIesLightProfile.Uri != null && extLightsIesLightProfile.BufferView != null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].uri cannot be defined when EXT_lights_ies.lights[i].bufferView is defined.");
        }
        if (extLightsIesLightProfile.Uri == null && extLightsIesLightProfile.BufferView == null)
        {
            throw new InvalidDataException("EXT_lights_ies.lights[i].uri or EXT_lights_ies.lights[i].bufferView is a required property.");
        }
        jsonWriter.WriteStartObject();
        if (extLightsIesLightProfile.Uri != null)
        {
            jsonWriter.WritePropertyName(ElementName.Buffer.Uri);
            jsonWriter.WriteStringValue(extLightsIesLightProfile.Uri);
        }
        if (extLightsIesLightProfile.MimeType != null)
        {
            jsonWriter.WritePropertyName(ElementName.Image.MimeType);
            jsonWriter.WriteStringValue(extLightsIesLightProfile.MimeType);
        }
        if (extLightsIesLightProfile.BufferView != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.BufferView);
            jsonWriter.WriteNumberValue((int)extLightsIesLightProfile.BufferView);
        }
        if (extLightsIesLightProfile.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(extLightsIesLightProfile.Name);
        }
        if (extLightsIesLightProfile.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<ExtLightsIesLightProfile>(ref jsonWriter, context, extLightsIesLightProfile.Extensions);
        }
        if (extLightsIesLightProfile.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, extLightsIesLightProfile.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
