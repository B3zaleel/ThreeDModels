namespace ThreeDModels.Format.Ctm.Elements;

public class AttributeMap
{
    public required string Name { get; set; }
    public required List<AttributeValue> Values { get; set; } = [];
}
