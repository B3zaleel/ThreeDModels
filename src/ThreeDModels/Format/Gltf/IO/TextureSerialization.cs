using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class TextureSerialization
{
    public static Texture? Read(GltfReaderContext context)
    {
        int? sampler = null;
        int? source = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Texture.Sampler)))
            {
                sampler = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Texture.Source)))
            {
                source = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Texture.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Texture.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Texture>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Texture.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new()
        {
            Sampler = sampler,
            Source = source,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
