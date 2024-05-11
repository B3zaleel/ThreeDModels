namespace ThreeDModels.Format.Obj.Display;

public class DisplayAttributes
{
    public bool BevelInterpolationEnabled { get; set; } = false;
    public bool ColorInterpolationEnabled { get; set; } = false;
    public bool DissolveInterpolationEnabled { get; set; } = false;
    public int LevelOfDetail { get; set; } = 0;
    public string MaterialName { get; set; } = string.Empty;
    public string TextureMapName { get; set; } = string.Empty;
}
