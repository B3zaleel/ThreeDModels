using System.Text.Json;
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
            else if (propertyName == nameof(byteLength))
            {
                byteLength = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Elements.Buffer>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Elements.Buffer? buffer)
    {
        if (buffer == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (buffer.Uri != null)
        {
            jsonWriter.WriteString(ElementName.Buffer.Uri, buffer.Uri);
        }
        jsonWriter.WriteNumber(ElementName.Buffer.ByteLength, buffer.ByteLength);
        if (buffer.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, buffer.Name);
        }
        if (buffer.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Elements.Buffer>(ref jsonWriter, context, buffer.Extensions);
        }
        if (buffer.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, buffer.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
