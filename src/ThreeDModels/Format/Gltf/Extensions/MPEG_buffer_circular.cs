using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class MPEG_buffer_circular : IGltfProperty
{
    /// <summary>
    /// The recommended number of sequential buffer frames to be offered by a circular buffer to the presentation engine.
    /// </summary>
    public int? Count { get; set; }
    /// <summary>
    /// Index of the media entry in the <see cref="MPEG_media"/> extension, which is used as the source for the input data to the buffer.
    /// </summary>
    public required int Media { get; set; }
    /// <summary>
    /// The array of indices of a track of a media entry, indicated by media and listed by <see cref="MPEG_media"/> extension, which is used as the source for the input data to this buffer.
    /// </summary>
    public List<int>? Tracks { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class MpegBufferCircularExtension : IGltfExtension
{
    public string Name => nameof(MPEG_buffer_circular);
    public const int Default_Count = 2;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("KHR_animation_pointer must be used in a MeshPrimitive.");
        }
        int? count = null;
        int? media = null;
        List<int>? tracks = null;
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
            if (propertyName == nameof(count))
            {
                count = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(media))
            {
                media = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(tracks))
            {
                tracks = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_buffer_circular>(ref jsonReader, context);
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
        if (count != null && count < 2)
        {
            throw new InvalidDataException("MPEG_buffer_circular.count must be greater than or equal to 2.");
        }
        if (tracks != null && tracks.Count < 1)
        {
            throw new InvalidDataException("MPEG_buffer_circular.tracks must have at least one element.");
        }
        if (media == null)
        {
            throw new InvalidDataException("MPEG_buffer_circular.media is a required property.");
        }
        return new MPEG_buffer_circular()
        {
            Count = count ?? Default_Count,
            Media = (int)media!,
            Tracks = tracks,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
