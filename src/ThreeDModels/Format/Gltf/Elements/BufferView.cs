using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a subset of a buffer.
/// </summary>
public class BufferView : IGltfRootProperty
{
    /// <summary>
    /// The index of the buffer.
    /// </summary>
    public required int Buffer { get; set; }
    /// <summary>
    /// The offset into the buffer in bytes.
    /// </summary>
    public int ByteOffset { get; set; } = Default.ByteOffset;
    /// <summary>
    /// The length of the bufferView in bytes.
    /// </summary>
    public required int ByteLength { get; set; }
    /// <summary>
    /// The stride, in bytes.
    /// </summary>
    public int? ByteStride { get; set; }
    /// <summary>
    /// The hint representing the intended GPU buffer type to use with this buffer view.
    /// </summary>
    public int? Target { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
