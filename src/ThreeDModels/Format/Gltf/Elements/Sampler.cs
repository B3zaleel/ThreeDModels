using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents texture sampler properties for filtering and wrapping modes.
/// </summary>
public class Sampler : IGltfRootProperty
{
    /// <summary>
    /// Magnification filter.
    /// </summary>
    public int? MagFilter { get; set; }
    /// <summary>
    /// Minification filter.
    /// </summary>
    public int? MinFilter { get; set; }
    /// <summary>
    /// S (U) wrapping mode.
    /// </summary>
    public int? WrapS { get; set; } = Default.Sampler_WrappingMode;
    /// <summary>
    /// T (V) wrapping mode.
    /// </summary>
    public int? WrapT { get; set; } = Default.Sampler_WrappingMode;
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
