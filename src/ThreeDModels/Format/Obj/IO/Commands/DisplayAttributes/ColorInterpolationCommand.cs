namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class ColorInterpolationCommand : ICommand
{
    public static string Name => "c_interp";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.ColorInterpolationEnabled = ObjReader.GetBool(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
