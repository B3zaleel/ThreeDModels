using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the sheen material model.
/// </summary>
public class KHR_materials_sheen : IGltfProperty
{
    /// <summary>
    /// Color of the sheen layer (in linear space).
    /// </summary>
    public required float[] SheenColorFactor { get; set; }
    /// <summary>
    /// The sheen color (RGB) texture.
    /// </summary>
    public TextureInfo? SheenColorTexture { get; set; }
    /// <summary>
    /// The sheen layer roughness of the material.
    /// </summary>
    public float SheenRoughnessFactor { get; set; }
    /// <summary>
    /// The sheen roughness (Alpha) texture.
    /// </summary>
    public TextureInfo? SheenRoughnessTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsSheenExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_sheen);
    public static readonly float[] Default_SheenColorFactor = [0.0f, 0.0f, 0.0f];
    public const float Default_SheenRoughnessFactor = 0.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_sheen must be used in a Material.");
        }
        float[]? sheenColorFactor = null;
        TextureInfo? sheenColorTexture = null;
        float? sheenRoughnessFactor = null;
        TextureInfo? sheenRoughnessTexture = null;
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
            if (propertyName == nameof(sheenColorFactor))
            {
                sheenColorFactor = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(sheenColorTexture))
            {
                sheenColorTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(sheenRoughnessFactor))
            {
                sheenRoughnessFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(sheenRoughnessTexture))
            {
                sheenRoughnessTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_sheen>(ref jsonReader, context);
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
        if (sheenColorFactor != null && sheenColorFactor.Length != 3)
        {
            throw new InvalidDataException("KHR_materials_sheen.sheenColorFactor must have 3 elements.");
        }
        return new KHR_materials_sheen()
        {
            SheenColorFactor = sheenColorFactor ?? Default_SheenColorFactor,
            SheenColorTexture = sheenColorTexture,
            SheenRoughnessFactor = sheenRoughnessFactor ?? Default_SheenRoughnessFactor,
            SheenRoughnessTexture = sheenRoughnessTexture,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
