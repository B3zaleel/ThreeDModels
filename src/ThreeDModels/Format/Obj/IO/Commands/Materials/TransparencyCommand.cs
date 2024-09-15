namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class TransparencyCommand : IMaterialCommand
{
    public static string Name => "Tr";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Transparency (Tr) must follow a material definition (newmtl)");
        }
        context.Material.Transparency = float.Parse(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
