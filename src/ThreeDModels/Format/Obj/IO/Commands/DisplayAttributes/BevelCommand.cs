using ThreeDModels.Format.Obj.Display;

namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class BevelCommand : ICommand
{
    public static string Name => "bevel";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.BevelInterpolationEnabled = ObjReader.GetBool(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
