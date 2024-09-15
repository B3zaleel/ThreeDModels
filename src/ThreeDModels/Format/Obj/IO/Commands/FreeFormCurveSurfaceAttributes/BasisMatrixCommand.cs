namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceAttributes;

internal class BasisMatrixCommand : ICommand
{
    public static string Name => "bmat";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("BasisMatrix (bmat) must follow a curve or surface element");
        }
        var matrix = new List<float>();
        for (int i = 2; i < commandLine.Count; i++)
        {
            matrix.Add(float.Parse(commandLine[i]));
        }
        if (commandLine[1] == "u")
        {
            context.FreeFormGeometry.BasisMatrixU = matrix;
        }
        else
        {
            context.FreeFormGeometry.BasisMatrixV = commandLine[1] == "v"
                ? matrix
                : throw new InvalidDataException($"BasisMatrix (bmat) has an invalid direction '{commandLine[1]}'");
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
