using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class PointCommand : ICommand
{
    public static string Name => "p";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        var point = new Point()
        {
            VertexIndices = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        for (int i = 1; i < commandLine.Count; i++)
        {
            point.VertexIndices.Add(int.Parse(commandLine[i]));
        }
        context.Group.Points.Add(point);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
