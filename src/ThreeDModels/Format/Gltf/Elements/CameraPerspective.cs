namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a perspective camera containing properties to create a perspective projection matrix.
/// </summary>
public class CameraPerspective : IGltfProperty
{
    /// <summary>
    /// The floating-point aspect ratio of the field of view.
    /// </summary>
    public float? AspectRatio { get; set; }
    /// <summary>
    /// The floating-point vertical field of view in radians.
    /// </summary>
    public required float YFov { get; set; }
    /// <summary>
    /// The floating-point distance to the far clipping plane.
    /// </summary>
    public float? ZFar { get; set; }
    /// <summary>
    /// The floating-point distance to the near clipping plane.
    /// </summary>
    public required float ZNear { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
