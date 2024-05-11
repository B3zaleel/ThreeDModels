using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Surface : FreeFormGeometry, IObjElement
{
    public required Range<float> UParameterRange { get; set; }
    public required Range<float> VParameterRange { get; set; }
    public required List<VertexReference> VertexReferences { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
