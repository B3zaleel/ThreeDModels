namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class DissolveInterpolationCommand : ICommand
{
    public static string Name => "d_interp";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.DissolveInterpolationEnabled = ObjReader.GetBool(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
