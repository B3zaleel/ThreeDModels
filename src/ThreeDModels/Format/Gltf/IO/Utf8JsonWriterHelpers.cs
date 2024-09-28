using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public static class Utf8JsonWriterHelpers
{
    public static void WriteString(ref Utf8JsonWriter jsonWriter, string? value)
    {
        if (value == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStringValue(value);
    }

    public static void WriteFloat(ref Utf8JsonWriter jsonWriter, float? value)
    {
        if (value == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteNumberValue((float)value);
    }

    public static void WriteInteger(ref Utf8JsonWriter jsonWriter, int? value)
    {
        if (value == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteNumberValue((int)value);
    }

    public static void WriteList<T>(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, List<T>? list, ArrayElementWriter<T> elementWriter)
    {
        if (list == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartArray();
        for (int i = 0; i < list.Count; i++)
        {
            elementWriter(ref jsonWriter, context, list[i]);
        }
        jsonWriter.WriteEndArray();
    }

    public static void WriteStringList(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, List<string>? list)
    {
        WriteList(ref jsonWriter, context, list, (ref Utf8JsonWriter writer, GltfWriterContext _, string value) => WriteString(ref writer, value));
    }

    public static void WriteIntegerList(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, List<int>? list)
    {
        WriteList(ref jsonWriter, context, list, (ref Utf8JsonWriter writer, GltfWriterContext _, int value) => WriteInteger(ref writer, value));
    }

    public static void WriteFloatList(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, List<float>? list)
    {
        WriteList(ref jsonWriter, context, list, (ref Utf8JsonWriter writer, GltfWriterContext _, float value) => WriteFloat(ref writer, value));
    }
}
