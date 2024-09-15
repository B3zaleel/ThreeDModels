using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class SpecularColorCommand : IMaterialCommand
{
    public static string Name => "Ks";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Specular color (Ks) must follow a material definition (newmtl)");
        }
        context.Material.Specular = new Color
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
