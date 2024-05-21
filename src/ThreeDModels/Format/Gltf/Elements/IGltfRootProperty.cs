namespace ThreeDModels.Format.Gltf.Elements;

public interface IGltfRootProperty : IGltfProperty
{
    /// <summary>
    /// The user-defined name of this object.
    /// </summary>
    public string? Name { get; set; }
}
