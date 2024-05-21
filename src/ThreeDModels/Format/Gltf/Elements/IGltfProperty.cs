namespace ThreeDModels.Format.Gltf.Elements;

public interface IGltfProperty
{
    /// <summary>
    /// A collection of extension-specific objects.
    /// </summary>
    public Dictionary<string, object?>? Extensions { get; set; }
    /// <summary>
    /// Application-specific object.
    /// </summary>
    public object? Extras { get; set; }
}
