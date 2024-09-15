using ThreeDModels.Format.Obj.Grouping;

namespace ThreeDModels.Format.Obj.IO.Commands.Grouping;

internal class MergingGroupCommand : ICommand
{
    public static string Name => "mg";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        int groupNumber = -1;
        if (!int.TryParse(commandLine[1], out groupNumber) && commandLine[1] != Constants.Off || groupNumber < 0 || groupNumber > context.Group.Names!.Count)
        {
            throw new InvalidDataException("Invalid merging group number");
        }
        if (commandLine[1] == Constants.Off || groupNumber == 0)
        {
            context.Group.MergingGroups = [];
        }
        else if (!context.Group.MergingGroups.Any(mergingGroup => mergingGroup.GroupIndex == groupNumber - 1))
        {
            var resolution = float.Parse(commandLine[2]);
            context.Group.MergingGroups.Add(new MergingGroup { GroupIndex = groupNumber - 1, Resolution = resolution });
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
