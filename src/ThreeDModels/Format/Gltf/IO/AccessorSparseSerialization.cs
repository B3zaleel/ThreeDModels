using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AccessorSparseSerialization
{
    public static AccessorSparse? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? count = null;
        AccessorSparseIndices? indices = null;
        AccessorSparseValues? values = null;
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
            if (propertyName == nameof(count))
            {
                count = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(indices))
            {
                indices = AccessorSparseIndicesSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(values))
            {
                values = AccessorSparseValuesSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparse>(ref jsonReader, context);
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
        if (count == null || indices == null || values == null)
        {
            throw new InvalidDataException("AccessorSparse.count, AccessorSparse.indices, and AccessorSparse.values are required properties.");
        }
        if (count < 1)
        {
            throw new InvalidDataException("AccessorSparse.count must be greater than or equal to 1.");
        }
        return new()
        {
            Count = (int)count!,
            Indices = indices!,
            Values = values!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AccessorSparse? accessorSparse)
    {
        if (accessorSparse == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.Accessor.Count, accessorSparse.Count);
        jsonWriter.WritePropertyName(ElementName.AccessorSparse.Indices);
        AccessorSparseIndicesSerialization.Write(ref jsonWriter, context, accessorSparse.Indices);
        jsonWriter.WritePropertyName(ElementName.AccessorSparse.Values);
        AccessorSparseValuesSerialization.Write(ref jsonWriter, context, accessorSparse.Values);
        if (accessorSparse.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AccessorSparse>(ref jsonWriter, context, accessorSparse.Extensions);
        }
        if (accessorSparse.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, accessorSparse.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
