using ThreeDModels.Format.Gltf.Elements;

namespace ThreeDModels.Format.Gltf.IO;

public static class Default
{
    public const byte ByteOffset = 0;
    public const float ExclusiveMinimum = 0.0f;
    public const string InterpolationAlgorithm = "LINEAR";
    public const bool Accessor_Normalized = false;
    public const byte Accessor_ValuesRange_Length_Min = 1;
    public const byte Accessor_ValuesRange_Length_Max = 16;
    public static readonly float[] Material_BaseColorFactor = [1.0f, 1.0f, 1.0f, 1.0f];
    public const float Material_Factor = 1.0f;
    public const bool Material_DoubleSided = false;
    public const float Material_AlphaCutoff = 0.5f;
    public const string Material_AlphaMode = "OPAQUE";
    public const int Material_TexCoord = 0;
    public static readonly float[] Material_EmissiveFactor = [0.0f, 0.0f, 0.0f];
    public static readonly float[] Node_Matrix = [1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f];
    public static readonly float[] Node_Rotation = [0.0f, 0.0f, 0.0f, 1.0f];
    public static readonly float[] Node_Scale = [1.0f, 1.0f, 1.0f];
    public static readonly float[] Node_Translation = [0.0f, 0.0f, 0.0f];
    public const int Sampler_WrappingMode = WrappingMode.REPEAT;
    public const byte MeshPrimitive_Mode = 4;
}
