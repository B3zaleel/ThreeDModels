using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SamplerSerialization
{
    public static Sampler? Read(GltfReaderContext context)
    {
        int? magFilter = null;
        int? minFilter = null;
        int? wrapS = null;
        int? wrapT = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        context.JsonReader.Read();
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException($"Failed to find {nameof(Asset)} property");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.MagFilter)))
            {
                magFilter = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.MinFilter)))
            {
                minFilter = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapS)))
            {
                wrapS = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapT)))
            {
                wrapT = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.WrapT)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Sampler>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Sampler.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
