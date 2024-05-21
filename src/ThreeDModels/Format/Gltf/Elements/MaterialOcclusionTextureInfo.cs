namespace ThreeDModels.Format.Gltf.Elements;

public class MaterialOcclusionTextureInfo : TextureInfo
{
    /// <summary>
    /// A scalar multiplier controlling the amount of occlusion applied.
    /// </summary>
    public float Strength { get; set; } = 1.0f;
}
