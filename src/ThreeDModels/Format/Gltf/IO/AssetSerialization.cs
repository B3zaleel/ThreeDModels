using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AssetSerialization
{
    public static Asset? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? copyright = null;
        string? generator = null;
        string? version = null;
        string? minVersion = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Copyright)))
            {
                copyright = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Generator)))
            {
                generator = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Version)))
            {
                version = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.MinVersion)))
            {
                minVersion = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Asset>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Asset.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new()
        {
            Copyright = copyright,
            Generator = generator,
            Version = version!,
            MinVersion = minVersion,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
