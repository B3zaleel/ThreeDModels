using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents the material appearance of a primitive.
/// </summary>
public class Material : IGltfRootProperty
{
    /// <summary>
    /// A set of parameter values that are used to define the metallic-roughness material model from Physically Based Rendering (PBR) methodology.
    /// </summary>
    public MaterialPbrMetallicRoughness? PbrMetallicRoughness { get; set; }
    /// <summary>
    /// The tangent space normal texture.
    /// </summary>
    public MaterialNormalTextureInfo? NormalTexture { get; set; }
    /// <summary>
    /// The occlusion texture.
    /// </summary>
    public MaterialOcclusionTextureInfo? OcclusionTexture { get; set; }
    /// <summary>
    /// The emissive texture.
    /// </summary>
    public TextureInfo? EmissiveTexture { get; set; }
    /// <summary>
    /// The factors for the emissive color of the material.
    /// </summary>
    public float[] EmissiveFactor { get; set; } = Default.Material_EmissiveFactor;
    /// <summary>
    /// The alpha rendering mode of the material.
    /// </summary>
    public string? AlphaMode { get; set; } = Default.Material_AlphaMode;
    /// <summary>
    /// The alpha cutoff value of the material when <see cref="AlphaMode"/> == "MASK".
    /// <para/>
    /// If the alpha value is greater than or equal to this value then it is rendered as fully opaque, otherwise, it is rendered as fully transparent.
    /// <para/>
    /// A value greater than `1.0` will render the entire material as fully transparent.
    /// </summary>
    public float? AlphaCutoff { get; set; } = Default.Material_AlphaCutoff;
    /// <summary>
    /// Specifies whether the material is double sided.
    /// <para/>
    /// When this value is false, back-face culling is enabled.
    /// <para/>
    /// When this value is true, back-face culling is disabled and double-sided lighting is enabled.
    /// </summary>
    public bool DoubleSided { get; set; } = false;
    public string? Name { get; set; } = string.Empty;
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
