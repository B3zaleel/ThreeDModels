using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class ExtMeshoptCompressionBuffer : IGltfProperty
{
    /// <summary>
    /// indicate that the buffer is only referenced by bufferViews that have EXT_meshopt_compression extension and as such doesn't need to be loaded.
    /// </summary>
    public bool Fallback { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class ExtMeshoptCompressionBufferView : IGltfProperty
{
    /// <summary>
    /// The index of the buffer with compressed data.
    /// </summary>
    public required int Buffer { get; set; }
    /// <summary>
    /// The offset into the buffer in bytes.
    /// </summary>
    public required int ByteOffset { get; set; }
    /// <summary>
    /// The length of the compressed data in bytes.
    /// </summary>
    public required int ByteLength { get; set; }
    /// <summary>
    /// The stride, in bytes.
    /// </summary>
    public required short ByteStride { get; set; }
    /// <summary>
    /// The number of elements.
    /// </summary>
    public required int Count { get; set; }
    /// <summary>
    /// The compression mode.
    /// </summary>
    public required string Mode { get; set; }
    /// <summary>
    /// The compression filter.
    /// </summary>
    public required string Filter { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class EXT_meshopt_compressionExtension : IGltfExtension
{
    public string Name => "EXT_meshopt_compression";
    public const bool Default_Fallback = false;
    public const int Default_ByteOffset = 0;
    public static readonly string[] Modes = ["ATTRIBUTES", "TRIANGLES", "INDICES"];
    public static readonly string[] Filters = ["NONE", "OCTAHEDRAL", "QUATERNION", "EXPONENTIAL"];
    public const string Default_Filter = "NONE";

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Elements.Buffer))
        {
            bool? fallback = null;
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
                if (propertyName == nameof(fallback))
                {
                    fallback = ReadBoolean(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<ExtMeshoptCompressionBuffer>(ref jsonReader, context);
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
            return new ExtMeshoptCompressionBuffer()
            {
                Fallback = fallback ?? Default_Fallback,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(BufferView))
        {
            int? buffer = null;
            int? byteOffset = null;
            int? byteLength = null;
            int? byteStride = null;
            int? count = null;
            string? mode = null;
            string? filter = null;
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
                else if (propertyName == nameof(count))
                {
                    count = ReadInteger(ref jsonReader);
                }
                else if (propertyName == nameof(mode))
                {
                    mode = ReadString(ref jsonReader);
                }
                else if (propertyName == nameof(filter))
                {
                    filter = ReadString(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<ExtMeshoptCompressionBufferView>(ref jsonReader, context);
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
            if (buffer == null || byteLength == null || byteStride == null || count == null || mode == null)
            {
                throw new InvalidDataException("bufferView.EXT_meshopt_compression.buffer, EXT_meshopt_compression.byteLength, EXT_meshopt_compression.byteStride, EXT_meshopt_compression.count, and EXT_meshopt_compression.mode are required properties.");
            }
            if (!Modes.Contains(mode))
            {
                throw new InvalidDataException($"bufferView.EXT_meshopt_compression.mode must be one of: [{string.Join(", ", Modes)}].");
            }
            if (filter != null && !Filters.Contains(filter))
            {
                throw new InvalidDataException($"bufferView.EXT_meshopt_compression.filter must be one of: [{string.Join(", ", Filters)}].");
            }
            return new ExtMeshoptCompressionBufferView()
            {
                Buffer = (int)buffer,
                ByteOffset = byteOffset ?? Default_ByteOffset,
                ByteLength = (int)byteLength,
                ByteStride = (short)byteStride,
                Count = (int)count,
                Mode = mode,
                Filter = filter ?? Default_Filter,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("EXT_meshopt_compression must be used in either a Buffer or a BufferView.");
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        throw new NotImplementedException(/* TODO: Implement this*/);
    }
}
