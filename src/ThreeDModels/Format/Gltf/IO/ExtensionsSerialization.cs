using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public static class ExtensionsSerialization
{
    public static Dictionary<string, object?>? Read<TParent>(GltfReaderContext context)
    {
        Dictionary<string, object?>? extensions = null;
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
            else if (context.JsonReader.TokenType != JsonTokenType.PropertyName)
            {
                throw new InvalidDataException("Failed to find extension property name");
            }
            var propertyName = context.JsonReader.GetString()!;
            if (context.Extensions.TryGetValue(propertyName, out var reader))
            {
                extensions ??= [];
                extensions.Add(propertyName, reader(context, typeof(TParent)));
            }
            else
            {
                throw new InvalidDataException($"Failed to find extension reader for {propertyName}");
            }
        }
        return extensions;
    }
}
