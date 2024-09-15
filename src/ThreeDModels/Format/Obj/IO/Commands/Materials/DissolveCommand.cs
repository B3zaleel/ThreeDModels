namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class DissolveCommand : IMaterialCommand
{
    public static string Name => "d";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Dissolve (d) must follow a material definition (newmtl)");
        }
        context.Material.Transparency = 1 - float.Parse(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
