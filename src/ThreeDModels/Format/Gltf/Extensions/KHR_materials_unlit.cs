using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_materials_unlit : IGltfProperty
{
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}

public class KhrMaterialsUnlitExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_unlit);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_unlit must be used in a Material.");
        }
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
            if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KHR_materials_unlit>(ref jsonReader, context);
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
        return new KHR_materials_unlit()
        {
            Extensions = extensions,
            Extras = extras,
        };
    }
}
