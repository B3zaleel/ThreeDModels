using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.Grouping;

public class Group
{
    public List<string>? Names { get; set; }
    public List<int> SmoothingGroupIndices { get; set; } = [];
    public List<MergingGroup> MergingGroups { get; set; } = [];
    public List<Point> Points { get; } = [];
    public List<Line> Lines { get; } = [];
    public List<Face> Faces { get; } = [];
    public List<Curve> Curves { get; } = [];
    public List<Curve2D> Curves2D { get; } = [];
    public List<Surface> Surfaces { get; } = [];
}
