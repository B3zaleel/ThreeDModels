using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

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
                extras = JsonSerialization.Read(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, MaterialPbrMetallicRoughness? materialPbrMetallicRoughness)
    {
        if (materialPbrMetallicRoughness == null)
        {
            return;
        }
        jsonWriter.WriteStartObject();
        if (materialPbrMetallicRoughness.BaseColorFactor != null && !materialPbrMetallicRoughness.BaseColorFactor.SequenceEqual(Default.Material_BaseColorFactor))
        {
            jsonWriter.WritePropertyName(ElementName.MaterialPbrMetallicRoughness.BaseColorFactor);
            WriteFloatList(ref jsonWriter, context, materialPbrMetallicRoughness.BaseColorFactor.ToList());
        }
        if (materialPbrMetallicRoughness.BaseColorTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.MaterialPbrMetallicRoughness.BaseColorTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, materialPbrMetallicRoughness.BaseColorTexture);
        }
        if (materialPbrMetallicRoughness.MetallicFactor != null && materialPbrMetallicRoughness.MetallicFactor != Default.Material_Factor)
        {
            jsonWriter.WriteNumber(ElementName.MaterialPbrMetallicRoughness.MetallicFactor, (float)materialPbrMetallicRoughness.MetallicFactor);
        }
        if (materialPbrMetallicRoughness.RoughnessFactor != null && materialPbrMetallicRoughness.RoughnessFactor != Default.Material_Factor)
        {
            jsonWriter.WriteNumber(ElementName.MaterialPbrMetallicRoughness.RoughnessFactor, (float)materialPbrMetallicRoughness.RoughnessFactor);
        }
        if (materialPbrMetallicRoughness.MetallicRoughnessTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.MaterialPbrMetallicRoughness.MetallicRoughnessTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, materialPbrMetallicRoughness.MetallicRoughnessTexture);
        }
        if (materialPbrMetallicRoughness.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MaterialPbrMetallicRoughness>(ref jsonWriter, context, materialPbrMetallicRoughness.Extensions);
        }
        if (materialPbrMetallicRoughness.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, materialPbrMetallicRoughness.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
