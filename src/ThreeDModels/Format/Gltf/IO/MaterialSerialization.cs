using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

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
}
