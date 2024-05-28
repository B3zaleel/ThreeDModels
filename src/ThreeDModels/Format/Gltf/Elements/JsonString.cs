namespace ThreeDModels.Format.Gltf.Elements;

public class JsonString(JsonElement? parent, string value) : JsonElement(parent)
{
    public string Value { get; set; } = value;
}
