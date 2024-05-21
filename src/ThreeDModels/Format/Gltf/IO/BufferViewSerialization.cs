using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class BufferViewSerialization
{
    public static BufferView? Read(GltfReaderContext context)
    {
        int? buffer = null;
        int? byteOffset = null;
        int? byteLength = null;
        int? byteStride = null;
        int? target = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.Buffer)))
            {
                buffer = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.ByteOffset)))
            {
                byteOffset = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.ByteLength)))
            {
                byteLength = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.ByteStride)))
            {
                byteStride = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.Target)))
            {
                target = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<BufferView>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(BufferView.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
