using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialPbrMetallicRoughnessSerialization
{
    public static MaterialPbrMetallicRoughness? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float[]? baseColorFactor = null;
        TextureInfo? baseColorTexture = null;
        float? metallicFactor = null;
        float? roughnessFactor = null;
        TextureInfo? metallicRoughnessTexture = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.BaseColorFactor)))
            {
                baseColorFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.BaseColorTexture)))
            {
                baseColorTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.MetallicFactor)))
            {
                metallicFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.RoughnessFactor)))
            {
                roughnessFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.MetallicRoughnessTexture)))
            {
                metallicRoughnessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MaterialPbrMetallicRoughness>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialPbrMetallicRoughness.Extras)))
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
