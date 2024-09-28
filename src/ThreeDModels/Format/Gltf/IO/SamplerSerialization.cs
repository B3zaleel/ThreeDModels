using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SamplerSerialization
{
    public static Sampler? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? magFilter = null;
        int? minFilter = null;
        int? wrapS = null;
        int? wrapT = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        Elements.JsonElement? extras = null;
        jsonReader.Read();
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException($"Failed to find {nameof(Asset)} property");
        }
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = jsonReader.GetString();
            if (propertyName == nameof(magFilter))
            {
                magFilter = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(minFilter))
            {
                minFilter = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(wrapS))
            {
                wrapS = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(wrapT))
            {
                wrapT = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Sampler>(ref jsonReader, context);
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
            MagFilter = magFilter,
            MinFilter = minFilter,
            WrapS = wrapS ?? Default.Sampler_WrappingMode,
            WrapT = wrapT ?? Default.Sampler_WrappingMode,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Sampler? sampler)
    {
        if (sampler == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (sampler.MagFilter != null)
        {
            jsonWriter.WriteNumber(ElementName.Sampler.MagFilter, sampler.MagFilter.Value);
        }
        if (sampler.MinFilter != null)
        {
            jsonWriter.WriteNumber(ElementName.Sampler.MinFilter, sampler.MinFilter.Value);
        }
        if (sampler.WrapS != null)
        {
            jsonWriter.WriteNumber(ElementName.Sampler.WrapS, (int)sampler.WrapS);
        }
        if (sampler.WrapT != null)
        {
            jsonWriter.WriteNumber(ElementName.Sampler.WrapT, (int)sampler.WrapT);
        }
        jsonWriter.WriteString(ElementName.Accessor.Name, sampler.Name);
        if (sampler.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Sampler>(ref jsonWriter, context, sampler.Extensions);
        }
        if (sampler.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, sampler.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
