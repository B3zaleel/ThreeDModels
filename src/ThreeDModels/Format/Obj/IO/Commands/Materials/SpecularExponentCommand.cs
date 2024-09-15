namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class SpecularExponentCommand : IMaterialCommand
{
    public static string Name => "Ns";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material == null)
        {
            throw new InvalidDataException("Specular exponent (Ns) must follow a material definition (newmtl)");
        }
        context.Material.SpecularExponent = float.Parse(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
