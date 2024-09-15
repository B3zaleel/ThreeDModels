namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceAttributes;

internal class StepCommand : ICommand
{
    public static string Name => "step";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("Step (step) must follow a curve or surface element");
        }
        context.FreeFormGeometry.StepU = int.Parse(commandLine[1]);
        context.FreeFormGeometry.StepV = commandLine.Count > 2 ? int.Parse(commandLine[2]) : null;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
