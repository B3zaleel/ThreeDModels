namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a set of primitives to be rendered.
/// 
/// Its global transform is defined by a node that references it.
/// </summary>
public class Mesh : IGltfRootProperty
{
    /// <summary>
    /// An array of primitives, each defining geometry to be rendered.
    /// </summary>
    public required List<MeshPrimitive> Primitives { get; set; }
    /// <summary>
    /// Array of weights to be applied to the morph targets.
    /// </summary>
    public required List<float> Weights { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
