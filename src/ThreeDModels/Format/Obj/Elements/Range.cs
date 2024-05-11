namespace ThreeDModels.Format.Obj.Elements;

public class Range<T>(T start, T end)
{
    public T Start { get; set; } = start;
    public T End { get; set; } = end;
}
