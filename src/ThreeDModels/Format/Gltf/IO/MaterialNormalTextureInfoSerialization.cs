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
            else if (propertyName == nameof(scale))
            {
                scale = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MaterialNormalTextureInfo>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, MaterialNormalTextureInfo? materialNormalTextureInfo)
    {
        if (materialNormalTextureInfo == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.TextureInfo.Index, materialNormalTextureInfo.Index);
        if (materialNormalTextureInfo.TexCoord != null && materialNormalTextureInfo.TexCoord != Default.Material_TexCoord)
        {
            jsonWriter.WriteNumber(ElementName.TextureInfo.TexCoord, (int)materialNormalTextureInfo.TexCoord);
        }
        if (materialNormalTextureInfo.Scale != Default.Material_Factor)
        {
            jsonWriter.WriteNumber(ElementName.Node.Scale, materialNormalTextureInfo.Scale);
        }
        if (materialNormalTextureInfo.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MaterialNormalTextureInfo>(ref jsonWriter, context, materialNormalTextureInfo.Extensions);
        }
        if (materialNormalTextureInfo.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, materialNormalTextureInfo.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
