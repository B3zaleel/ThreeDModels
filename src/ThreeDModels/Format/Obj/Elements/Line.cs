using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Line : IObjElement
{
    public required List<VertexReference> VertexIndices { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
