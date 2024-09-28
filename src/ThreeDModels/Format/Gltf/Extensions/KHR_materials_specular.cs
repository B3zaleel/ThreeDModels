using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the strength of the specular reflection.
/// </summary>
public class KHR_materials_specular : IGltfProperty
{
    /// <summary>
    /// The strength of the specular reflection.
    /// </summary>
    public required float SpecularFactor { get; set; }
    /// <summary>
    /// A texture that defines the specular factor in the alpha channel.
    /// </summary>
    public TextureInfo? SpecularTexture { get; set; }
    /// <summary>
    /// The F0 RGB color of the specular reflection.
    /// </summary>
    public required float[] SpecularColorFactor { get; set; }
    /// <summary>
    /// A texture that defines the F0 color of the specular reflection.
    /// </summary>
    public TextureInfo? SpecularColorTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsSpecularExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_specular);
    public const float Default_SpecularFactor = 1.0f;
    public static readonly float[] Default_SpecularColorFactor = [1.0f, 1.0f, 1.0f];

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_specular must be used in a Material.");
        }
        float? specularFactor = null;
        TextureInfo? specularTexture = null;
        float[]? specularColorFactor = null;
        TextureInfo? specularColorTexture = null;
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
            if (propertyName == nameof(specularFactor))
            {
                specularFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(specularTexture))
            {
                specularTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(specularColorFactor))
            {
                specularColorFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(specularColorTexture))
            {
                specularColorTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_specular>(ref jsonReader, context);
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
        if (specularColorFactor != null && specularColorFactor.Length != 3)
        {
            throw new InvalidDataException("KHR_materials_specular.specularColorFactor must have 3 elements.");
        }
        return new KHR_materials_specular()
        {
            SpecularFactor = specularFactor ?? Default_SpecularFactor,
            SpecularTexture = specularTexture,
            SpecularColorFactor = specularColorFactor ?? Default_SpecularColorFactor,
            SpecularColorTexture = specularColorTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
