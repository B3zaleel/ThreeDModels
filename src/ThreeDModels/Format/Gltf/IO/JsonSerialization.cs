using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;

namespace ThreeDModels.Format.Gltf.IO;

public static class JsonSerialization
{
    public static Elements.JsonElement? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        Elements.JsonElement? element = null;
        int objectDepth = 0;
        int arrayDepth = 0;
        string? propertyName = null;
        if (jsonReader.TokenType == JsonTokenType.PropertyName && !jsonReader.Read())
        {
            return null;
        }

        while (true)
        {
            if (jsonReader.TokenType == JsonTokenType.StartObject)
            {
                var value = new JsonObject(element, []);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.StartArray)
            {
                var value = new JsonArray(element, []);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.String)
            {
                var value = new JsonString(element, jsonReader.GetString()!);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                else if (element == null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.Number)
            {
                var value = new JsonNumber(element, jsonReader.GetDouble());
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                else if (element == null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.True)
            {
                var value = new JsonBoolean(element, true);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                else if (element == null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.False)
            {
                var value = new JsonBoolean(element, false);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                else if (element == null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.Null)
            {
                var value = new JsonNull(element);
                if (element is JsonObject jsonObject)
                {
                    jsonObject.Value.Add(propertyName!, value);
                }
                else if (element is JsonArray jsonArray)
                {
                    jsonArray.Value.Add(value);
                }
                else if (element == null)
                {
                    return value;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
                element = value;
            }
            else if (jsonReader.TokenType == JsonTokenType.PropertyName)
            {
                if (element is JsonObject)
                {
                    propertyName = jsonReader.GetString()!;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
            }
            else if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                if (element is JsonObject)
                {
                    objectDepth--;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of object.");
                }
            }
            else if (jsonReader.TokenType == JsonTokenType.EndArray)
            {
                if (element is JsonArray)
                {
                    arrayDepth--;
                }
                else
                {
                    throw new InvalidDataException("Failed to find start of array.");
                }
            }
            else
            {
                throw new InvalidDataException($"Unknown token type: {jsonReader.TokenType}");
            }
            if (element.Parent != null)
            {
                element = element.Parent;
                jsonReader.Read();
            }
            else
            {
                break;
            }
        }
        return element;
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Elements.JsonElement? element)
    {
        if (element == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (element is JsonObject jsonObject)
        {
            jsonWriter.WriteStartObject();
            foreach (var (key, value) in jsonObject.Value)
            {
                jsonWriter.WritePropertyName(key);
                Write(ref jsonWriter, context, value);
            }
            jsonWriter.WriteEndObject();
        }
        else if (element is JsonArray jsonArray)
        {
            jsonWriter.WriteStartArray();
            foreach (var value in jsonArray.Value)
            {
                Write(ref jsonWriter, context, value);
            }
            jsonWriter.WriteEndArray();
        }
        else if (element is JsonString jsonString)
        {
            jsonWriter.WriteStringValue(jsonString.Value);
        }
        else if (element is JsonNumber jsonNumber)
        {
            jsonWriter.WriteNumberValue(jsonNumber.Value);
        }
        else if (element is JsonBoolean jsonBoolean)
        {
            jsonWriter.WriteBooleanValue(jsonBoolean.Value);
        }
        else if (element is JsonNull)
        {
            jsonWriter.WriteNullValue();
        }
        else
        {
            throw new InvalidDataException($"Unknown element type: {element.GetType()}");
        }
    }
}
