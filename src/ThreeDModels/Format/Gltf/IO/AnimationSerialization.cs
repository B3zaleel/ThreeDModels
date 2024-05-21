using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationSerialization
{
    public static Animation? Read(GltfReaderContext context)
    {
        List<AnimationChannel>? channels = null;
        List<AnimationSampler>? samplers = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Animation.Channels)))
            {
                channels = ReadList(context, JsonTokenType.StartObject, reader => AnimationChannelSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Animation.Samplers)))
            {
                samplers = ReadList(context, JsonTokenType.StartObject, reader => AnimationSamplerSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Animation.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Animation.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Animation>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Animation.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
