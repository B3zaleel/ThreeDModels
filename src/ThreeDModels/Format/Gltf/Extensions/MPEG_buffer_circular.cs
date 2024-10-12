using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

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
    public Elements.JsonElement? Extras { get; set; }
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

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("MPEG_buffer_circular must be used in a MeshPrimitive.");
        }
        var mpegBufferCircular = (MPEG_buffer_circular?)element;
        if (mpegBufferCircular == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (mpegBufferCircular.Count != null && mpegBufferCircular.Count < 2)
        {
            throw new InvalidDataException("MPEG_buffer_circular.count must be greater than or equal to 2.");
        }
        if (mpegBufferCircular.Tracks != null && mpegBufferCircular.Tracks.Count < 1)
        {
            throw new InvalidDataException("MPEG_buffer_circular.tracks must have at least one element.");
        }
        jsonWriter.WriteStartObject();
        if (mpegBufferCircular.Count != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Count);
            jsonWriter.WriteNumberValue((int)mpegBufferCircular.Count);
        }
        jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_buffer_circular.Media);
        jsonWriter.WriteNumberValue(mpegBufferCircular.Media);
        if (mpegBufferCircular.Tracks != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_buffer_circular.Tracks);
            WriteIntegerList(ref jsonWriter, context, mpegBufferCircular.Tracks);
        }
        if (mpegBufferCircular.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MPEG_buffer_circular>(ref jsonWriter, context, mpegBufferCircular.Extensions);
        }
        if (mpegBufferCircular.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, mpegBufferCircular.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
