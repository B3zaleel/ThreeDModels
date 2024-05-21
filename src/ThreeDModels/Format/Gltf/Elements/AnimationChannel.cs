namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a combination of an animation sampler with a target property being animated.
/// </summary>
public class AnimationChannel : IGltfProperty
{
    /// <summary>
    /// The index of a sampler in this animation used to compute the value for the target.
    /// </summary>
    public required int Sampler { get; set; }
    /// <summary>
    /// The descriptor of the animated property.
    /// </summary>
    public required AnimationChannelTarget Target { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
