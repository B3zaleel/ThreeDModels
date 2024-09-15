namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceBodyStatements;

internal class ParameterCommand : ICommand
{
    public static string Name => "parm";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("Parameter (parm) must follow a curve or surface element");
        }
        var matrix = new List<float>();
        for (int i = 2; i < commandLine.Count; i++)
        {
            matrix.Add(float.Parse(commandLine[i]));
        }
        if (commandLine[1] == "u")
        {
            context.FreeFormGeometry.ParameterU = matrix;
        }
        else
        {
            context.FreeFormGeometry.ParameterV = commandLine[1] == "v"
                ? matrix
                : throw new InvalidDataException($"BasisMatrix (bmat) has an invalid direction '{commandLine[1]}'");
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
