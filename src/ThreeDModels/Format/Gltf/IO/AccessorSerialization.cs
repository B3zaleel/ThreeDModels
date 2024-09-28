using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AccessorSerialization
{
    public static Accessor? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? bufferView = null;
        int? byteOffset = null;
        int? componentType = null;
        bool? normalized = null;
        int? count = null;
        string? type = null;
        List<float>? max = null;
        List<float>? min = null;
        AccessorSparse? sparse = null;
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
            if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(byteOffset))
            {
                byteOffset = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(componentType))
            {
                componentType = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(normalized))
            {
                normalized = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(count))
            {
                count = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(type))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(max))
            {
                max = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(min))
            {
                min = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(sparse))
            {
                sparse = AccessorSparseSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Accessor>(ref jsonReader, context);
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
        if (componentType == null || count == null || type == null)
        {
            throw new InvalidDataException("Accessor.componentType, Accessor.count, and Accessor.type are required properties.");
        }
        if (bufferView == null && byteOffset != null)
        {
            throw new InvalidDataException("Accessor.byteOffset is only valid when Accessor.bufferView is defined.");
        }
        if (max == null || min == null || max.Count != min.Count)
        {
            throw new InvalidDataException("Accessor.max and Accessor.min are required properties and must have the same number of elements.");
        }
        if (max.Count < Default.Accessor_ValuesRange_Length_Min || max.Count > Default.Accessor_ValuesRange_Length_Max)
        {
            throw new InvalidDataException($"Accessor.max and Accessor.min must have between {Default.Accessor_ValuesRange_Length_Min} and {Default.Accessor_ValuesRange_Length_Max} elements.");
        }

        return new()
        {
            BufferView = bufferView,
            ByteOffset = byteOffset,
            ComponentType = (int)componentType!,
            Normalized = normalized ?? Default.Accessor_Normalized,
            Count = (int)count!,
            Type = type!,
            Max = max!,
            Min = min!,
            Sparse = sparse,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Accessor? accessor)
    {
        if (accessor == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (accessor.BufferView == null && accessor.ByteOffset != null)
        {
            throw new InvalidDataException("Accessor.byteOffset is only valid when Accessor.bufferView is defined.");
        }
        if (accessor.Max.Count != accessor.Min.Count)
        {
            throw new InvalidDataException("Accessor.max and Accessor.min must have the same number of elements.");
        }
        if (accessor.Max.Count < Default.Accessor_ValuesRange_Length_Min || accessor.Max.Count > Default.Accessor_ValuesRange_Length_Max)
        {
            throw new InvalidDataException($"Accessor.max and Accessor.min must have between {Default.Accessor_ValuesRange_Length_Min} and {Default.Accessor_ValuesRange_Length_Max} elements.");
        }
        jsonWriter.WriteStartObject();
        if (accessor.BufferView != null)
        {
            jsonWriter.WriteNumber(ElementName.Accessor.BufferView, (int)accessor.BufferView);
        }
        if (accessor.ByteOffset != null)
        {
            jsonWriter.WriteNumber(ElementName.Accessor.ByteOffset, (int)accessor.ByteOffset);
        }
        jsonWriter.WriteNumber(ElementName.Accessor.ComponentType, accessor.ComponentType);
        jsonWriter.WriteBoolean(ElementName.Accessor.Normalized, accessor.Normalized);
        jsonWriter.WriteNumber(ElementName.Accessor.Count, accessor.Count);
        jsonWriter.WriteString(ElementName.Accessor.Type, accessor.Type);
        jsonWriter.WritePropertyName(ElementName.Accessor.Max);
        WriteFloatList(ref jsonWriter, context, accessor.Max);
        jsonWriter.WritePropertyName(ElementName.Accessor.Min);
        WriteFloatList(ref jsonWriter, context, accessor.Min);
        if (accessor.Sparse != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Sparse);
            AccessorSparseSerialization.Write(ref jsonWriter, context, accessor.Sparse);
        }
        if (accessor.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, accessor.Name);
        }
        if (accessor.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Accessor>(ref jsonWriter, context, accessor.Extensions);
        }
        if (accessor.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, accessor.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
