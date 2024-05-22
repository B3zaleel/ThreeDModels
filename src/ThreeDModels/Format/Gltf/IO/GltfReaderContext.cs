using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public class GltfReaderContext
{
    internal readonly Dictionary<string, ExtensionElementReader> Extensions;

    internal GltfReaderContext(Dictionary<string, ExtensionElementReader> extensions)
    {
        Extensions = extensions;
    }
}
