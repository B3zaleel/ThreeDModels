using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class FB_geometry_metadata : IGltfProperty
{
    /// <summary>
    /// The number of distinct vertices recursively contained in this scene.
    /// </summary>
    public float? VertexCount { get; set; }
    /// <summary>
    /// The number of distinct primitives recursively contained in this scene.
    /// </summary>
    public float? PrimitiveCount { get; set; }
    /// <summary>
    /// The bounding box of this scene, in static geometry scene-space coordinates.
    /// </summary>
    public SceneBounds? SceneBounds { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents the minimum and maximum bounding box extent.
/// </summary>
public class SceneBounds : IGltfProperty
{
    /// <summary>
    /// The bounding box corner with the numerically lowest scene-space coordinates.
    /// </summary>
    public required float[] Min { get; set; }
    /// <summary>
    /// The bounding box corner with the numerically highest scene-space coordinates.
    /// </summary>
    public required float[]? Max { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class FbGeometryMetadataExtension : IGltfExtension
{
    public string Name => nameof(FB_geometry_metadata);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Scene))
        {
            throw new InvalidDataException("FB_geometry_metadata must be used in a Scene.");
        }
        float? vertexCount = null;
        float? primitiveCount = null;
        SceneBounds? sceneBounds = null;
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
            if (propertyName == nameof(vertexCount))
            {
                vertexCount = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(primitiveCount))
            {
                primitiveCount = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(sceneBounds))
            {
                sceneBounds = SceneBoundsSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<FB_geometry_metadata>(ref jsonReader, context);
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
        return new FB_geometry_metadata()
        {
            VertexCount = vertexCount,
            PrimitiveCount = primitiveCount,
            SceneBounds = sceneBounds,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Scene))
        {
            throw new InvalidDataException("FB_geometry_metadata must be used in a Scene.");
        }
        var fbGeometryMetadata = (FB_geometry_metadata?)element;
        if (fbGeometryMetadata == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (fbGeometryMetadata.VertexCount != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.FB_geometry_metadata.VertexCount);
            jsonWriter.WriteNumberValue((int)fbGeometryMetadata.VertexCount);
        }
        if (fbGeometryMetadata.PrimitiveCount != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.FB_geometry_metadata.PrimitiveCount);
            jsonWriter.WriteNumberValue((int)fbGeometryMetadata.PrimitiveCount);
        }
        if (fbGeometryMetadata.SceneBounds != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.FB_geometry_metadata.SceneBounds);
            SceneBoundsSerialization.Write(ref jsonWriter, context, fbGeometryMetadata.SceneBounds);
        }
        if (fbGeometryMetadata.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<FB_geometry_metadata>(ref jsonWriter, context, fbGeometryMetadata.Extensions);
        }
        if (fbGeometryMetadata.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, fbGeometryMetadata.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}

public class SceneBoundsSerialization
{
    public static SceneBounds? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float[]? min = null;
        float[]? max = null;
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
            if (propertyName == nameof(min))
            {
                min = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(max))
            {
                max = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparse>(ref jsonReader, context);
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
        if (min == null || max == null)
        {
            throw new InvalidDataException("SceneBounds.min and SceneBounds.max are required properties.");
        }
        if (min.Length != max.Length)
        {
            throw new InvalidDataException("SceneBounds.min and SceneBounds.max must have the same number of elements.");
        }
        if (min.Length != 3 || max.Length != 3)
        {
            throw new InvalidDataException("SceneBounds.min and SceneBounds.max must have exactly 3 elements.");
        }
        return new()
        {
            Min = min!,
            Max = max!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, SceneBounds? sceneBounds)
    {
        if (sceneBounds == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (sceneBounds.Min.Length != sceneBounds.Max.Length)
        {
            throw new InvalidDataException("SceneBounds.min and SceneBounds.max must have the same number of elements.");
        }
        if (sceneBounds.Min.Length != 3 || sceneBounds.Max.Length != 3)
        {
            throw new InvalidDataException("SceneBounds.min and SceneBounds.max must have exactly 3 elements.");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Accessor.Min);
        WriteFloatList(ref jsonWriter, context, sceneBounds.Min.ToList());
        jsonWriter.WritePropertyName(ElementName.Accessor.Max);
        WriteFloatList(ref jsonWriter, context, sceneBounds.Max.ToList());
        if (sceneBounds.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<SceneBounds>(ref jsonWriter, context, sceneBounds.Extensions);
        }
        if (sceneBounds.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, sceneBounds.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
