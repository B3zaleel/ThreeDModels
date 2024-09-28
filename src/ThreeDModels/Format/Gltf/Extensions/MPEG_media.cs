using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class MPEG_media : IGltfRootProperty
{
    public required List<MpegMedia> Media { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object used to create a texture, audio source or other objects in the scene.
/// </summary>
public class MpegMedia : IGltfProperty
{
    /// <summary>
    /// The startTime gives the time at which the rendering of the timed media will be in seconds.
    /// </summary>
    public float StartTime { get; set; }
    /// <summary>
    /// The startTimeOffset indicates the time offset into the source, starting from which the timed media is generated.
    /// </summary>
    public float StartTimeOffset { get; set; }
    /// <summary>
    /// The endTimeOffset indicates the time offset into the source, up to which the timed media is generated.
    /// </summary>
    public float EndTimeOffset { get; set; }
    /// <summary>
    /// Specifies that the media start playing as soon as it is ready.
    /// </summary>
    public bool Autoplay { get; set; }
    /// <summary>
    /// Specifies that playback starts simultaneously for all media sources with the the same value and the autoplay flag has been set to true.
    /// </summary>
    public int? AutoplayGroup { get; set; }
    /// <summary>
    /// Specifies that the media start over again, every time it is finished.
    /// </summary>
    public bool Loop { get; set; }
    /// <summary>
    /// Specifies that media controls should be exposed to end user.
    /// </summary>
    public bool Controls { get; set; }
    /// <summary>
    /// A list of alternatives of the same media (e.g. different video code used).
    /// </summary>
    public required List<MpegMediaAlternative> Alternatives { get; set; }
    /// <summary>
    /// User-defined name of the media.
    /// </summary>
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegMediaAlternative : IGltfProperty
{
    /// <summary>
    /// The uri of the media.
    /// </summary>
    public required string Uri { get; set; }
    /// <summary>
    /// The media's MIME type.
    /// </summary>
    public required string MimeType { get; set; }
    /// <summary>
    /// A list of items that lists the components of the referenced media source that are to be used.
    /// </summary>
    public List<MpegMediaAlternativeTrack>? Tracks { get; set; }
    /// <summary>
    /// An object that may contain any additional media-specific parameters.
    /// </summary>
    public object? ExtraParams { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegMediaAlternativeTrack : IGltfProperty
{
    /// <summary>
    /// URL fragment to access the track within the media alternative.
    /// </summary>
    public required string Track { get; set; }
    /// <summary>
    /// A comma-separated list of codecs (as defined in IETF RFC 6381) of the media included in the track.
    /// </summary>
    public required string Codecs { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegMediaExtension : IGltfExtension
{
    public string Name => nameof(MPEG_media);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("MPEG_media must be used in a Gltf root.");
        }
        List<MpegMedia>? media = null;
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
            if (propertyName == nameof(media))
            {
                media = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegMediaSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_media>(ref jsonReader, context);
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
        if (media == null || media.Count == 0)
        {
            throw new InvalidDataException("MPEG_media.media must contain at least 1 item.");
        }
        return new MPEG_media()
        {
            Media = media!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}

public class MpegMediaSerialization
{
    public const float Default_StartTime = 0.0f;
    public const float Default_StartTimeOffset = 0.0f;
    public const float Default_EndTimeOffset = 0.0f;
    public const bool Default_Autoplay = true;
    public const bool Default_Loop = false;
    public const bool Default_Controls = false;

    public static MpegMedia? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float? startTime = null;
        float? startTimeOffset = null;
        float? endTimeOffset = null;
        bool? autoplay = null;
        int? autoplayGroup = null;
        bool? loop = null;
        bool? controls = null;
        List<MpegMediaAlternative>? alternatives = null;
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
            if (propertyName == nameof(startTime))
            {
                startTime = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(startTimeOffset))
            {
                startTimeOffset = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(endTimeOffset))
            {
                endTimeOffset = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(autoplay))
            {
                autoplay = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(autoplayGroup))
            {
                autoplayGroup = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(loop))
            {
                loop = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(controls))
            {
                controls = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(alternatives))
            {
                alternatives = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegMediaAlternativeSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegMedia>(ref jsonReader, context);
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
        if (alternatives == null || alternatives.Count == 0)
        {
            throw new InvalidDataException("MPEG_media.media[i].alternatives must have at least 1 item.");
        }
        if (startTime != null && autoplay != null)
        {
            throw new InvalidDataException("Only one of MPEG_media.media[i].startTime and MPEG_media.media[i].autoplay can be defined at any time.");
        }
        return new()
        {
            StartTime = startTime ?? Default_StartTime,
            StartTimeOffset = startTimeOffset ?? Default_StartTimeOffset,
            EndTimeOffset = endTimeOffset ?? Default_EndTimeOffset,
            Autoplay = autoplay ?? Default_Autoplay,
            AutoplayGroup = autoplayGroup,
            Loop = loop ?? Default_Loop,
            Controls = controls ?? Default_Controls,
            Alternatives = alternatives!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegMediaAlternativeSerialization
{
    public static MpegMediaAlternative? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        string? mimeType = null;
        List<MpegMediaAlternativeTrack>? tracks = null;
        object? extraParams = null;
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
            else if (propertyName == nameof(tracks))
            {
                tracks = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegMediaAlternativeTrackSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(extraParams))
            {
                extraParams = JsonSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegMediaAlternative>(ref jsonReader, context);
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
        if (uri == null || mimeType == null)
        {
            throw new InvalidDataException("MPEG_media.media[i].alternatives[j].uri and MPEG_media.media[i].alternatives[j].mimeType are required properties.");
        }
        return new()
        {
            Uri = uri,
            MimeType = mimeType,
            Tracks = tracks,
            ExtraParams = extraParams,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegMediaAlternativeTrackSerialization
{
    public static MpegMediaAlternativeTrack? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? track = null;
        string? codecs = null;
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
            if (propertyName == nameof(track))
            {
                track = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(codecs))
            {
                codecs = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegMediaAlternativeTrack>(ref jsonReader, context);
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
        if (track == null || codecs == null)
        {
            throw new InvalidDataException("MPEG_media.tracks[i].track and MPEG_media.tracks[i].codecs are required properties.");
        }
        return new()
        {
            Track = track,
            Codecs = codecs,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
