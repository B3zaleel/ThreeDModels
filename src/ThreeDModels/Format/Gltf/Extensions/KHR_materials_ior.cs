using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that defines the index of refraction of a material.
/// </summary>
public class KHR_materials_ior : IGltfProperty
{
    /// <summary>
    /// The index of refraction.
    /// </summary>
    public float Ior { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class KhrMaterialsIorExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_ior);
    public const float Default_Ior = 1.5f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("KHR_materials_ior must be used in a MeshPrimitive.");
        }
        float? ior = null;
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
            if (propertyName == nameof(ior))
            {
                ior = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_ior>(ref jsonReader, context);
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
        return new KHR_materials_ior()
        {
            Ior = ior ?? Default_Ior,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
