namespace ThreeDModels.Format.Obj.Elements;

/// <summary>
/// Represents a free-form curve or surface element.
/// </summary>
public abstract class FreeFormGeometry
{
    /// <summary>
    /// Gets or sets a value indicating whether the curve or surface is rational.
    /// </summary>
    public bool IsRational { get; set; }
    /// <summary>
    /// Gets or sets the type of curve or surface.
    /// </summary>
    public CurveSurfaceType Type { get; set; }
    /// <summary>
    /// Gets or sets the degree of the curve or surface in the U direction.
    /// </summary>
    public int DegreeU { get; set; }
    /// <summary>
    /// Gets or sets the degree of the curve or surface in the V direction.
    /// </summary>
    public int? DegreeV { get; set; }
    /// <summary>
    /// Gets or sets the basis matrices used for basis matrix curve or surface in the `u` direction.
    /// </summary>
    public List<float> BasisMatrixU { get; set; }
    /// <summary>
    /// Gets or sets the basis matrices used for basis matrix curve or surface in the `v` direction.
    /// </summary>
    public List<float> BasisMatrixV { get; set; }
    /// <summary>
    /// Gets or sets the step size of the curve or surface in the U direction.
    /// </summary>
    public int? StepU { get; set; }
    /// <summary>
    /// Gets or sets the step size of the curve or surface in the V direction.
    /// </summary>
    public int? StepV { get; set; }
    public List<float> ParameterU { get; set; }
    public List<float>? ParameterV { get; set; }
    public List<List<Curve2DReference>> TrimmingCurves { get; set; } = [];
    public List<List<Curve2DReference>> Holes { get; set; } = [];
    public List<List<Curve2DReference>> SpecialCurves { get; set; } = [];
    public List<List<int>> SpecialPoints { get; set; } = [];
}
