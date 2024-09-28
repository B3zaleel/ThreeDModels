namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a texture and its sampler.
/// </summary>
public class Texture : IGltfRootProperty
{
    /// <summary>
    /// The index of the <see cref="Sampler"/> used by this texture.
    /// </summary>
    public int? Sampler { get; set; }
    /// <summary>
    /// The index of the <see cref="Image"/> used by this texture.
    /// </summary>
    public int? Source { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
