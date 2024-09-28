using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;

namespace ThreeDModels.Format.Gltf.IO;

public static class ExtensionsSerialization
{
    public static Dictionary<string, object?>? Read<TParent>(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        Dictionary<string, object?>? extensions = null;
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
            else if (jsonReader.TokenType != JsonTokenType.PropertyName)
            {
                throw new InvalidDataException("Failed to find extension property name");
            }
            var propertyName = jsonReader.GetString()!;
            if (context.Extensions.TryGetValue(propertyName, out var reader))
            {
                extensions ??= [];
                extensions.Add(propertyName, reader(ref jsonReader, context, typeof(TParent)));
            }
            else
            {
                throw new InvalidDataException($"Failed to find extension reader for {propertyName}");
            }
        }
        return extensions;
    }

    public static void Write<TParent>(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Dictionary<string, object?>? extensions)
    {
        if (extensions == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        foreach (var extension in extensions)
        {
            jsonWriter.WritePropertyName(extension.Key);
            if (context.Extensions.TryGetValue(extension.Key, out var extensionWriter))
            {
                extensionWriter(ref jsonWriter, context, typeof(TParent), extension.Value);
            }
            else
            {
                throw new InvalidDataException($"Failed to find writer for extension `{extension.Key}`");
            }
        }
        jsonWriter.WriteEndObject();
    }
}
