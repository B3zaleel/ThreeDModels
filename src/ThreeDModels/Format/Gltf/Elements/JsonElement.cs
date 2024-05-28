namespace ThreeDModels.Format.Gltf.Elements;

public abstract class JsonElement(JsonElement? parent)
{
    public JsonElement? Parent { get; } = parent;
}
