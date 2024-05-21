namespace ThreeDModels.Format.Gltf.IO;

public interface IGltfExtension
{
    /// <summary>
    /// The name of the extension.
    /// </summary>
    public string Name { get; }
    public object? Read(GltfReaderContext context, Type parentType);
}
