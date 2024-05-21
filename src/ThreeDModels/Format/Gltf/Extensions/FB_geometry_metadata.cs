using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

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
    public object? Extras { get; set; }
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
    public object? Extras { get; set; }
}

public class FbGeometryMetadataExtension : IGltfExtension
{
    public string Name => nameof(FB_geometry_metadata);

    public object? Read(GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Scene))
        {
            throw new InvalidDataException("FB_geometry_metadata must be used in a Scene.");
        }
        float? vertexCount = null;
        float? primitiveCount = null;
        SceneBounds? sceneBounds = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(FB_geometry_metadata.VertexCount)))
            {
                vertexCount = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(FB_geometry_metadata.PrimitiveCount)))
            {
                primitiveCount = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(FB_geometry_metadata.SceneBounds)))
            {
                sceneBounds = SceneBoundsSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(FB_geometry_metadata.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<FB_geometry_metadata>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(FB_geometry_metadata.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
}

public class SceneBoundsSerialization
{
    public static SceneBounds? Read(GltfReaderContext context)
    {
        float[]? min = null;
        float[]? max = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(SceneBounds.Min)))
            {
                min = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(SceneBounds.Max)))
            {
                max = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(SceneBounds.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparse>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(SceneBounds.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
}
