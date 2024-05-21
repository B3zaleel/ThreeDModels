namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a reference to a texture.
/// </summary>
public class TextureInfo : IGltfProperty
{
    /// <summary>
    /// The index of the texture.
    /// </summary>
    public required int Index { get; set; }
    /// <summary>
    /// The set index of texture's TEXCOORD attribute used for texture coordinate mapping.
    /// </summary>
    public int? TexCoord { get; set; } = 0;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
