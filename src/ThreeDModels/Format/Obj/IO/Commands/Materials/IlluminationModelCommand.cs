namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class IlluminationModelCommand : IMaterialCommand
{
    public static string Name => "illum";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Illumination model (illum) must follow a material definition (newmtl)");
        }
        context.Material.IlluminationModel = byte.Parse(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
