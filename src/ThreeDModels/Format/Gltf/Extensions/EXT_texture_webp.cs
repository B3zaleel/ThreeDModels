using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies textures using the WebP image format.
/// </summary>
public class EXT_texture_webp : IGltfProperty
{
    /// <summary>
    /// The index of the images node which points to a WebP image.
    /// </summary>
    public int? Source { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtTextureWebpExtension : IGltfExtension
{
    public string Name => nameof(EXT_texture_webp);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("EXT_texture_webp must be used in a MeshPrimitive.");
        }
        int? source = null;
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
            if (propertyName == nameof(source))
            {
                source = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<EXT_texture_webp>(ref jsonReader, context);
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
        return new EXT_texture_webp()
        {
            Source = source,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType != typeof(MeshPrimitive))
        {
            throw new InvalidDataException("EXT_texture_webp must be used in a MeshPrimitive.");
        }
        var extTextureWebp = (EXT_texture_webp?)element;
        if (extTextureWebp == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (extTextureWebp.Source != null)
        {
            jsonWriter.WritePropertyName(ElementName.Texture.Source);
            jsonWriter.WriteNumberValue((int)extTextureWebp.Source);
        }
        if (extTextureWebp.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<EXT_texture_webp>(ref jsonWriter, context, extTextureWebp.Extensions);
        }
        if (extTextureWebp.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, extTextureWebp.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
