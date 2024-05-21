using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AccessorSparseIndicesSerialization
{
    public static AccessorSparseIndices? Read(GltfReaderContext context)
    {
        int? bufferView = null;
        int? byteOffset = 0;
        int? componentType = 0;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparseIndices.BufferView)))
            {
                bufferView = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparseIndices.ByteOffset)))
            {
                byteOffset = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparseIndices.ComponentType)))
            {
                componentType = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparseIndices.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparseIndices>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparseIndices.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (bufferView == null || componentType == null)
        {
            throw new InvalidDataException("AccessorSparseIndices.bufferView and AccessorSparseIndices.componentType are required properties.");
        }
        return new()
        {
            BufferView = (int)bufferView!,
            ByteOffset = byteOffset ?? Default.ByteOffset,
            ComponentType = (int)componentType!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
