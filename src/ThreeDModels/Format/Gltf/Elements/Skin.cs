namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents joints and matrices defining a skin.
/// </summary>
public class Skin : IGltfRootProperty
{
    /// <summary>
    /// The index of the accessor containing the floating-point 4x4 inverse-bind matrices.
    /// </summary>
    public int? InverseBindMatrices { get; set; } = 0;
    /// <summary>
    /// The index of the node used as a skeleton root.
    /// </summary>
    public int? Skeleton { get; set; } = 0;
    /// <summary>
    /// Indices of skeleton nodes, used as joints in this skin.
    /// </summary>
    public required List<int> Joints { get; set; } = [];
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
