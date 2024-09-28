using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshSerialization
{
    public static Mesh? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<MeshPrimitive>? primitives = null;
        List<float>? weights = null;
        string? name = null;
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
            if (propertyName == nameof(primitives))
            {
                primitives = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MeshPrimitiveSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(weights))
            {
                weights = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Mesh>(ref jsonReader, context);
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
            Primitives = primitives!,
            Weights = weights!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Mesh? mesh)
    {
        if (mesh == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Mesh.Primitives);
        WriteList(ref jsonWriter, context, mesh.Primitives, MeshPrimitiveSerialization.Write);
        jsonWriter.WritePropertyName(ElementName.Mesh.Weights);
        WriteFloatList(ref jsonWriter, context, mesh.Weights);
        if (mesh.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, mesh.Name);
        }
        if (mesh.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Mesh>(ref jsonWriter, context, mesh.Extensions);
        }
        if (mesh.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, mesh.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
