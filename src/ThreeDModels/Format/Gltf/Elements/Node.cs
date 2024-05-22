using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a node in the node hierarchy.
/// </summary>
public class Node : IGltfRootProperty
{
    /// <summary>
    /// The index of the camera referenced by this node.
    /// </summary>
    public int? Camera { get; set; }
    /// <summary>
    /// The indices of this node's children.
    /// </summary>
    public List<int>? Children { get; set; }
    /// <summary>
    /// The index of the skin referenced by this node.
    /// </summary>
    public int? Skin { get; set; }
    /// <summary>
    /// A floating-point 4x4 transformation matrix stored in column-major order.
    /// </summary>
    public float[]? Matrix { get; set; } = Default.Node_Matrix;
    /// <summary>
    /// The index of the mesh in this node.
    /// </summary>
    public int? Mesh { get; set; }
    public float[]? Rotation { get; set; } = Default.Node_Rotation;
    /// <summary>
    /// The node's non-uniform scale, given as the scaling factors along the x, y, and z axes.
    /// </summary>
    public float[]? Scale { get; set; } = Default.Node_Scale;
    /// <summary>
    /// The node's translation along the x, y, and z axes.
    /// </summary>
    public float[]? Translation { get; set; } = Default.Node_Translation;
    /// <summary>
    /// The weights of the instantiated morph target.
    /// </summary>
    public List<float>? Weights { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
