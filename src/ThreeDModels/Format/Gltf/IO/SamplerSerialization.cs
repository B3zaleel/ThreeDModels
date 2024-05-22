using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SamplerSerialization
{
    public static Sampler? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? magFilter = null;
        int? minFilter = null;
        int? wrapS = null;
        int? wrapT = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        jsonReader.Read();
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException($"Failed to find {nameof(Asset)} property");
        }
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = jsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.MagFilter)))
            {
                magFilter = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.MinFilter)))
            {
                minFilter = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapS)))
            {
                wrapS = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapT)))
            {
                wrapT = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapT)))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Sampler>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new()
        {
            MagFilter = magFilter,
            MinFilter = minFilter,
            WrapS = wrapS ?? Default.Sampler_WrappingMode,
            WrapT = wrapT ?? Default.Sampler_WrappingMode,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
