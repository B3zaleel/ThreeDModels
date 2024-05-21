using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public readonly ref struct GltfReaderContext(Utf8JsonReader jsonReader, Dictionary<string, ExtensionElementReader> extensions)
{
    internal readonly Dictionary<string, ExtensionElementReader> Extensions = extensions;
    public readonly Utf8JsonReader JsonReader = jsonReader;
}
