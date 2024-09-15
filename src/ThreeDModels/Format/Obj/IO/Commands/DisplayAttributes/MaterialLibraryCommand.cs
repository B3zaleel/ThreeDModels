using ThreeDModels.Format.Obj.Display;
using ThreeDModels.Format.Obj.IO.Commands.Materials;

namespace ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;

internal class MaterialLibraryCommand : ICommand
{
    private static readonly Dictionary<string, IMaterialCommandRead> _commands = new()
    {
        { NewMaterialCommand.Name, NewMaterialCommand.Read },
        { AmbientColorCommand.Name, AmbientColorCommand.Read },
        { DiffuseColorCommand.Name, DiffuseColorCommand.Read },
        { SpecularColorCommand.Name, SpecularColorCommand.Read },
        { SpecularExponentCommand.Name, SpecularExponentCommand.Read },
        { DissolveCommand.Name, DissolveCommand.Read },
        { TransparencyCommand.Name, TransparencyCommand.Read },
        { IlluminationModelCommand.Name, IlluminationModelCommand.Read },
    };
    public static string Name => "mtllib";

    public static void Read(List<string> commandLine, ObjReaderContext context, ref Obj obj)
    {
        for (int i = 1; i < commandLine.Count; i++)
        {
            obj.MaterialLibraries.Add(GetMaterials(commandLine[i]));
        }
    }

    public static void Write()
    {
        throw new NotImplementedException();
    }

    private static List<Material> GetMaterials(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return GetMaterials(fs);
    }

    private static List<Material> GetMaterials(Stream stream)
    {
        var context = new MaterialReaderContext(material: null, materials: []);
        using var textReader = new StreamReader(stream);
        while (stream.Position < stream.Length)
        {
            var commandLine = ObjReader.GetCommandLine(textReader);
            if (commandLine.Count == 0)
            {
                continue;
            }
            if (_commands.TryGetValue(commandLine[0], out var readCommand))
            {
                readCommand.Invoke(commandLine, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown material command '{commandLine[0]}'");
            }
        }
        if (context.Material != null)
        {
            context.Materials.Add(context.Material);
        }
        return context.Materials;
    }
}
