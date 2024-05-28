using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class ADOBE_materials_clearcoat_tint : IGltfProperty
{
    /// <summary>
    /// The transmittance of the clearcoat layer.
    /// </summary>
    public required float[] ClearcoatTintFactor { get; set; }
    /// <summary>
    /// The clearcoat layer tint texture.
    /// </summary>
    public TextureInfo? ClearcoatTintTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class AdobeMaterialsClearcoatTintExtension : IGltfExtension
{
    public string Name => nameof(ADOBE_materials_clearcoat_tint);
    public static readonly float[] Default_ClearcoatTintFactor = [1.0f, 1.0f, 1.0f];

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("ADOBE_materials_clearcoat_tint must be used in a Material.");
        }
        float[]? clearcoatTintFactor = null;
        TextureInfo? clearcoatTintTexture = null;
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
            if (propertyName == nameof(clearcoatTintFactor))
            {
                clearcoatTintFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(clearcoatTintTexture))
            {
                clearcoatTintTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<ADOBE_materials_clearcoat_tint>(ref jsonReader, context);
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
        if (clearcoatTintFactor != null && clearcoatTintFactor.Length != 3)
        {
            throw new InvalidDataException("ADOBE_materials_clearcoat_tint.clearcoatTintFactor must have 3 items.");
        }
        return new ADOBE_materials_clearcoat_tint()
        {
            ClearcoatTintFactor = clearcoatTintFactor ?? Default_ClearcoatTintFactor,
            ClearcoatTintTexture = clearcoatTintTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
