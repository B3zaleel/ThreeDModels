using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_texture_transform : IGltfProperty
{
    /// <summary>
    /// The offset of the UV coordinate origin as a factor of the texture dimensions.
    /// </summary>
    public required float[] Offset { get; set; }
    /// <summary>
    /// Rotate the UVs by this many radians counter-clockwise around the origin.
    /// </summary>
    public float Rotation { get; set; }
    /// <summary>
    /// The scale factor applied to the components of the UV coordinates.
    /// </summary>
    public required float[] Scale { get; set; }
    /// <summary>
    /// Overrides the textureInfo texCoord value if supplied, and if this extension is supported.
    /// </summary>
    public int? TexCoord { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrTextureTransformExtension : IGltfExtension
{
    public string Name => nameof(KHR_texture_transform);
    public static readonly float[] Default_Offset = [0.0f, 0.0f];
    public const float Default_Rotation = 0.0f;
    public static readonly float[] Default_Scale = [1.0f, 1.0f];

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(TextureInfo))
        {
            throw new InvalidDataException("KHR_texture_transform must be used in a TextureInfo.");
        }
        float[]? offset = null;
        float? rotation = null;
        float[]? scale = null;
        int? texCoord = null;
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
            if (propertyName == nameof(offset))
            {
                offset = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(rotation))
            {
                rotation = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(scale))
            {
                scale = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(texCoord))
            {
                texCoord = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_texture_transform>(ref jsonReader, context);
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
        if (offset != null && offset.Length != 2)
        {
            throw new InvalidDataException("KHR_texture_transform.offset must have 2 items.");
        }
        if (scale != null && scale.Length != 2)
        {
            throw new InvalidDataException("KHR_texture_transform.scale must have 2 items.");
        }
        return new KHR_texture_transform()
        {
            Offset = offset ?? Default_Offset,
            Rotation = rotation ?? Default_Rotation,
            Scale = scale ?? Default_Scale,
            TexCoord = texCoord,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
