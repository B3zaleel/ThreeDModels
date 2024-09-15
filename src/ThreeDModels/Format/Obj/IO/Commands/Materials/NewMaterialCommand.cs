using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO.Commands.Materials;

internal class NewMaterialCommand : IMaterialCommand
{
    public static string Name => "newmtl";

    public static void Read(List<string> commandLine, MaterialReaderContext context)
    {
        if (context.Material != null)
        {
            context.Materials.Add(context.Material);
        }
        context.Material = new Material { Name = commandLine[1] };
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
