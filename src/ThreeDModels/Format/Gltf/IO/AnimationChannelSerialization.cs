using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationChannelSerialization
{
    public static AnimationChannel? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? sampler = null;
        AnimationChannelTarget? target = null;
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
            else if (propertyName == nameof(target))
            {
                target = AnimationChannelTargetSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AnimationChannel>(ref jsonReader, context);
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
        if (sampler is null || target is null)
        {
            throw new InvalidDataException("AnimationChannel.sampler and AnimationChannel.target are required properties.");
        }
        return new()
        {
            Sampler = (int)sampler!,
            Target = target!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AnimationChannel? animationChannel)
    {
        if (animationChannel is null)
        {
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.AnimationChannel.Sampler, animationChannel.Sampler);
        jsonWriter.WritePropertyName(ElementName.AnimationChannel.Target);
        AnimationChannelTargetSerialization.Write(ref jsonWriter, context, animationChannel.Target);
        if (animationChannel.Extensions is not null)
        {
            jsonWriter.WritePropertyName(nameof(animationChannel.Extensions));
            ExtensionsSerialization.Write<AnimationChannel>(ref jsonWriter, context, animationChannel.Extensions);
        }
        if (animationChannel.Extras is not null)
        {
            jsonWriter.WritePropertyName(nameof(animationChannel.Extras));
            JsonSerialization.Write(ref jsonWriter, context, animationChannel.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
