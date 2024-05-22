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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparse.Count)))
            {
                count = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparse.Indices)))
            {
                indices = AccessorSparseIndicesSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparse.Values)))
            {
                values = AccessorSparseValuesSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparse.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<AccessorSparse>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(AccessorSparse.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
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
}
