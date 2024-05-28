namespace ThreeDModels.Format.Gltf.Elements;

public class JsonBoolean(JsonElement? parent, bool value) : JsonElement(parent)
{
    public bool Value { get; set; } = value;
}
