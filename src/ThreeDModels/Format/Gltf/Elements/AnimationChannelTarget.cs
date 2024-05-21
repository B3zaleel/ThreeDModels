namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents the descriptor of the animated property.
/// </summary>
public class AnimationChannelTarget : IGltfProperty
{
    /// <summary>
    /// The index of the node to animate.
    /// </summary>
    public int? Node { get; set; }
    /// <summary>
    /// The name of the node's TRS property to animate, or the `weights` of the Morph Targets it instantiates. 
    /// <para/>
    /// For the `translation` property, the values that are provided by the sampler are the translation along the X, Y, and Z axes.
    /// <para/>
    /// For the `rotation` property, the values are a quaternion in the order (x, y, z, w), where w is the scalar. 
    /// <para/>
    /// For the `scale` property, the values are the scaling factors along the X, Y, and Z axes.
    /// </summary>
    public required string Path { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
