using ThreeDModels.Format.Obj.Vertex;

namespace ThreeDModels.Format.Obj.IO.Commands.VertexData;

internal class VertexNormalCommand : ICommand
{
    public static string Name => "vn";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        obj.VertexNormals.Add(new VertexNormal
        {
            I = float.Parse(commandLine[1]),
            J = float.Parse(commandLine[2]),
            K = float.Parse(commandLine[3]),
        });
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
