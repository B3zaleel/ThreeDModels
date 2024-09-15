using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class Curve2Command : ICommand
{
    public static string Name => "curv2";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (commandLine.Count < 3)
        {
            throw new InvalidDataException("Curve2D (curv2) must have at least 2 control points");
        }
        var curve2D = new Curve2D()
        {
            ParameterVertexIndices = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        for (int i = 1; i < commandLine.Count; i++)
        {
            curve2D.ParameterVertexIndices.Add(int.Parse(commandLine[i]));
        }
        context.FreeFormGeometry = curve2D;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
