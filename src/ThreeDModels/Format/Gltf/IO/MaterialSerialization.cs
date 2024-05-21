using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MaterialSerialization
{
    public static Material? Read(GltfReaderContext context)
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.PbrMetallicRoughness)))
            {
                pbrMetallicRoughness = MaterialPbrMetallicRoughnessSerialization.Read(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.NormalTexture)))
            {
                normalTexture = MaterialNormalTextureInfoSerialization.Read(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.OcclusionTexture)))
            {
                occlusionTexture = MaterialOcclusionTextureInfoSerialization.Read(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.EmissiveTexture)))
            {
                emissiveTexture = TextureInfoSerialization.Read(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.EmissiveFactor)))
            {
                emissiveFactor = ReadFloatList(context)?.ToArray();
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.AlphaMode)))
            {
                alphaMode = ReadString(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.AlphaCutoff)))
            {
                alphaCutoff = ReadFloat(context);
            }
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.DoubleSided)))
            {
                doubleSided = ReadBoolean(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Material>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Material.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
