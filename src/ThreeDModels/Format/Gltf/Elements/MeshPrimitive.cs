using ThreeDModels.Format.Gltf.IO;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents geometry to be rendered with the given material.
/// </summary>
public class MeshPrimitive : IGltfProperty
{
    /// <summary>
    /// A plain JSON object, where each key corresponds to a mesh attribute semantic and each value is the index of the accessor containing attribute's data.
    /// </summary>
    public required IntegerMap Attributes { get; set; }
    /// <summary>
    /// The index of the accessor that contains the vertex indices.
    /// </summary>
    public int? Indices { get; set; }
    /// <summary>
    /// The index of the material to apply to this primitive when rendering.
    /// </summary>
    public int? Material { get; set; }
    /// <summary>
    /// The topology type of primitives to render.
    /// </summary>
    public int Mode { get; set; } = Default.MeshPrimitive_Mode;
    /// <summary>
    /// An array of morph targets.
    /// <para/>
    /// Each morph target is a plain JSON object specifying attributes displacements in 
    /// a morph target, where each key corresponds to one of the three supported attribute 
    /// semantic (`POSITION`, `NORMAL`, or `TANGENT`) and each value is the index of the 
    /// accessor containing the attribute displacements' data.
    /// </summary>
    public List<IntegerMap>? Targets { get; set; } = [];
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }
}
