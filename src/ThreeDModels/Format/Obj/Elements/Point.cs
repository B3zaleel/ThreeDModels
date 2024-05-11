using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Point : IObjElement
{
    public required List<int> VertexIndices { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
