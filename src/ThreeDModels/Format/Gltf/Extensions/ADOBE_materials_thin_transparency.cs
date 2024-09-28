using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class ADOBE_materials_thin_transparency : IGltfProperty
{
    public float TransmissionFactor { get; set; }
    public TextureInfo? TransmissionTexture { get; set; }
    public float Ior { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class AdobeMaterialsThinTransparencyExtension : IGltfExtension
{
    public string Name => nameof(ADOBE_materials_thin_transparency);
    public const float Default_TransmissionFactor = 1.0f;
    public const float Default_Ior = 1.33f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("ADOBE_materials_thin_transparency must be used in a Gltf root.");
        }
        float? transmissionFactor = null;
        TextureInfo? transmissionTexture = null;
        float? ior = null;
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
            if (propertyName == nameof(transmissionFactor))
            {
                transmissionFactor = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(transmissionTexture))
            {
                transmissionTexture = TextureInfoSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(ior))
            {
                ior = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<ADOBE_materials_thin_transparency>(ref jsonReader, context);
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
        return new ADOBE_materials_thin_transparency()
        {
            TransmissionFactor = transmissionFactor ?? Default_TransmissionFactor,
            TransmissionTexture = transmissionTexture,
            Ior = ior ?? Default_Ior,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
