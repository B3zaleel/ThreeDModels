using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class CurveCommand : ICommand
{
    public static string Name => "curv";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (commandLine.Count == 1)
        {
            throw new InvalidDataException("Curve (curv) must have a starting parameter value");
        }
        if (commandLine.Count == 2)
        {
            throw new InvalidDataException("Curve (curv) must have an ending parameter value");
        }
        if (commandLine.Count < 5)
        {
            throw new InvalidDataException("Curve (curv) must have at least 2 control vertices");
        }
        var curve = new Curve()
        {
            ParameterRange = new Range<float>(start: float.Parse(commandLine[1]), end: float.Parse(commandLine[2])),
            ControlVertexIndices = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        for (int i = 3; i < commandLine.Count; i++)
        {
            curve.ControlVertexIndices.Add(int.Parse(commandLine[i]));
        }
        context.FreeFormGeometry = curve;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
