namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents an object pointing to a buffer view containing the indices of deviating accessor values.
/// </summary>
public class AccessorSparseIndices : IGltfProperty
{
    /// <summary>
    /// The index of the buffer view with sparse indices.
    /// </summary>
    public required int BufferView { get; set; }
    /// <summary>
    /// The offset relative to the start of the buffer view in bytes.
    /// </summary>
    public int ByteOffset { get; set; } = 0;
    /// <summary>
    /// The indices data type.
    /// </summary>
    public required int ComponentType { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
