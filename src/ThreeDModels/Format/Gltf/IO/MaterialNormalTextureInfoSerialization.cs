using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialNormalTextureInfoSerialization
{
    public static MaterialNormalTextureInfo? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? index = null;
        int? texCoord = null;
        float? scale = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialNormalTextureInfo.Index)))
            {
                index = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialNormalTextureInfo.TexCoord)))
            {
                texCoord = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialNormalTextureInfo.Scale)))
            {
                scale = ReadFloat(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialNormalTextureInfo.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MaterialNormalTextureInfo>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MaterialNormalTextureInfo.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (index is null)
        {
            throw new InvalidDataException("MaterialNormalTextureInfo.index is a required property.");
        }
        return new()
        {
            Index = (int)index!,
            TexCoord = texCoord ?? Default.Material_TexCoord,
            Scale = scale ?? Default.Material_Factor,
            Extensions = extensions,
            Extras = extras,
        };
    }
}