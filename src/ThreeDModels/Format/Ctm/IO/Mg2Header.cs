namespace ThreeDModels.Format.Ctm.IO;

internal record Mg2Header(float VertexPrecision, float NormalPrecision, Data3D<float> LowerBoundCoordinates, Data3D<float> UpperBoundCoordinates, Data3D<int> GridDivisions);
