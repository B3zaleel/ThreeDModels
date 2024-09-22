using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class BufferViewSerialization
{
    public static BufferView? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? buffer = null;
        int? byteOffset = null;
        int? byteLength = null;
        int? byteStride = null;
        int? target = null;
        string? name = null;
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
            if (propertyName == nameof(buffer))
            {
                buffer = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(byteOffset))
            {
                byteOffset = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(byteLength))
            {
                byteLength = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(byteStride))
            {
                byteStride = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(target))
            {
                target = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<BufferView>(ref jsonReader, context);
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
        if (buffer is null || byteLength is null)
        {
            throw new InvalidDataException("BufferView.buffer and BufferView.byteLength are required properties.");
        }
        return new()
        {
            Buffer = (int)buffer!,
            ByteOffset = byteOffset ?? Default.ByteOffset,
            ByteLength = (int)byteLength!,
            ByteStride = byteStride,
            Target = target,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
