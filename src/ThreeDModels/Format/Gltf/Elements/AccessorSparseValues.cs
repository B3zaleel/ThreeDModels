namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents an object pointing to a buffer view containing the deviating accessor values.
/// </summary>
public class AccessorSparseValues : IGltfProperty
{
    /// <summary>
    /// The index of the bufferView with sparse values.
    /// </summary>
    public required int BufferView { get; set; }
    /// <summary>
    /// The offset relative to the start of the bufferView in bytes.
    /// </summary>
    public int ByteOffset { get; set; } = 0;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
