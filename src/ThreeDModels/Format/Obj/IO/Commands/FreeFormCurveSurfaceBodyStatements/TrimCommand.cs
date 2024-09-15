using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceBodyStatements;

internal class TrimCommand : ICommand
{
    public static string Name => "trim";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("Trim (trim) must follow a curve or surface element");
        }
        var curve2Ds = new List<Curve2DReference>();
        for (int i = 1; i < commandLine.Count; i += 3)
        {
            curve2Ds.Add(new()
            {
                Start = float.Parse(commandLine[i]),
                End = float.Parse(commandLine[i + 1]),
                Curve2DIndex = int.Parse(commandLine[i + 2]),
            });
        }
        context.FreeFormGeometry.TrimmingCurves ??= [];
        context.FreeFormGeometry.TrimmingCurves.Add(curve2Ds);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
