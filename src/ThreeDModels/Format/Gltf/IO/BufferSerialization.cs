using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class BufferSerialization
{
    public static Elements.Buffer? Read(GltfReaderContext context)
    {
        string? uri = null;
        int? byteLength = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Uri)))
            {
                uri = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.ByteLength)))
            {
                byteLength = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Elements.Buffer>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Elements.Buffer.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
