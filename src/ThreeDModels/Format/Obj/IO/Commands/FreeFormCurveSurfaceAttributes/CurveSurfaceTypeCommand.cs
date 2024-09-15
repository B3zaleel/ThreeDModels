using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceAttributes;

internal class CurveSurfaceTypeCommand : ICommand
{
    public static string Name => "cstype";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("CurveSurfaceType (cstype) must follow a curve or surface element");
        }
        if (commandLine.Count > 2)
        {
            context.FreeFormGeometry.IsRational = commandLine[1] == "rat";
            context.FreeFormGeometry.Type = Enum.Parse<CurveSurfaceType>(commandLine[2]);
        }
        else
        {
            context.FreeFormGeometry.Type = Enum.Parse<CurveSurfaceType>(commandLine[1]);
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
