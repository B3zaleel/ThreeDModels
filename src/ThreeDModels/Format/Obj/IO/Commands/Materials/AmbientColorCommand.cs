using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class AmbientColorCommand : IMaterialCommand
{
    public static string Name => "Ka";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Ambient color (Ka) must follow a material definition (newmtl)");
        }
        context.Material.Ambient = new Color
        {
            Red = float.Parse(commandLine[1]),
            Green = float.Parse(commandLine[2]),
            Blue = float.Parse(commandLine[3]),
        };
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
