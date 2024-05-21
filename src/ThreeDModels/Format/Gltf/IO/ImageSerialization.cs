using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class ImageSerialization
{
    public static Image? Read(GltfReaderContext context)
    {
        string? uri = null;
        string? mimeType = null;
        int? bufferView = null;
        string? name = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.Uri)))
            {
                uri = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.MimeType)))
            {
                mimeType = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.BufferView)))
            {
                bufferView = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Image>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Image.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (bufferView != null && uri != null)
        {
            throw new InvalidDataException("Image.uri must not be defined if Image.bufferView has been defined.");
        }
        if (bufferView != null && mimeType == null)
        {
            throw new InvalidDataException("Image.mimeType must be defined if Image.bufferView has been defined.");
        }
        return new()
        {
            Uri = uri,
            MimeType = mimeType,
            BufferView = bufferView,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
