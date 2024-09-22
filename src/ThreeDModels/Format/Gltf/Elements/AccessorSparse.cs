namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a sparse storage of accessor values that deviate from their initialization value.
/// </summary>
public class AccessorSparse : IGltfProperty
{
    /// <summary>
    /// Number of deviating accessor values stored in the sparse array.
    /// </summary>
    public required int Count { get; set; }
    /// <summary>
    /// An object pointing to a buffer view containing the indices of deviating accessor values.
    /// </summary>
    public required AccessorSparseIndices Indices { get; set; }
    /// <summary>
    /// An object pointing to a buffer view containing the deviating accessor values.
    /// </summary>
    public required AccessorSparseValues Values { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
