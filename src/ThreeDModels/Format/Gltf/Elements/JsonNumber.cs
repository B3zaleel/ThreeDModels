namespace ThreeDModels.Format.Gltf.Elements;

public class JsonNumber(JsonElement? parent, double value) : JsonElement(parent)
{
    public double Value { get; set; } = value;
}
