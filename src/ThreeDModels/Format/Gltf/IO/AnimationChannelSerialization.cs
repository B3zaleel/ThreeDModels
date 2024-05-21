using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationChannelSerialization
{
    public static AnimationChannel? Read(GltfReaderContext context)
    {
        int? sampler = null;
        AnimationChannelTarget? target = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannel.Sampler)))
            {
                sampler = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannel.Target)))
            {
                target = AnimationChannelTargetSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannel.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AnimationChannel>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannel.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
}
