namespace ThreeDModels.Format.Gltf.Elements;

public class JsonArray(JsonElement? parent, List<JsonElement> value) : JsonElement(parent)
{
    public List<JsonElement> Value { get; set; } = value;
}
