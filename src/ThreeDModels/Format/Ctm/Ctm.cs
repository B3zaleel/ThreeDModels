using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm;

public class Ctm
{
    public List<Vertex> Vertices { get; set; } = [];
    public List<Triangle> Triangles { get; set; } = [];
    public List<UvMap> UvMaps { get; set; } = [];
    public List<AttributeMap> AttributeMaps { get; set; } = [];
    public required CompressionMethod CompressionMethod { get; set; } = CompressionMethod.RAW;
    public int Flags { get; set; } = 0;
    public string Comment { get; set; } = string.Empty;
}
