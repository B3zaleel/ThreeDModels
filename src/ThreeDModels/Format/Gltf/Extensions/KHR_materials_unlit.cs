using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_materials_unlit : IGltfProperty
{
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
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

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Material))
        {
            throw new InvalidDataException("KHR_materials_unlit must be used in a Material.");
        }
        var khrMaterialsUnlit = (KHR_materials_unlit?)element;
        if (khrMaterialsUnlit == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (khrMaterialsUnlit.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KHR_materials_unlit>(ref jsonWriter, context, khrMaterialsUnlit.Extensions);
        }
        if (khrMaterialsUnlit.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsUnlit.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
