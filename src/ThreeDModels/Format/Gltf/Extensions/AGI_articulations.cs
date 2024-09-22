using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class AGI_articulations : IGltfProperty
{
    /// <summary>
    /// A list of articulations.
    /// </summary>
    public List<AgiArticulation>? Articulations { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object that indicates a named range of motion available to one or more nodes within the model.
/// </summary>
public class AgiArticulation : IGltfProperty
{
    /// <summary>
    /// The unique name of this articulation.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// An array of stages, each of which defines a degree of freedom of movement.
    /// </summary>
    public required List<AgiArticulationStage> Stages { get; set; }
    /// <summary>
    /// The local forward vector for the associated node, for the purpose of pointing at a target or other object.
    /// </summary>
    public float[]? PointingVector { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents a stage of a model articulation definition.
/// </summary>
public class AgiArticulationStage : IGltfProperty
{
    /// <summary>
    /// The name of this articulation stage.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// The type of motion applied by this articulation stage.
    /// </summary>
    public required string Type { get; set; }
    /// <summary>
    /// The minimum value for the range of motion of this articulation stage.
    /// </summary>
    public required float MinimumValue { get; set; }
    /// <summary>
    /// The maximum value for the range of motion of this articulation stage.
    /// </summary>
    public required float MaximumValue { get; set; }
    /// <summary>
    /// The initial value for this articulation stage.
    /// </summary>
    public required float InitialValue { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object that associates a <see cref="Node"/> with the model's root <see cref="AGI_articulations"/> object.
/// </summary>
public class AgiArticulationNode
{
    /// <summary>
    /// Indicates that this node's origin and orientation act as an attach point for external objects, analysis, or effects.
    /// </summary>
    public bool? IsAttachPoint { get; set; }
    /// <summary>
    /// The name of an Articulation that applies to this node.
    /// </summary>
    public string? ArticulationName { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class AgiArticulationsExtension : IGltfExtension
{
    public string Name => nameof(AGI_articulations);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<AgiArticulation>? articulations = null;
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
                if (propertyName == nameof(articulations))
                {
                    articulations = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AgiArticulationSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<AGI_articulations>(ref jsonReader, context);
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
            if (articulations != null && articulations.Count == 0)
            {
                throw new InvalidDataException("AGI_articulations.articulations must contain at least one articulation.");
            }
            return new AGI_articulations()
            {
                Articulations = articulations,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Node))
        {
            bool? isAttachPoint = null;
            string? articulationName = null;
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
                if (propertyName == nameof(isAttachPoint))
                {
                    isAttachPoint = ReadBoolean(ref jsonReader);
                }
                else if (propertyName == nameof(articulationName))
                {
                    articulationName = ReadString(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<AgiArticulationNode>(ref jsonReader, context);
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
            return new AgiArticulationNode()
            {
                IsAttachPoint = isAttachPoint,
                ArticulationName = articulationName,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("AGI_articulations must be used in either a Gltf root or a Node.");
    }
}

public class AgiArticulationSerialization
{
    public static AgiArticulation? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? name = null;
        List<AgiArticulationStage>? stages = null;
        float[]? pointingVector = null;
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
            else if (propertyName == nameof(stages))
            {
                stages = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AgiArticulationStageSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(pointingVector))
            {
                pointingVector = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AgiArticulation>(ref jsonReader, context);
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
        if (name == null || stages == null)
        {
            throw new InvalidDataException("AGI_articulations.articulations[i].name and AGI_articulations.articulations[i].stages are required properties.");
        }
        return new()
        {
            Name = name,
            Stages = stages,
            PointingVector = pointingVector,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class AgiArticulationStageSerialization
{
    public static AgiArticulationStage? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? name = null;
        string? type = null;
        float? minimumValue = null;
        float? maximumValue = null;
        float? initialValue = null;
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
            else if (propertyName == nameof(type))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(minimumValue))
            {
                minimumValue = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(maximumValue))
            {
                maximumValue = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(initialValue))
            {
                initialValue = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AgiArticulationNode>(ref jsonReader, context);
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
        if (name == null || type == null || minimumValue == null || maximumValue == null || initialValue == null)
        {
            throw new InvalidDataException("AGI_articulations.articulations[i].stages[j].name, AGI_articulations.articulations[i].stages[j].type, AGI_articulations.articulations[i].stages[j].minimumValue, AGI_articulations.articulations[i].stages[j].maximumValue, and AGI_articulations.articulations[i].stages[j].initialValue are required properties.");
        }
        return new()
        {
            Name = name,
            Type = type,
            MinimumValue = (float)minimumValue,
            MaximumValue = (float)maximumValue,
            InitialValue = (float)initialValue,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
