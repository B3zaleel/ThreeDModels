namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceAttributes;

internal class DegreeCommand : ICommand
{
    public static string Name => "deg";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("Degree (deg) must follow a curve or surface element");
        }
        context.FreeFormGeometry.DegreeU = int.Parse(commandLine[1]);
        context.FreeFormGeometry.DegreeV = commandLine.Count > 2 ? int.Parse(commandLine[2]) : null;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
