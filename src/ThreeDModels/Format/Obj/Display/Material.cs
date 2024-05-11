namespace ThreeDModels.Format.Obj.Display;

public class Material
{
    public required string Name { get; set; }
    public Color? Ambient { get; set; }
    public Color? Diffuse { get; set; }
    public Color? Specular { get; set; }
    public float SpecularExponent { get; set; } = 0.0f;
    public float Transparency { get; set; } = 0.0f;
    public byte IlluminationModel { get; set; } = 1;
}
