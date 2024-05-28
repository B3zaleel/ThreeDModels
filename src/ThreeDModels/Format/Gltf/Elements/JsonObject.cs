namespace ThreeDModels.Format.Gltf.Elements;

public class JsonObject(JsonElement? parent, Dictionary<string, JsonElement> value) : JsonElement(parent)
{
    public Dictionary<string, JsonElement> Value { get; set; } = value;
}
