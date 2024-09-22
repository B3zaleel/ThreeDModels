namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents the root nodes of a scene.
/// </summary>
public class Scene : IGltfRootProperty
{
    /// <summary>
    /// The indices of each root node.
    /// </summary>
    public List<int>? Nodes { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
