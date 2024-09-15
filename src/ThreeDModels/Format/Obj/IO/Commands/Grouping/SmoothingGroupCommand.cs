namespace ThreeDModels.Format.Obj.IO.Commands.Grouping;

internal class SmoothingGroupCommand : ICommand
{
    public static string Name => "sg";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        int groupNumber = -1;
        if (!int.TryParse(commandLine[1], out groupNumber) && commandLine[1] != Constants.Off || groupNumber < 0 || groupNumber > context.Group.Names!.Count)
        {
            throw new InvalidDataException("Invalid smoothing group number");
        }
        if (commandLine[1] == Constants.Off || groupNumber == 0)
        {
            context.Group.SmoothingGroupIndices = [];
        }
        else if (!context.Group.SmoothingGroupIndices.Contains(groupNumber - 1))
        {
            context.Group.SmoothingGroupIndices.Add(groupNumber - 1);
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
