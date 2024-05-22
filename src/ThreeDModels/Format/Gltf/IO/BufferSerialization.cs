using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class BufferSerialization
{
    public static Elements.Buffer? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        int? byteLength = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Uri)))
            {
                uri = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.ByteLength)))
            {
                byteLength = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Name)))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Elements.Buffer>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (byteLength == null)
        {
            throw new InvalidDataException("Buffer.byteLength is a required property.");
        }
        return new()
        {
            Uri = uri,
            ByteLength = (int)byteLength!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
