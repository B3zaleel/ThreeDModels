using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Curve2D : FreeFormGeometry, IObjElement
{
    public required List<int> ParameterVertexIndices { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
