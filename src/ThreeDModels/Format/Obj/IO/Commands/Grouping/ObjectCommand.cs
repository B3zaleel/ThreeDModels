namespace ThreeDModels.Format.Obj.IO.Commands.Grouping;

internal class ObjectCommand : ICommand
{
    public static string Name => "o";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        context.ElementName = commandLine[1];
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
