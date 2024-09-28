using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationSerialization
{
    public static Animation? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<AnimationChannel>? channels = null;
        List<AnimationSampler>? samplers = null;
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
            if (propertyName == nameof(channels))
            {
                channels = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AnimationChannelSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(samplers))
            {
                samplers = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => AnimationSamplerSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Animation>(ref jsonReader, context);
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
        if (channels == null || samplers == null)
        {
            throw new InvalidDataException("Animation.channels and Animation.samplers are required properties");
        }
        return new()
        {
            Channels = channels!,
            Samplers = samplers!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Animation? animation)
    {
        if (animation is null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Animation.Channels);
        WriteList(ref jsonWriter, context, animation.Channels, AnimationChannelSerialization.Write);
        jsonWriter.WritePropertyName(ElementName.Animation.Samplers);
        WriteList(ref jsonWriter, context, animation.Samplers, AnimationSamplerSerialization.Write);
        if (animation.Name is not null)
        {
            jsonWriter.WriteString(nameof(animation.Name), animation.Name);
        }
        if (animation.Extensions is not null)
        {
            ExtensionsSerialization.Write<Animation>(ref jsonWriter, context, animation.Extensions);
        }
        if (animation.Extras is not null)
        {
            JsonSerialization.Write(ref jsonWriter, context, animation.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
