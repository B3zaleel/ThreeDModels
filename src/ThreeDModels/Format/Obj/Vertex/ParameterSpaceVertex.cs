namespace ThreeDModels.Format.Obj.Vertex;

public class ParameterSpaceVertex
{
    public required float U { get; set; }
    public float V { get; set; }
    public required float W { get; set; } = 1.0f;
}
