using System.Text.Json;

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
}
