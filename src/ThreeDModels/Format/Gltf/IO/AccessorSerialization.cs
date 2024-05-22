using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

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
        object? extras = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.BufferView)))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.ByteOffset)))
            {
                byteOffset = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.ComponentType)))
            {
                componentType = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Normalized)))
            {
                normalized = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Count)))
            {
                count = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Type)))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Max)))
            {
                max = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Min)))
            {
                min = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Sparse)))
            {
                sparse = AccessorSparseSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Name)))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Accessor>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Accessor.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
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
}
