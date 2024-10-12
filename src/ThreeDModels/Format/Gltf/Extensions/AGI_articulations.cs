using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

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

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType == typeof(Gltf))
        {
            var agiArticulations = (AGI_articulations?)element;
            if (agiArticulations == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            if (agiArticulations.Articulations != null && agiArticulations.Articulations.Count == 0)
            {
                throw new InvalidDataException("AGI_articulations.articulations must contain at least one articulation.");
            }
            jsonWriter.WriteStartObject();
            if (agiArticulations.Articulations != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.Articulations);
                WriteList(ref jsonWriter, context, agiArticulations.Articulations, AgiArticulationSerialization.Write);
            }
            if (agiArticulations.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<AGI_articulations>(ref jsonWriter, context, agiArticulations.Extensions);
            }
            if (agiArticulations.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, agiArticulations.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else if (parentType == typeof(Node))
        {
            var agiArticulationNode = (AgiArticulationNode?)element;
            if (agiArticulationNode == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            if (agiArticulationNode.IsAttachPoint != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.IsAttachPoint);
                jsonWriter.WriteBooleanValue((bool)agiArticulationNode.IsAttachPoint);
            }
            if (agiArticulationNode.ArticulationName != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.ArticulationName);
                jsonWriter.WriteStringValue(agiArticulationNode.ArticulationName);
            }
            if (agiArticulationNode.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<AgiArticulationNode>(ref jsonWriter, context, agiArticulationNode.Extensions);
            }
            if (agiArticulationNode.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, agiArticulationNode.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else
        {
            throw new InvalidDataException("AGI_articulations must be used in either a Gltf root or a Node.");
        }
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AgiArticulation? agiArticulation)
    {
        if (agiArticulation == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Accessor.Name);
        jsonWriter.WriteStringValue(agiArticulation.Name);
        jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.AgiArticulation.Stages);
        WriteList(ref jsonWriter, context, agiArticulation.Stages, AgiArticulationStageSerialization.Write);
        if (agiArticulation.PointingVector != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.AgiArticulation.PointingVector);
            WriteFloatList(ref jsonWriter, context, agiArticulation.PointingVector.ToList());
        }
        if (agiArticulation.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AgiArticulation>(ref jsonWriter, context, agiArticulation.Extensions);
        }
        if (agiArticulation.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, agiArticulation.Extras);
        }
        jsonWriter.WriteEndObject();
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AgiArticulationStage? agiArticulationStage)
    {
        if (agiArticulationStage == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Accessor.Name);
        jsonWriter.WriteStringValue(agiArticulationStage.Name);
        jsonWriter.WritePropertyName(ElementName.Accessor.Type);
        jsonWriter.WriteStringValue(agiArticulationStage.Type);
        jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.AgiArticulationStage.MinimumValue);
        jsonWriter.WriteNumberValue(agiArticulationStage.MinimumValue);
        jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.AgiArticulationStage.MaximumValue);
        jsonWriter.WriteNumberValue(agiArticulationStage.MaximumValue);
        jsonWriter.WritePropertyName(ElementName.Extensions.AGI_articulations.AgiArticulationStage.InitialValue);
        jsonWriter.WriteNumberValue(agiArticulationStage.InitialValue);
        if (agiArticulationStage.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AgiArticulationStage>(ref jsonWriter, context, agiArticulationStage.Extensions);
        }
        if (agiArticulationStage.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, agiArticulationStage.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
