using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

public static class IntegerMapSerialization
{
    public static IntegerMap? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        IntegerMap integerMap = [];
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
                extensions = ExtensionsSerialization.Read<IntegerMap>(ref jsonReader, context);
            }
            else if (propertyName == nameof(extras))
            {
                extras = JsonSerialization.Read(ref jsonReader, context);
            }
            else
            {
                integerMap.Add(propertyName!, (int)ReadInteger(ref jsonReader)!);
            }
        }
        integerMap.Extensions = extensions;
        integerMap.Extras = extras;
        return integerMap;
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, IntegerMap? integerMap)
    {
        if (integerMap is null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        foreach (KeyValuePair<string, int> pair in integerMap)
        {
            jsonWriter.WriteNumber(pair.Key, pair.Value);
        }
        if (integerMap.Extensions is not null)
        {
            ExtensionsSerialization.Write<IntegerMap>(ref jsonWriter, context, integerMap.Extensions);
        }
        if (integerMap.Extras is not null)
        {
            JsonSerialization.Write(ref jsonWriter, context, integerMap.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
