namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a keyframe animation.
/// </summary>
public class Animation : IGltfRootProperty
{
    /// <summary>
    /// An array of animation channels.
    /// </summary>
    public required List<AnimationChannel> Channels { get; set; } = [];
    /// <summary>
    /// An array of animation samplers.
    /// </summary>
    public required List<AnimationSampler> Samplers { get; set; } = [];
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
