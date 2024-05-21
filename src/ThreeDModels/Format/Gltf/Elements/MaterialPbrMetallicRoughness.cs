using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents a set of parameter values that are used to define the metallic-roughness material model from Physically-Based Rendering (PBR) methodology.
/// </summary>
public class MaterialPbrMetallicRoughness : IGltfProperty
{
    /// <summary>
    /// The factors for the base color of the material.
    /// </summary>
    public float[] BaseColorFactor { get; set; } = Default.Material_BaseColorFactor;
    /// <summary>
    /// The base color texture.
    /// </summary>
    public TextureInfo? BaseColorTexture { get; set; }
    /// <summary>
    /// The factor for the metalness of the material.
    /// </summary>
    public float? MetallicFactor { get; set; } = Default.Material_Factor;
    /// <summary>
    /// The factor for the roughness of the material.
    /// </summary>
    public float? RoughnessFactor { get; set; } = Default.Material_Factor;
    /// <summary>
    /// The metallic-roughness texture.
    /// </summary>
    public TextureInfo? MetallicRoughnessTexture { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
}
