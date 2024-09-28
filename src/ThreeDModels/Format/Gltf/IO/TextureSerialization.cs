using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class TextureSerialization
{
    public static Texture? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? sampler = null;
        int? source = null;
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
            if (propertyName == nameof(sampler))
            {
                sampler = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(source))
            {
                source = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Texture>(ref jsonReader, context);
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
        return new()
        {
            Sampler = sampler,
            Source = source,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Texture? texture)
    {
        if (texture is null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (texture.Sampler is not null)
        {
            jsonWriter.WriteNumber(ElementName.AnimationChannel.Sampler, (int)texture.Sampler);
        }
        if (texture.Source is not null)
        {
            jsonWriter.WriteNumber(ElementName.Texture.Source, (int)texture.Source);
        }
        if (texture.Name is not null)
        {
            jsonWriter.WriteString(nameof(texture.Name), texture.Name);
        }
        if (texture.Extensions is not null)
        {
            ExtensionsSerialization.Write<Texture>(ref jsonWriter, context, texture.Extensions);
        }
        if (texture.Extras is not null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, texture.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
