using ThreeDModels.Format.Obj.Display;
using ThreeDModels.Format.Obj.Elements;
using ThreeDModels.Format.Obj.Grouping;

namespace ThreeDModels.Format.Obj.IO;

internal class ObjReaderContext
{
    public Group Group { get; set; }
    public string ElementName { get; set; }
    public DisplayAttributes? Display { get; set; }
    public FreeFormGeometry? FreeFormGeometry { get; set; }

    public ObjReaderContext(Group group, string elementName, DisplayAttributes? display, FreeFormGeometry? freeFormGeometry)
    {
        Group = group;
        ElementName = elementName;
        Display = display;
        FreeFormGeometry = freeFormGeometry;
    }
}
