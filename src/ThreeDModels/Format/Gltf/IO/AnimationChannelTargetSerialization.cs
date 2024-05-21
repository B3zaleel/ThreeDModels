using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationChannelTargetSerialization
{
    public static AnimationChannelTarget? Read(GltfReaderContext context)
    {
        int? node = null;
        string? path = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannelTarget.Node)))
            {
                node = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannelTarget.Path)))
            {
                path = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannelTarget.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AnimationChannelTarget>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationChannelTarget.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (path is null)
        {
            throw new InvalidDataException("AnimationChannelTarget.path is a required property.");
        }
        return new()
        {
            Node = node,
            Path = path!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
