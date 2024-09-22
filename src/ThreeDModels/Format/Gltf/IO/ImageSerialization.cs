using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class ImageSerialization
{
    public static Image? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        string? mimeType = null;
        int? bufferView = null;
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
            if (propertyName == nameof(uri))
            {
                uri = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(mimeType))
            {
                mimeType = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Image>(ref jsonReader, context);
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
