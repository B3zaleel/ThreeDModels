using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public static class Utf8JsonReaderHelpers
{
    public static string? ReadString(ref Utf8JsonReader jsonReader)
    {
        if (jsonReader.TokenType == JsonTokenType.PropertyName && !jsonReader.Read())
        {
            return null;
        }
        return jsonReader.TokenType == JsonTokenType.String ? jsonReader.GetString() : null;
    }

    public static bool? ReadBoolean(ref Utf8JsonReader jsonReader)
    {
        if (jsonReader.TokenType == JsonTokenType.PropertyName && !jsonReader.Read())
        {
            return null;
        }
        return jsonReader.TokenType == JsonTokenType.True || jsonReader.TokenType == JsonTokenType.False
            ? jsonReader.GetBoolean()
            : null;
    }

    public static float? ReadFloat(ref Utf8JsonReader jsonReader)
    {
        if (jsonReader.TokenType == JsonTokenType.PropertyName && !jsonReader.Read())
        {
            return null;
        }
        return jsonReader.TokenType == JsonTokenType.Number ? jsonReader.GetSingle() : null;
    }

    public static int? ReadInteger(ref Utf8JsonReader jsonReader)
    {
        if (jsonReader.TokenType == JsonTokenType.PropertyName && !jsonReader.Read())
        {
            return null;
        }
        return jsonReader.TokenType == JsonTokenType.Number ? jsonReader.GetInt32() : null;
    }

    public static List<T>? ReadList<T>(ref Utf8JsonReader jsonReader, GltfReaderContext context, JsonTokenType tokenType, ArrayElementReader<T> elementReader)
    {
        jsonReader.Read();
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartArray)
        {
            throw new InvalidDataException($"Failed to find start of array.");
        }
        var list = new List<T>();
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }
            else if (jsonReader.TokenType == JsonTokenType.Comment || jsonReader.TokenType == JsonTokenType.Null)
            {
                continue;
            }
            else if (jsonReader.TokenType == tokenType)
            {
                list.Add(elementReader(ref jsonReader, context));
                continue;
            }
            else
            {
                throw new InvalidDataException($"Unexpected token: {jsonReader.TokenType}");
            }
        }
        return list;
    }

    public static List<string>? ReadStringList(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        return ReadList(ref jsonReader, context, JsonTokenType.String, (ref Utf8JsonReader reader, GltfReaderContext _) => ReadString(ref reader)!);
    }

    public static List<int>? ReadIntegerList(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        return ReadList(ref jsonReader, context, JsonTokenType.Number, (ref Utf8JsonReader reader, GltfReaderContext _) => (int)ReadInteger(ref reader)!);
    }

    public static List<float>? ReadFloatList(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        return ReadList(ref jsonReader, context, JsonTokenType.Number, (ref Utf8JsonReader reader, GltfReaderContext _) => (float)ReadFloat(ref reader)!);
    }

    public static List<T>? ReadObjectList<T>(ref Utf8JsonReader jsonReader, GltfReaderContext context, ArrayElementReader<T> elementReader)
    {
        return ReadList(ref jsonReader, context, JsonTokenType.StartObject, elementReader);
    }
}
