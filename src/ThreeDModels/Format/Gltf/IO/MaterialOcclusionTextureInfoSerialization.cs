using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialOcclusionTextureInfoSerialization
{
    public static MaterialOcclusionTextureInfo? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? index = null;
        int? texCoord = null;
        float? strength = null;
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
            if (propertyName == nameof(index))
            {
                index = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(texCoord))
            {
                texCoord = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(strength))
            {
                strength = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MaterialOcclusionTextureInfo>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, MaterialOcclusionTextureInfo? materialOcclusionTextureInfo)
    {
        if (materialOcclusionTextureInfo == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.TextureInfo.Index, materialOcclusionTextureInfo.Index);
        if (materialOcclusionTextureInfo.TexCoord != null && materialOcclusionTextureInfo.TexCoord != Default.Material_TexCoord)
        {
            jsonWriter.WriteNumber(ElementName.TextureInfo.TexCoord, (int)materialOcclusionTextureInfo.TexCoord);
        }
        if (materialOcclusionTextureInfo.Strength != Default.Material_Factor)
        {
            jsonWriter.WriteNumber(ElementName.MaterialOcclusionTextureInfo.Strength, materialOcclusionTextureInfo.Strength);
        }
        if (materialOcclusionTextureInfo.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MaterialOcclusionTextureInfo>(ref jsonWriter, context, materialOcclusionTextureInfo.Extensions);
        }
        if (materialOcclusionTextureInfo.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, materialOcclusionTextureInfo.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
