namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class UseMaterialCommand : ICommand
{
    public static string Name => "usemtl";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.MaterialName = commandLine.Count > 1 ? commandLine[1] : string.Empty;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
