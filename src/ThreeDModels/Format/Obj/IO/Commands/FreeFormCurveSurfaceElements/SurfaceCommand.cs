using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class SurfaceCommand : ICommand
{
    public static string Name => "surf";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (commandLine.Count == 1)
        {
            throw new InvalidDataException("Curve (curv) must have a starting parameter value");
        }
        if (commandLine.Count == 2)
        {
            throw new InvalidDataException("Curve (curv) must have an ending parameter value");
        }
        if (commandLine.Count < 5)
        {
            throw new InvalidDataException("Curve (curv) must have at least 2 control vertices");
        }
        var surface = new Surface()
        {
            UParameterRange = new Range<float>(start: float.Parse(commandLine[1]), end: float.Parse(commandLine[2])),
            VParameterRange = new Range<float>(start: float.Parse(commandLine[3]), end: float.Parse(commandLine[4])),
            VertexReferences = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        var requireVertexTextureIndex = false;
        var requireVertexNormalIndex = false;
        for (int i = 4; i < commandLine.Count; i++)
        {
            var fields = commandLine[i].Split(Constants.FieldSeparator);
            if (i == 1)
            {
                requireVertexTextureIndex = fields.Length > 1 && !string.IsNullOrEmpty(fields[1]);
                requireVertexNormalIndex = fields.Length > 2 && !string.IsNullOrEmpty(fields[2]);
            }
            if ((requireVertexTextureIndex && (fields.Length < 2 || string.IsNullOrEmpty(fields[1])))
                || (requireVertexNormalIndex && fields.Length < 3 && !string.IsNullOrEmpty(fields[2])))
            {
                throw new InvalidDataException("Inconsistent surface (surf) fields");
            }
            surface.VertexReferences.Add(new VertexReference()
            {
                VertexIndex = int.Parse(fields[0]),
                VertexTextureIndex = requireVertexTextureIndex ? int.Parse(fields[1]) : null,
                VertexNormalIndex = requireVertexNormalIndex ? int.Parse(fields[2]) : null
            });
        }
        context.FreeFormGeometry = surface;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
