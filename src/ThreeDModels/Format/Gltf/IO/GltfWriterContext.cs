namespace ThreeDModels.Format.Gltf.IO;

public class GltfWriterContext
{
    internal readonly Dictionary<string, ExtensionElementWriter> Extensions;

    internal GltfWriterContext(Dictionary<string, ExtensionElementWriter> extensions)
    {
        Extensions = extensions;
    }
}
