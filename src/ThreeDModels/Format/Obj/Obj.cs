using ThreeDModels.Format.Obj.Display;
using ThreeDModels.Format.Obj.Grouping;
using ThreeDModels.Format.Obj.Vertex;

namespace ThreeDModels.Format.Obj;

public class Obj
{
    public List<GeometricVertex> GeometricVertices { get; } = [];
    public List<TextureVertex> TextureVertices { get; } = [];
    public List<VertexNormal> VertexNormals { get; } = [];
    public List<ParameterSpaceVertex> ParameterSpaceVertices { get; } = [];
    public List<Group> Groups { get; set; } = [];
    public List<List<Material>> MaterialLibraries { get; } = [];
}
