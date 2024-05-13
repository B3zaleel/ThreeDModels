namespace ThreeDModels.Format.Ctm.Elements;

public class Vertex
{
    public required float X { get; set; }
    public required float Y { get; set; }
    public required float Z { get; set; }
    public Normal? Normal { get; set; }
}
