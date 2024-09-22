using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that exposes dynamic scene updates using the JSON patch protocol with MPEG media.
/// </summary>
public class MPEG_scene_dynamic : IGltfProperty
{
    /// <summary>
    /// The index of the media in <see cref="MPEG_media"/> that provides dynamic scene update information.
    /// </summary>
    public required int Media { get; set; }
    /// <summary>
    /// The index of a track of media in `MPEG_media` that provides dynamic scene update information.
    /// </summary>
    public int? Track { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegSceneDynamicExtension : IGltfExtension
{
    public string Name => nameof(MPEG_scene_dynamic);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        int? media = null;
        int? track = null;
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
                media = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(track))
            {
                track = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_scene_dynamic>(ref jsonReader, context);
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
        if (media == null)
        {
            throw new InvalidDataException("MPEG_scene_dynamic.media is a required property.");
        }
        return new MPEG_scene_dynamic()
        {
            Media = (int)media!,
            Track = track,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
