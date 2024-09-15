using ThreeDModels.Format.Obj.Vertex;

namespace ThreeDModels.Format.Obj.IO.Commands.VertexData;

internal class ParameterSpaceVertexCommand : ICommand
{
    public static string Name => "vp";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        obj.ParameterSpaceVertices.Add(new ParameterSpaceVertex
        {
            U = float.Parse(commandLine[1]),
            V = commandLine.Count > 2 ? float.Parse(commandLine[2]) : 0.0f,
            W = commandLine.Count > 3 ? float.Parse(commandLine[3]) : 1.0f,
        });
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
