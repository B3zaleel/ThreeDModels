using ThreeDModels.Format.Gltf.Elements;

namespace ThreeDModels.Format.Gltf;

public class Gltf : IGltfProperty
{
    public List<string>? ExtensionsUsed { get; set; }
    public List<string>? ExtensionsRequired { get; set; }
    public List<Accessor>? Accessors { get; set; }
    public List<Animation>? Animations { get; set; }
    public required Asset Asset { get; set; }
    public List<Elements.Buffer>? Buffers { get; set; }
    public List<BufferView>? BufferViews { get; set; }
    public List<Camera>? Cameras { get; set; }
    public List<Image>? Images { get; set; }
    public List<Material>? Materials { get; set; }
    public List<Mesh>? Meshes { get; set; }
    public List<Node>? Nodes { get; set; }
    public List<Sampler>? Samplers { get; set; }
    public int? Scene { get; set; }
    public List<Scene>? Scenes { get; set; }
    public List<Skin>? Skins { get; set; }
    public List<Texture>? Textures { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public object? Extras { get; set; }
    public List<Chunk> AdditionalChunks { get; set; } = [];
}
