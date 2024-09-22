namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents data that points to binary geometry, animation, or skins.
/// </summary>
public class Buffer : IGltfRootProperty
{
    /// <summary>
    /// The URI (or IRI) of the buffer.
    /// </summary>
    public string? Uri { get; set; } = string.Empty;
    /// <summary>
    /// The length of the buffer in bytes.
    /// </summary>
    public required int ByteLength { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
