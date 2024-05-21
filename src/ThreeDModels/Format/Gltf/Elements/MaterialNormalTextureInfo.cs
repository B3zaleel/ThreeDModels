namespace ThreeDModels.Format.Gltf.Elements;

public class MaterialNormalTextureInfo : TextureInfo
{
    /// <summary>
    /// The scalar parameter applied to each normal vector of the normal texture.
    /// </summary>
    public float Scale { get; set; } = 1.0f;
}
