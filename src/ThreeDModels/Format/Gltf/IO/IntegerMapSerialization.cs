using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

public static class IntegerMapSerialization
{
    public static IntegerMap? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        IntegerMap integerMap = [];
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
            if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<IntegerMap>(ref jsonReader, context);
            }
            else if (propertyName == nameof(extras))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                integerMap.Add(propertyName!, (int)ReadInteger(ref jsonReader)!);
            }
        }
        integerMap.Extensions = extensions;
        integerMap.Extras = extras;
        return integerMap;
    }
}
