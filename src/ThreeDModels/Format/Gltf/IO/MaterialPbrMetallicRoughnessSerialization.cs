using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialPbrMetallicRoughnessSerialization
{
    public static MaterialPbrMetallicRoughness? Read(GltfReaderContext context)
    {
        float[]? baseColorFactor = null;
        TextureInfo? baseColorTexture = null;
        float? metallicFactor = null;
        float? roughnessFactor = null;
        TextureInfo? metallicRoughnessTexture = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.BaseColorFactor)))
            {
                baseColorFactor = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.BaseColorTexture)))
            {
                baseColorTexture = TextureInfoSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.MetallicFactor)))
            {
                metallicFactor = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.RoughnessFactor)))
            {
                roughnessFactor = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.MetallicRoughnessTexture)))
            {
                metallicRoughnessTexture = TextureInfoSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MaterialPbrMetallicRoughness>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.Extras)))
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
            BaseColorFactor = baseColorFactor ?? Default.Material_BaseColorFactor,
            BaseColorTexture = baseColorTexture,
            MetallicFactor = metallicFactor ?? Default.Material_Factor,
            RoughnessFactor = roughnessFactor ?? Default.Material_Factor,
            MetallicRoughnessTexture = metallicRoughnessTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
