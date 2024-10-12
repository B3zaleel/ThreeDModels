using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that indicates that some edges of a primitive's triangles should be outlined.
/// </summary>
public class CESIUM_primitive_outline : IGltfProperty
{
    public int? Indices { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class CesiumPrimitiveOutlineExtension : IGltfExtension
{
    public string Name => nameof(CESIUM_primitive_outline);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("CESIUM_primitive_outline must be used in a MeshPrimitive.");
        }
        int? indices = null;
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
            if (propertyName == nameof(indices))
            {
                indices = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<CESIUM_primitive_outline>(ref jsonReader, context);
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
        return new CESIUM_primitive_outline()
        {
            Indices = indices,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("CESIUM_primitive_outline must be used in a MeshPrimitive.");
        }
        var cesiumPrimitiveOutline = (CESIUM_primitive_outline?)element;
        if (cesiumPrimitiveOutline == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (cesiumPrimitiveOutline.Indices != null)
        {
            jsonWriter.WritePropertyName(ElementName.AccessorSparse.Indices);
            jsonWriter.WriteNumberValue((int)cesiumPrimitiveOutline.Indices);
        }
        if (cesiumPrimitiveOutline.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<CESIUM_primitive_outline>(ref jsonWriter, context, cesiumPrimitiveOutline.Extensions);
        }
        if (cesiumPrimitiveOutline.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, cesiumPrimitiveOutline.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
