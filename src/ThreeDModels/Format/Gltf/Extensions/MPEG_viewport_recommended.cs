using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object for specifying an set of recommended viewports.
/// </summary>
public class MPEG_viewport_recommended : IGltfProperty
{
    public required List<MpegViewportRecommended> Viewports { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

/// <summary>
/// Represents an object for specifying a recommended viewport.
/// </summary>
public class MpegViewportRecommended
{
    /// <summary>
    /// Provides a reference to `accessor` where the timed data for the translation of camera object will be made available.
    /// </summary>
    public int? Translation { get; set; }
    /// <summary>
    /// Provides a reference to an `accessor` where the timed data for the rotation of camera object will be made available.
    /// </summary>
    public int? Rotation { get; set; }
    /// <summary>
    /// Provides the type of camera.
    /// </summary>
    public string? Type { get; set; }
    /// <summary>
    /// Provides a reference to `accessor` where the timed data for the perspective or orthographic camera parameters will be made available.
    /// </summary>
    public int? Parameters { get; set; }
    /// <summary>
    /// The user-defined name of this object.
    /// </summary>
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class MpegViewportRecommendedExtension : IGltfExtension
{
    public string Name => nameof(MPEG_viewport_recommended);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        List<MpegViewportRecommended>? viewports = null;
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
            if (propertyName == nameof(viewports))
            {
                viewports = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegViewportRecommendedSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_viewport_recommended>(ref jsonReader, context);
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
        if (viewports == null || viewports.Count < 1)
        {
            throw new InvalidDataException("MPEG_viewport_recommended.viewports must contain at least 1 element.");
        }
        return new MPEG_viewport_recommended()
        {
            Viewports = viewports!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegViewportRecommendedSerialization
{
    public const string Default_Type = "perspective";

    public static MpegViewportRecommended? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? translation = null;
        int? rotation = null;
        string? type = null;
        int? parameters = null;
        string? name = null;
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
            if (propertyName == nameof(translation))
            {
                translation = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(rotation))
            {
                rotation = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(type))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(parameters))
            {
                parameters = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegViewportRecommended>(ref jsonReader, context);
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
        return new()
        {
            Name = name,
            Translation = translation,
            Rotation = rotation,
            Type = type ?? Default_Type,
            Parameters = parameters,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
