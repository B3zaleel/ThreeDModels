namespace ThreeDModels.Format.Gltf.Elements;

public class Chunk
{
    public uint Type { get; set; }
    public uint Length { get; set; }
    public required byte[] Data { get; set; }
}
