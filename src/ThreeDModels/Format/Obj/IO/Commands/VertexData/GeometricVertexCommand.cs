using ThreeDModels.Format.Obj.Vertex;

namespace ThreeDModels.Format.Obj.IO.Commands.VertexData;

internal class GeometricVertexCommand : ICommand
{
    public static string Name => "v";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        obj.GeometricVertices.Add(new GeometricVertex
        {
            X = float.Parse(commandLine[1]),
            Y = float.Parse(commandLine[2]),
            Z = float.Parse(commandLine[3]),
            W = commandLine.Count > 4 ? float.Parse(commandLine[4]) : 1,
        });
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
