using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.Elements;

public interface IObjElement
{
    public string Name { get; set; }
    public DisplayAttributes? Display { get; set; }
}
