namespace ThreeDModels.Format.Gltf.IO;

public delegate T ArrayElementReader<T>(GltfReaderContext context);

public delegate object? ExtensionElementReader(GltfReaderContext context, Type parentType);
