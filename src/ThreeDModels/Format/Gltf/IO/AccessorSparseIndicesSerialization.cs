using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AccessorSparseIndicesSerialization
{
    public static AccessorSparseIndices? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? bufferView = null;
        int? byteOffset = 0;
        int? componentType = 0;
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
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparseIndices>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AccessorSparseIndices? accessorSparseIndices)
    {
        if (accessorSparseIndices == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.Accessor.BufferView, accessorSparseIndices.BufferView);
        jsonWriter.WriteNumber(ElementName.Accessor.ByteOffset, accessorSparseIndices.ByteOffset);
        jsonWriter.WriteNumber(ElementName.Accessor.ComponentType, accessorSparseIndices.ComponentType);
        if (accessorSparseIndices.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AccessorSparseIndices>(ref jsonWriter, context, accessorSparseIndices.Extensions);
        }
        if (accessorSparseIndices.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, accessorSparseIndices.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
