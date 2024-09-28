using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines metadata for use with STK (Systems Tool Kit).
/// </summary>
public class AGI_stk_metadata : IGltfProperty
{
    /// <summary>
    /// A list of solar panel groups.
    /// </summary>
    public List<AgiStkSolarPanelGroup>? SolarPanelGroups { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class AgiStkSolarPanelGroup : IGltfProperty
{
    /// <summary>
    /// The name of this solar panel group.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// The percentage, from 0.0 to 100.0, of how efficiently the solar cells convert solar to electrical energy.
    /// </summary>
    public required float Efficiency { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object that associates a <see cref="Node"/> with the model's root <see cref="AGI_stk_metadata"/> object.
/// </summary>
public class AgiStkMetadataNode : IGltfProperty
{
    /// <summary>
    /// The name of a Solar Panel Group that includes this node.
    /// </summary>
    public string? SolarPanelGroupName { get; set; }
    /// <summary>
    /// Indicates if this node's geometry does not obscure any sensors' view in the STK Sensor Obscuration tool.
    /// </summary>
    public bool? NoObscuration { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class AgiStkMetadataExtension : IGltfExtension
{
    public string Name => nameof(AGI_stk_metadata);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<AgiStkSolarPanelGroup>? solarPanelGroups = null;
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
                if (propertyName == nameof(solarPanelGroups))
                {
                    solarPanelGroups = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AgiStkSolarPanelGroupSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<AGI_stk_metadata>(ref jsonReader, context);
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
            if (solarPanelGroups != null && solarPanelGroups.Count == 0)
            {
                throw new InvalidDataException("AGI_stk_metadata.solarPanelGroups must contain at least one solar panel group.");
            }
            return new AGI_stk_metadata()
            {
                SolarPanelGroups = solarPanelGroups,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Node))
        {
            string? solarPanelGroupName = null;
            bool? noObscuration = null;
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
                if (propertyName == nameof(solarPanelGroupName))
                {
                    solarPanelGroupName = ReadString(ref jsonReader);
                }
                else if (propertyName == nameof(noObscuration))
                {
                    noObscuration = ReadBoolean(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<AgiStkMetadataNode>(ref jsonReader, context);
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
            return new AgiStkMetadataNode()
            {
                SolarPanelGroupName = solarPanelGroupName,
                NoObscuration = noObscuration,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("AGI_stk_metadata must be used in either a Gltf root or a Node.");
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}

public class AgiStkSolarPanelGroupSerialization
{
    public static AgiStkSolarPanelGroup? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? name = null;
        float? efficiency = null;
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
            if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(efficiency))
            {
                efficiency = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AgiStkSolarPanelGroup>(ref jsonReader, context);
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
        if (name == null || efficiency == null)
        {
            throw new InvalidDataException("AGI_stk_metadata.solarPanelGroups[i].name and AGI_stk_metadata.solarPanelGroups[i].efficiency are required properties.");
        }
        return new()
        {
            Name = name,
            Efficiency = (float)efficiency,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
