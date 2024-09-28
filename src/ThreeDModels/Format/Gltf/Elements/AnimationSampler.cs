using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a combination of timestamps with a sequence of output values and defines an interpolation algorithm.
/// </summary>
public class AnimationSampler : IGltfProperty
{
    /// <summary>
    /// The index of an accessor containing keyframe timestamps.
    /// </summary>
    public required int Input { get; set; }
    /// <summary>
    /// Interpolation algorithm.
    /// </summary>
    public string Interpolation { get; set; } = Default.InterpolationAlgorithm;
    /// <summary>
    /// The index of an accessor, containing keyframe output values.
    /// </summary>
    public required int Output { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
