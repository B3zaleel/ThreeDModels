using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies timing information that allows to synchronize animation with media.
/// </summary>
public class MPEG_animation_timing : IGltfProperty
{
    /// <summary>
    /// Provides a reference to the accessor's index in the accessors array, that describes the buffer where the animation timing data will be made available.
    /// </summary>
    public required int Accessor { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAnimationTimingExtension : IGltfExtension
{
    public string Name => nameof(MPEG_animation_timing);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        int? accessor = null;
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
            if (propertyName == nameof(accessor))
            {
                accessor = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_animation_timing>(ref jsonReader, context);
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
        if (accessor == null)
        {
            throw new InvalidDataException("MPEG_animation_timing.accessor is a required property.");
        }
        return new MPEG_animation_timing()
        {
            Accessor = (int)accessor!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
