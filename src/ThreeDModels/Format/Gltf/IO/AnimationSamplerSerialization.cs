using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationSamplerSerialization
{
    public static AnimationSampler? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? input = null;
        string? interpolation = null;
        int? output = null;
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
            if (propertyName == nameof(input))
            {
                input = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(interpolation))
            {
                interpolation = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(output))
            {
                output = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AnimationSampler>(ref jsonReader, context);
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
