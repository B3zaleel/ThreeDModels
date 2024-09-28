using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AccessorSparseValuesSerialization
{
    public static AccessorSparseValues? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? bufferView = null;
        int? byteOffset = null;
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
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparseValues>(ref jsonReader, context);
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
        if (bufferView is null)
        {
            throw new InvalidDataException("AccessorSparseValues.bufferView is a required property.");
        }
        return new()
        {
            BufferView = (int)bufferView!,
            ByteOffset = byteOffset ?? Default.ByteOffset,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AccessorSparseValues? accessorSparseValues)
    {
        if (accessorSparseValues == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.Accessor.BufferView, accessorSparseValues.BufferView);
        jsonWriter.WriteNumber(ElementName.Accessor.ByteOffset, accessorSparseValues.ByteOffset);
        if (accessorSparseValues.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AccessorSparseValues>(ref jsonWriter, context, accessorSparseValues.Extensions);
        }
        if (accessorSparseValues.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, accessorSparseValues.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
