using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies textures using MPEG defined formats.
/// </summary>
public class MPEG_texture_video : IGltfProperty
{
    /// <summary>
    /// Provides the accessor's index in accessors array.
    /// </summary>
    public required int Accessor { get; set; }
    /// <summary>
    /// Provides the maximum width of the texture.
    /// </summary>
    public required float Width { get; set; }
    /// <summary>
    /// Provides the maximum height of the texture.
    /// </summary>
    public required float Height { get; set; }
    /// <summary>
    /// Indicates the format of the pixel data for this video texture.
    /// </summary>
    public string? Format { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegTextureVideoExtension : IGltfExtension
{
    public string Name => nameof(MPEG_texture_video);
    public const string Default_Format = "RGB";

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        int? accessor = null;
        float? width = null;
        float? height = null;
        string? format = null;
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
            else if (propertyName == nameof(width))
            {
                width = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(height))
            {
                height = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(format))
            {
                format = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_texture_video>(ref jsonReader, context);
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
        if (accessor == null || width == null || height == null)
        {
            throw new InvalidDataException("MPEG_texture_video.accessor, MPEG_texture_video.accessor, and MPEG_texture_video.accessor are required properties.");
        }
        return new MPEG_texture_video()
        {
            Accessor = (int)accessor!,
            Width = (float)width!,
            Height = (float)height!,
            Format = format ?? Default_Format,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        var mpegTextureVideo = (MPEG_texture_video?)element;
        if (mpegTextureVideo == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_animation_timing.Accessor);
        jsonWriter.WriteNumberValue(mpegTextureVideo.Accessor);
        jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_texture_video.Width);
        jsonWriter.WriteNumberValue(mpegTextureVideo.Width);
        jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_texture_video.Height);
        jsonWriter.WriteNumberValue(mpegTextureVideo.Height);
        if (mpegTextureVideo.Format != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.MPEG_texture_video.Format);
            jsonWriter.WriteStringValue(mpegTextureVideo.Format);
        }
        if (mpegTextureVideo.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MPEG_texture_video>(ref jsonWriter, context, mpegTextureVideo.Extensions);
        }
        if (mpegTextureVideo.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, mpegTextureVideo.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
