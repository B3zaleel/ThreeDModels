namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class LevelOfDetailCommand : ICommand
{
    public static string Name => "lod";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.Display ??= new Display.DisplayAttributes();
        context.Display.LevelOfDetail = int.Parse(commandLine[1]);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
