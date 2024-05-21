namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// An orthographic camera containing properties to create an orthographic projection matrix.
/// </summary>
public class CameraOrthographic : IGltfProperty
{
    /// <summary>
    /// The floating-point horizontal magnification of the view.
    /// </summary>
    public required float XMag { get; set; }
    /// <summary>
    /// The floating-point vertical magnification of the view.
    /// </summary>
    public required float YMag { get; set; }
    /// <summary>
    /// The floating-point distance to the far clipping plane.
    /// </summary>
    public required float ZFar { get; set; }
    /// <summary>
    /// The floating-point distance to the near clipping plane.
    /// </summary>
    public required float ZNear { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
