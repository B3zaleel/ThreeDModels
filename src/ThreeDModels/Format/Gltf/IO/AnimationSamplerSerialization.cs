using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationSamplerSerialization
{
    public static AnimationSampler? Read(GltfReaderContext context)
    {
        int? input = null;
        string? interpolation = null;
        int? output = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationSampler.Input)))
            {
                input = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationSampler.Interpolation)))
            {
                interpolation = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationSampler.Output)))
            {
                output = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationSampler.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AnimationSampler>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AnimationSampler.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (input == null || output == null)
        {
            throw new InvalidDataException("AnimationSampler.input and AnimationSampler.output are required properties.");
        }
        return new()
        {
            Input = (int)input!,
            Interpolation = interpolation ?? Default.InterpolationAlgorithm,
            Output = (int)output!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
