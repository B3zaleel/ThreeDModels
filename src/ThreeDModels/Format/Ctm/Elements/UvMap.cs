namespace ThreeDModels.Format.Ctm.Elements;

public class UvMap
{
    public string Name { get; set; } = string.Empty;
    public string Filename { get; set; } = string.Empty;
    public List<UvCoordinate> Coordinates { get; set; } = [];
}
