namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents an image data used to create a texture.
/// </summary>
public class Image : IGltfRootProperty
{
    /// <summary>
    /// The URI (or IRI) of the image.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// The image's media type.
    /// </summary>
    public string? MimeType { get; set; } = string.Empty;
    /// <summary>
    /// The index of the <see cref="Elements.BufferView"/> that contains the image.
    /// </summary>
    public int? BufferView { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
