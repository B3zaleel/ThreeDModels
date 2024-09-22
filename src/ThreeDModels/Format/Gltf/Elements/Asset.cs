namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents an object that contains metadata about the glTF asset.
/// </summary>
public class Asset : IGltfProperty
{
    /// <summary>
    /// A copyright message suitable for display to credit the content creator.
    /// </summary>
    public string? Copyright { get; set; } = string.Empty;
    /// <summary>
    /// Tool that generated this glTF model.  Useful for debugging.
    /// </summary>
    public string? Generator { get; set; } = string.Empty;
    /// <summary>
    /// The glTF version in the form of `<major>.<minor>` that this asset targets.
    /// </summary>
    public required string Version { get; set; }
    /// <summary>
    /// The minimum glTF version in the form of `<major>.<minor>` that this asset targets.
    /// </summary>
    public string? MinVersion { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
