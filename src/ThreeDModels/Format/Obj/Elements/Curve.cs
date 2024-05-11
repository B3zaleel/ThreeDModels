using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Curve : FreeFormGeometry, IObjElement
{
    public required Range<float> ParameterRange { get; set; }
    public required List<int> ControlVertexIndices { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
