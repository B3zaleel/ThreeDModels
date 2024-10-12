using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object for specifying levels of detail (LOD).
/// </summary>
public class MSFT_lod : IGltfProperty
{
    /// <summary>
    /// Array containing the indices of progressively lower LOD nodes.
    /// </summary>
    public List<int>? Ids { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MsftLodExtension : IGltfExtension
{
    public string Name => nameof(MSFT_lod);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("MSFT_packing_normalRoughnessMetallic must be used in a Gltf root.");
        }
        List<int>? ids = null;
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
            if (propertyName == nameof(ids))
            {
                ids = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MSFT_lod>(ref jsonReader, context);
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
        return new MSFT_lod()
        {
            Ids = ids,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(Gltf))
        {
            throw new InvalidDataException("MSFT_lod must be used in a Gltf root.");
        }
        var msftLod = (MSFT_lod?)element;
        if (msftLod == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (msftLod.Ids != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.MSFT_lod.Ids);
            WriteIntegerList(ref jsonWriter, context, msftLod.Ids);
        }
        if (msftLod.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MSFT_lod>(ref jsonWriter, context, msftLod.Extensions);
        }
        if (msftLod.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, msftLod.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
