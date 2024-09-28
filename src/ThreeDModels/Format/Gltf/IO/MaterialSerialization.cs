using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialSerialization
{
    public static Material? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        MaterialPbrMetallicRoughness? pbrMetallicRoughness = null;
        MaterialNormalTextureInfo? normalTexture = null;
        MaterialOcclusionTextureInfo? occlusionTexture = null;
        TextureInfo? emissiveTexture = null;
        float[]? emissiveFactor = null;
        string? alphaMode = null;
        float? alphaCutoff = null;
        bool? doubleSided = null;
        string? name = null;
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
            if (propertyName == nameof(pbrMetallicRoughness))
            {
                pbrMetallicRoughness = MaterialPbrMetallicRoughnessSerialization.Read(ref jsonReader, context);
            }
            if (propertyName == nameof(normalTexture))
            {
                normalTexture = MaterialNormalTextureInfoSerialization.Read(ref jsonReader, context);
            }
            if (propertyName == nameof(occlusionTexture))
            {
                occlusionTexture = MaterialOcclusionTextureInfoSerialization.Read(ref jsonReader, context);
            }
            if (propertyName == nameof(emissiveTexture))
            {
                emissiveTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            if (propertyName == nameof(emissiveFactor))
            {
                emissiveFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            if (propertyName == nameof(alphaMode))
            {
                alphaMode = ReadString(ref jsonReader);
            }
            if (propertyName == nameof(alphaCutoff))
            {
                alphaCutoff = ReadFloat(ref jsonReader);
            }
            if (propertyName == nameof(doubleSided))
            {
                doubleSided = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Material>(ref jsonReader, context);
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
        if (emissiveFactor != null && emissiveFactor.Length != Default.Material_EmissiveFactor.Length)
        {
            throw new InvalidDataException($"Material.emissiveFactor must have {Default.Material_EmissiveFactor.Length} elements.");
        }
        if (alphaCutoff != null && alphaMode == null)
        {
            throw new InvalidDataException("Material.alphaCutoff is only valid when Material.alphaMode is defined.");
        }
        return new()
        {
            PbrMetallicRoughness = pbrMetallicRoughness,
            NormalTexture = normalTexture,
            OcclusionTexture = occlusionTexture,
            EmissiveTexture = emissiveTexture,
            EmissiveFactor = emissiveFactor ?? Default.Material_EmissiveFactor,
            AlphaMode = alphaMode,
            AlphaCutoff = alphaCutoff ?? Default.Material_AlphaCutoff,
            DoubleSided = doubleSided ?? Default.Material_DoubleSided,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Material? material)
    {
        if (material == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (material.EmissiveFactor != null && material.EmissiveFactor.Length != Default.Material_EmissiveFactor.Length)
        {
            throw new InvalidDataException($"Material.emissiveFactor must have {Default.Material_EmissiveFactor.Length} elements.");
        }
        if (material.AlphaCutoff != null && material.AlphaMode == null)
        {
            throw new InvalidDataException("Material.alphaCutoff is only valid when Material.alphaMode is defined.");
        }
        jsonWriter.WriteStartObject();
        if (material.PbrMetallicRoughness != null)
        {
            jsonWriter.WritePropertyName(ElementName.Material.PbrMetallicRoughness);
            MaterialPbrMetallicRoughnessSerialization.Write(ref jsonWriter, context, material.PbrMetallicRoughness);
        }
        if (material.NormalTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Material.NormalTexture);
            MaterialNormalTextureInfoSerialization.Write(ref jsonWriter, context, material.NormalTexture);
        }
        if (material.OcclusionTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Material.OcclusionTexture);
            MaterialOcclusionTextureInfoSerialization.Write(ref jsonWriter, context, material.OcclusionTexture);
        }
        if (material.EmissiveTexture != null)
        {
            jsonWriter.WritePropertyName(ElementName.Material.EmissiveTexture);
            TextureInfoSerialization.Write(ref jsonWriter, context, material.EmissiveTexture);
        }
        if (material.EmissiveFactor != null && !material.EmissiveFactor.SequenceEqual(Default.Material_EmissiveFactor))
        {
            jsonWriter.WritePropertyName(ElementName.Material.EmissiveFactor);
            WriteFloatList(ref jsonWriter, context, material.EmissiveFactor.ToList());
        }
        if (material.AlphaMode != null)
        {
            jsonWriter.WriteString(ElementName.Material.AlphaMode, material.AlphaMode);
        }
        if (material.AlphaCutoff != null && material.AlphaCutoff != Default.Material_AlphaCutoff)
        {
            jsonWriter.WriteNumber(ElementName.Material.AlphaCutoff, (float)material.AlphaCutoff);
        }
        jsonWriter.WriteBoolean(ElementName.Material.DoubleSided, material.DoubleSided);
        if (material.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, material.Name);
        }
        if (material.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Material>(ref jsonWriter, context, material.Extensions);
        }
        if (material.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, material.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
