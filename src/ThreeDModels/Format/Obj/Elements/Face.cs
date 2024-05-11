using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public class Face : IObjElement
{
    public required List<VertexReference> VertexReferences { get; set; }
    public string Name { get; set; } = string.Empty;
    public DisplayAttributes? Display { get; set; }
}
