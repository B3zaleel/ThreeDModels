using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SceneSerialization
{
    public static Scene? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<int>? nodes = null;
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
            if (propertyName == nameof(nodes))
            {
                nodes = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Scene>(ref jsonReader, context);
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
            Nodes = nodes,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Scene? scene)
    {
        if (scene == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (scene.Nodes != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Nodes);
            WriteIntegerList(ref jsonWriter, context, scene.Nodes);
        }
        if (scene.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, scene.Name);
        }
        if (scene.Extensions != null)
        {
            ExtensionsSerialization.Write<Scene>(ref jsonWriter, context, scene.Extensions);
        }
        if (scene.Extras != null)
        {
            JsonSerialization.Write(ref jsonWriter, context, scene.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
