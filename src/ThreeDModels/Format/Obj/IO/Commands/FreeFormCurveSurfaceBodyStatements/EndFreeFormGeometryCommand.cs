using ThreeDModels.Format.Obj.Elements;

namespace ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceBodyStatements;

internal class EndFreeFormGeometryCommand : ICommand
{
    public static string Name => "end";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        if (context.FreeFormGeometry == null)
        {
            throw new InvalidDataException("The curve or surface element is not initialized");
        }
        else if (context.FreeFormGeometry is Curve curve)
        {
            context.Group.Curves.Add(curve);
        }
        else if (context.FreeFormGeometry is Curve2D curve2D)
        {
            context.Group.Curves2D.Add(curve2D);
        }
        else if (context.FreeFormGeometry is Surface surface)
        {
            context.Group.Surfaces.Add(surface);
        }
        else
        {
            throw new InvalidDataException("Unknown free-form geometry type");
        }
        context.FreeFormGeometry = null;
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }
}
