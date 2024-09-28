using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshPrimitiveSerialization
{
    public static MeshPrimitive? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        IntegerMap? attributes = null;
        int? indices = null;
        int? material = null;
        int? mode = null;
        List<IntegerMap>? targets = null;
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
            if (propertyName == nameof(attributes))
            {
                attributes = IntegerMapSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(indices))
            {
                indices = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(material))
            {
                material = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(mode))
            {
                mode = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(targets))
            {
                targets = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => IntegerMapSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MeshPrimitive>(ref jsonReader, context);
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
        return new()
        {
            Attributes = attributes!,
            Indices = indices,
            Material = material,
            Mode = mode ?? Default.MeshPrimitive_Mode,
            Targets = targets,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, MeshPrimitive? meshPrimitive)
    {
        if (meshPrimitive == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (meshPrimitive.Attributes != null)
        {
            jsonWriter.WritePropertyName(ElementName.MeshPrimitive.Targets);
            IntegerMapSerialization.Write(ref jsonWriter, context, meshPrimitive.Attributes);
        }
        if (meshPrimitive.Indices != null)
        {
            jsonWriter.WriteNumber(ElementName.MeshPrimitive.Indices, (int)meshPrimitive.Indices);
        }
        if (meshPrimitive.Material != null)
        {
            jsonWriter.WriteNumber(ElementName.MeshPrimitive.Material, (int)meshPrimitive.Material);
        }
        if (meshPrimitive.Mode != Default.MeshPrimitive_Mode)
        {
            jsonWriter.WriteNumber(ElementName.MeshPrimitive.Mode, meshPrimitive.Mode);
        }
        if (meshPrimitive.Targets != null)
        {
            jsonWriter.WritePropertyName(ElementName.MeshPrimitive.Targets);
            WriteList(ref jsonWriter, context, meshPrimitive.Targets, IntegerMapSerialization.Write);
        }
        if (meshPrimitive.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<MeshPrimitive>(ref jsonWriter, context, meshPrimitive.Extensions);
        }
        if (meshPrimitive.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, meshPrimitive.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
