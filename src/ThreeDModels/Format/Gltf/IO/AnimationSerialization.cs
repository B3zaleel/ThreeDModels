using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationSerialization
{
    public static Animation? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<AnimationChannel>? channels = null;
        List<AnimationSampler>? samplers = null;
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
                extras = ExtrasSerialization.Read(ref jsonReader, context);
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
}
