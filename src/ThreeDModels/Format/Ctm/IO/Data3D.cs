namespace ThreeDModels.Format.Ctm.IO;

/// <summary>
/// Represents data about a 3D-axial reference.
/// </summary>
/// <param name="X"></param>
/// <param name="Y"></param>
/// <param name="Z"></param>
/// <typeparam name="T"></typeparam>
/// <returns></returns>
internal record Data3D<T>(T X, T Y, T Z);
