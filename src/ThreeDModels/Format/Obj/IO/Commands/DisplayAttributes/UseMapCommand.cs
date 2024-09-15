namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class UseMapCommand : ICommand
{
    public static string Name => "usemap";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.TextureMapName = commandLine.Count > 1 && commandLine[1] != Constants.Off ? commandLine[1] : string.Empty;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
