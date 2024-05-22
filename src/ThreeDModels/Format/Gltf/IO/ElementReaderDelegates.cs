using System.Text.Json;

namespace ThreeDModels.Format.Gltf.IO;

public delegate T ArrayElementReader<T>(ref Utf8JsonReader jsonReader, GltfReaderContext context);

public delegate object? ExtensionElementReader(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType);
