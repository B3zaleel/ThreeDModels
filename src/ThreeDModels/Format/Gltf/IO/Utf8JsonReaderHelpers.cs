using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public static class Utf8JsonReaderHelpers
{
    public static string? ReadString(GltfReaderContext context)
    {
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        return context.JsonReader.TokenType == JsonTokenType.String ? context.JsonReader.GetString() : null;
    }

    public static bool? ReadBoolean(GltfReaderContext context)
    {
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        return context.JsonReader.TokenType == JsonTokenType.True || context.JsonReader.TokenType == JsonTokenType.False
            ? context.JsonReader.GetBoolean()
            : null;
    }

    public static float? ReadFloat(GltfReaderContext context)
    {
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        return context.JsonReader.TokenType == JsonTokenType.Number ? context.JsonReader.GetSingle() : null;
    }

    public static int? ReadInteger(GltfReaderContext context)
    {
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        return context.JsonReader.TokenType == JsonTokenType.Number ? context.JsonReader.GetInt32() : null;
    }

    public static List<T>? ReadList<T>(GltfReaderContext context, JsonTokenType tokenType, ArrayElementReader<T> elementReader)
    {
        context.JsonReader.Read();
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartArray)
        {
            throw new InvalidDataException($"Failed to find start of array.");
        }
        var list = new List<T>();
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }
            else if (context.JsonReader.TokenType == JsonTokenType.Comment || context.JsonReader.TokenType == JsonTokenType.Null)
            {
                continue;
            }
            else if (context.JsonReader.TokenType == tokenType)
            {
                list.Add(elementReader(context));
                continue;
            }
            else
            {
                throw new InvalidDataException($"Unexpected token: {context.JsonReader.TokenType}");
            }
        }
        return list;
    }

    public static List<string>? ReadStringList(GltfReaderContext context)
    {
        return ReadList(context, JsonTokenType.String, reader => ReadString(reader)!);
    }

    public static List<int>? ReadIntegerList(GltfReaderContext context)
    {
        return ReadList(context, JsonTokenType.Number, reader => (int)ReadInteger(reader)!);
    }

    public static List<float>? ReadFloatList(GltfReaderContext context)
    {
        return ReadList(context, JsonTokenType.Number, reader => (float)ReadFloat(reader)!);
    }
}
