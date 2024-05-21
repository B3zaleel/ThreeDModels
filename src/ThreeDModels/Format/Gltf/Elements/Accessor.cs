namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a typed view into a buffer view that contains raw binary data.
/// </summary>
public class Accessor : IGltfRootProperty
{
    /// <summary>
    /// The index of the bufferView.
    /// </summary>
    public int? BufferView { get; set; }
    /// <summary>
    /// The offset relative to the start of the buffer view in bytes.
    /// </summary>
    public int? ByteOffset { get; set; }
    /// <summary>
    /// The datatype of the accessor's components.
    /// </summary>
    public required int ComponentType { get; set; }
    /// <summary>
    /// Specifies whether integer data values are normalized before usage.
    /// </summary>
    public bool Normalized { get; set; }
    /// <summary>
    /// The number of elements referenced by this accessor.
    /// </summary>
    public required int Count { get; set; }
    /// <summary>
    /// Specifies if the accessor's elements are scalars, vectors, or matrices.
    /// </summary>
    public required string Type { get; set; } = string.Empty;
    /// <summary>
    /// Maximum value of each component in this accessor.
    /// </summary>
    public List<float> Max { get; set; } = [];
    /// <summary>
    /// Minimum value of each component in this accessor.
    /// </summary>
    public List<float> Min { get; set; } = [];
    /// <summary>
    /// Sparse storage of elements that deviate from their initialization value.
    /// </summary>
    public AccessorSparse? Sparse { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
