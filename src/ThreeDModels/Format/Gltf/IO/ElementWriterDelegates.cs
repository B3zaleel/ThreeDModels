using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public delegate void ArrayElementWriter<T>(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, T element);

public delegate void ExtensionElementWriter(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element);
