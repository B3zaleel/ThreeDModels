namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceBodyStatements;

internal class SpecialPointCommand : ICommand
{
    public static string Name => "sp";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("SpecialPoint (sp) must follow a curve or surface element");
        }
        var geometricPointsIndices = new List<int>();
        for (int i = 1; i < commandLine.Count; i += 3)
        {
            geometricPointsIndices.Add(int.Parse(commandLine[i]));
        }
        context.FreeFormGeometry.SpecialCurves ??= [];
        context.FreeFormGeometry.SpecialPoints.Add(geometricPointsIndices);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
