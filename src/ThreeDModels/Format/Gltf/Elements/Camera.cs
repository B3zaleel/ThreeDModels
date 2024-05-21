namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a camera's projection.
/// </summary>
public class Camera : IGltfRootProperty
{
    /// <summary>
    /// An orthographic camera containing properties to create an orthographic projection matrix.
    /// </summary>
    public CameraOrthographic? Orthographic { get; set; }
    /// <summary>
    /// A perspective camera containing properties to create a perspective projection matrix.
    /// </summary>
    public CameraPerspective? Perspective { get; set; }
    /// <summary>
    /// Specifies if the camera uses a perspective or orthographic projection.
    /// </summary>
    public required string Type { get; set; }
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
