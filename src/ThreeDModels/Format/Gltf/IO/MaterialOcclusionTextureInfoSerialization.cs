using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialOcclusionTextureInfoSerialization
{
    public static MaterialOcclusionTextureInfo? Read(GltfReaderContext context)
    {
        int? index = null;
        int? texCoord = null;
        float? strength = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialOcclusionTextureInfo.Index)))
            {
                index = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialOcclusionTextureInfo.TexCoord)))
            {
                texCoord = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialOcclusionTextureInfo.Strength)))
            {
                strength = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialOcclusionTextureInfo.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MaterialOcclusionTextureInfo>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialOcclusionTextureInfo.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (index is null)
        {
            throw new InvalidDataException("MaterialOcclusionTextureInfo.index is a required property.");
        }
        return new()
        {
            Index = (int)index!,
            TexCoord = texCoord ?? Default.Material_TexCoord,
            Strength = strength ?? Default.Material_Factor,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
