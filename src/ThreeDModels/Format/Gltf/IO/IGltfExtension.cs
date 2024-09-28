using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public interface IGltfExtension
{
    /// <summary>
    /// The name of the extension.
    /// </summary>
    public string Name { get; }
    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType);
    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element);
}
