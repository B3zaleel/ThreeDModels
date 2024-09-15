using ThreeDModels.Format.Obj.Grouping;

namespace ThreeDModels.Format.Obj.IO.Commands.Grouping;

internal class GroupCommand : ICommand
{
    public static string Name => "g";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.Group != null)
        {
            obj.Groups.Add(context.Group);
        }
        context.Group = new Group()
        {
            Names = commandLine.Count == 1 ? [Constants.DefaultGroup] : commandLine.GetRange(1, commandLine.Count - 1),
        };
        context.Display = null;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
