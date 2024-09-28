using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies spatial audio support.
/// </summary>
public class MPEG_audio_spatial : IGltfProperty
{
    /// <summary>
    /// A list of of audio sources.
    /// </summary>
    public List<MpegAudioSpatialSource>? Sources { get; set; }
    /// <summary>
    /// A listener object that places an audio listener node in the scene that should be attached to a parent camera node.
    /// </summary>
    public MpegAudioSpatialListener? Listener { get; set; }
    /// <summary>
    /// A list of reverb items.
    /// </summary>
    public List<MpegAudioSpatialReverb>? Reverbs { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAudioSpatialSource : IGltfProperty
{
    /// <summary>
    /// A unique identifier of the audio source in the scene.
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Indicates the type of the audio source.
    /// </summary>
    public required string Type { get; set; }
    /// <summary>
    /// Provides a level-adjustment in dB for the signal associated with the source.
    /// </summary>
    public float Pregain { get; set; }
    /// <summary>
    /// Playback speed of the audio signal.
    /// </summary>
    public float PlaybackSpeed { get; set; }
    /// <summary>
    /// Indicates the function used to calculate the attenuation of the audio signal based on the distance to the source.
    /// </summary>
    public string? Attenuation { get; set; }
    /// <summary>
    /// An array of parameters that are input to the attenuation function.
    /// </summary>
    public List<float>? AttenuationParameters { get; set; }
    /// <summary>
    /// Provides the distance in meters for which the distance gain is implicitly included in the source signal after application of pregain.
    /// </summary>
    public float ReferenceDistance { get; set; }
    /// <summary>
    /// A list of accessor references, by specifying the accessors indices in accessors array, that describe the buffers where the decoded audio will be made available.
    /// </summary>
    public required List<int> Accessors { get; set; }
    /// <summary>
    /// Provides one or more pointers to reverb units, optionally extended by a floating point scaling factor.
    /// </summary>
    public List<int>? ReverbFeed { get; set; }
    /// <summary>
    /// Provides a list of gain [dB] values to be applied to the source’s signal(s) when feeding it to the corresponding <see cref="reverbFeed"/>.
    /// </summary>
    public List<float>? ReverbFeedGain { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAudioSpatialListener : IGltfProperty
{
    /// <summary>
    /// A unique identifier of the audio listener in the scene.
    /// </summary>
    public required int Id { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAudioSpatialReverb : IGltfProperty
{
    /// <summary>
    /// A unique identifier of the audio reverb unit in the scene.
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Indicates if the reverb unit can be bypassed if the audio renderer does not support it.
    /// </summary>
    public bool Bypass { get; set; }
    /// <summary>
    /// List of items that contain a reverb property object describing reverb unit specific parameters.
    /// </summary>
    public required List<MpegAudioSpatialReverbProperty> Properties { get; set; }
    /// <summary>
    /// Delay in seconds from onset of source to onset of late reverberation for which DSR is provided.
    /// </summary>
    public float Predelay { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object that describes reverb unit specific parameters.
/// </summary>
public class MpegAudioSpatialReverbProperty : IGltfProperty
{
    /// <summary>
    /// Frequency for the provided RT60 and DSR values.
    /// </summary>
    public required float Frequency { get; set; }
    /// <summary>
    /// Specifies RT60 value in `second` for the frequency provided in the `frequency` field.
    /// </summary>
    public required float RT60 { get; set; }
    /// <summary>
    /// Specifies Diffuse-to-Source Ratio value in dB for the frequency provided in the `frequency` field.
    /// </summary>
    public required float DSR { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAudioSpatialExtension : IGltfExtension
{
    public string Name => nameof(MPEG_audio_spatial);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        List<MpegAudioSpatialSource>? sources = null;
        MpegAudioSpatialListener? listener = null;
        List<MpegAudioSpatialReverb>? reverbs = null;
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
            if (propertyName == nameof(sources))
            {
                sources = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegAudioSpatialSourceSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(listener))
            {
                listener = MpegAudioSpatialListenerSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(reverbs))
            {
                reverbs = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegAudioSpatialReverbSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_audio_spatial>(ref jsonReader, context);
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
        if (sources != null && sources.Count < 1)
        {
            throw new InvalidDataException("MPEG_audio_spatial.sources must contain at least one element.");
        }
        if (reverbs != null && reverbs.Count < 1)
        {
            throw new InvalidDataException("MPEG_audio_spatial.reverbs must contain at least one element.");
        }
        return new MPEG_audio_spatial()
        {
            Sources = sources,
            Listener = listener,
            Reverbs = reverbs,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}

public class MpegAudioSpatialSourceSerialization
{
    public const float Default_Pregain = 0.0f;
    public const float Default_PlaybackSpeed = 1.0f;
    public const float Default_ReferenceDistance = 1.0f;

    public static MpegAudioSpatialSource? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? id = null;
        string? type = null!;
        float? pregain = null;
        float? playbackSpeed = null;
        string? attenuation = null;
        List<float>? attenuationParameters = null;
        float? referenceDistance = null;
        List<int>? accessors = null!;
        List<int>? reverbFeed = null;
        List<float>? reverbFeedGain = null;
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
            if (propertyName == nameof(id))
            {
                id = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(type))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(pregain))
            {
                pregain = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(playbackSpeed))
            {
                playbackSpeed = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(attenuation))
            {
                attenuation = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(attenuationParameters))
            {
                attenuationParameters = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(referenceDistance))
            {
                referenceDistance = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(accessors))
            {
                accessors = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(reverbFeed))
            {
                reverbFeed = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(reverbFeedGain))
            {
                reverbFeedGain = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegAudioSpatialSource>(ref jsonReader, context);
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
        if (id == null || type == null || accessors == null)
        {
            throw new InvalidDataException("MPEG_audio_spatial.sources[n].id, MPEG_audio_spatial.sources[n].type, and MPEG_audio_spatial.sources[n].accessors are required properties.");
        }
        if (pregain != null && pregain < 0.0f)
        {
            throw new InvalidDataException("MPEG_audio_spatial.sources[n].pregain must be greater than or equal to 0.0.");
        }
        if (playbackSpeed != null && (playbackSpeed < 0.5 || playbackSpeed > 2.0))
        {
            throw new InvalidDataException("MPEG_audio_spatial.sources[n].playbackSpeed must be between 0.5 and 2.0.");
        }
        if (referenceDistance != null && referenceDistance < 1.0)
        {
            throw new InvalidDataException("MPEG_audio_spatial.sources[n].referenceDistance must be greater than or equal to 1.0.");
        }
        return new()
        {
            Id = (int)id,
            Type = type,
            Pregain = pregain ?? Default_Pregain,
            PlaybackSpeed = playbackSpeed ?? Default_PlaybackSpeed,
            Attenuation = attenuation,
            AttenuationParameters = attenuationParameters,
            ReferenceDistance = referenceDistance ?? Default_ReferenceDistance,
            Accessors = accessors,
            ReverbFeed = reverbFeed,
            ReverbFeedGain = reverbFeedGain,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegAudioSpatialListenerSerialization
{
    public static MpegAudioSpatialListener? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? id = null;
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
            if (propertyName == nameof(id))
            {
                id = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegAudioSpatialListener>(ref jsonReader, context);
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
        if (id == null)
        {
            throw new InvalidDataException("MPEG_audio_spatial.listener[n].id is a required property.");
        }
        return new()
        {
            Id = (int)id,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegAudioSpatialReverbSerialization
{
    public const bool Default_Bypass = true;
    public const float Default_Predelay = 1.0f;

    public static MpegAudioSpatialReverb? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? id = null;
        bool? bypass = null;
        List<MpegAudioSpatialReverbProperty>? properties = null;
        float? predelay = null;
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
            if (propertyName == nameof(id))
            {
                id = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(bypass))
            {
                bypass = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(properties))
            {
                properties = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MpegAudioSpatialReverbPropertySerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(predelay))
            {
                predelay = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegAudioSpatialReverb>(ref jsonReader, context);
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
        if (id == null || properties == null)
        {
            throw new InvalidDataException("MPEG_audio_spatial.reverbs[i].id and MPEG_audio_spatial.reverbs[i].properties are required properties.");
        }
        return new()
        {
            Id = (int)id,
            Bypass = bypass ?? Default_Bypass,
            Properties = properties!,
            Predelay = predelay ?? Default_Predelay,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class MpegAudioSpatialReverbPropertySerialization
{
    public static MpegAudioSpatialReverbProperty? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float? frequency = null;
        float? RT60 = null;
        float? DSR = null;
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
            if (propertyName == nameof(frequency))
            {
                frequency = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(RT60))
            {
                RT60 = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(DSR))
            {
                DSR = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MpegAudioSpatialReverbProperty>(ref jsonReader, context);
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
        if (frequency == null || RT60 == null || DSR == null)
        {
            throw new InvalidDataException("MPEG_audio_spatial.reverbs[i].property[j].frequency, MPEG_audio_spatial.reverbs[i].property[j].RT60, and MPEG_audio_spatial.reverbs[i].property[j].DSR are required properties.");
        }
        return new()
        {
            Frequency = (float)frequency,
            RT60 = (float)RT60,
            DSR = (float)DSR,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
