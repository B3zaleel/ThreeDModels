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
            if (propertyName == nameof(copyright))
            {
                copyright = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(generator))
            {
                generator = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(version))
            {
                version = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(minVersion))
            {
                minVersion = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Asset>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Asset? asset)
    {
        if (asset == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (asset.Copyright != null)
        {
            jsonWriter.WriteString(ElementName.Asset.Copyright, asset.Copyright);
        }
        if (asset.Generator != null)
        {
            jsonWriter.WriteString(ElementName.Asset.Generator, asset.Generator);
        }
        jsonWriter.WriteString(ElementName.Asset.Version, asset.Version);
        if (asset.MinVersion != null)
        {
            jsonWriter.WriteString(ElementName.Asset.MinVersion, asset.MinVersion);
        }
        if (asset.Extensions != null)
        {
            ExtensionsSerialization.Write<Asset>(ref jsonWriter, context, asset.Extensions);
        }
        if (asset.Extras != null)
        {
            JsonSerialization.Write(ref jsonWriter, context, asset.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
