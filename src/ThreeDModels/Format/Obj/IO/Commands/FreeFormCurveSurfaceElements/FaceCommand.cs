using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;

internal class FaceCommand : ICommand
{
    public static string Name => "f";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        var face = new Face()
        {
            VertexReferences = [],
            Name = context.ElementName,
            Display = context.Display,
        };
        if (commandLine.Count < 4)
        {
            throw new InvalidDataException("Face (f) must have at least 3 vertices");
        }
        var requireVertexTextureIndex = false;
        var requireVertexNormalIndex = false;
        for (int i = 1; i < commandLine.Count; i++)
        {
            var fields = commandLine[i].Split(Constants.FieldSeparator);
            if (i == 1)
            {
                requireVertexTextureIndex = fields.Length > 1 && !string.IsNullOrEmpty(fields[1]);
                requireVertexNormalIndex = fields.Length > 2 && !string.IsNullOrEmpty(fields[2]);
            }
            if ((requireVertexTextureIndex && (fields.Length < 2 || string.IsNullOrEmpty(fields[1])))
                || (requireVertexNormalIndex && (fields.Length < 3 || string.IsNullOrEmpty(fields[2]))))
            {
                throw new InvalidDataException("Inconsistent face (f) fields");
            }
            face.VertexReferences.Add(new VertexReference()
            {
                VertexIndex = int.Parse(fields[0]),
                VertexTextureIndex = requireVertexTextureIndex ? int.Parse(fields[1]) : null,
                VertexNormalIndex = requireVertexNormalIndex ? int.Parse(fields[2]) : null
            });
        }
        context.Group.Faces.Add(face);
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
