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
            if (propertyName == nameof(baseColorFactor))
            {
                baseColorFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(baseColorTexture))
            {
                baseColorTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(metallicFactor))
            {
                metallicFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(roughnessFactor))
            {
                roughnessFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(metallicRoughnessTexture))
            {
                metallicRoughnessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MaterialPbrMetallicRoughness>(ref jsonReader, context);
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
