using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class DiffuseColorCommand : IMaterialCommand
{
    public static string Name => "Kd";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Diffuse color (Kd) must follow a material definition (newmtl)");
        }
        context.Material.Diffuse = new Color
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
