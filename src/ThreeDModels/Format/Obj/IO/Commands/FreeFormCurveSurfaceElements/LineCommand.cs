using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class LineCommand : ICommand
{
    public static string Name => "l";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        var line = new Line()
        {
            VertexIndices = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        for (int i = 1; i < commandLine.Count; i++)
        {
            var fields = commandLine[i].Split(Constants.FieldSeparator);
            line.VertexIndices.Add(new VertexReference
            {
                VertexIndex = int.Parse(fields[0]),
                VertexTextureIndex = fields.Length > 1 ? int.Parse(fields[1]) : null
            });
        }
        context.Group.Lines.Add(line);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
