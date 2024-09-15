using ThreeDModels.Format.Obj.Grouping;
using ThreeDModels.Format.Obj.IO.Commands;
using ThreeDModels.Format.Obj.IO.Commands.DisplayAttributes;
using ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceAttributes;
using ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceBodyStatements;
using ThreeDModels.Format.Obj.IO.Commands.FreeFormCurveSurfaceElements;
using ThreeDModels.Format.Obj.IO.Commands.Grouping;
using ThreeDModels.Format.Obj.IO.Commands.VertexData;

namespace ThreeDModels.Format.Obj.IO;

public class ObjReader
{
    private readonly Dictionary<string, ICommandRead> _commands = new()
    {
        // Vertex data
        {GeometricVertexCommand.Name, GeometricVertexCommand.Read},
        {TextureVertexCommand.Name, TextureVertexCommand.Read},
        {VertexNormalCommand.Name, VertexNormalCommand.Read},
        {ParameterSpaceVertexCommand.Name, ParameterSpaceVertexCommand.Read},
        // Free-form curve/surface elements
        {PointCommand.Name, PointCommand.Read},
        {LineCommand.Name, LineCommand.Read},
        {FaceCommand.Name, FaceCommand.Read},
        {CurveCommand.Name, CurveCommand.Read},
        {Curve2Command.Name, Curve2Command.Read},
        {SurfaceCommand.Name, SurfaceCommand.Read},
        // Free-form curve/surface attributes
        {CurveSurfaceTypeCommand.Name, CurveSurfaceTypeCommand.Read},
        {DegreeCommand.Name, DegreeCommand.Read},
        {BasisMatrixCommand.Name, BasisMatrixCommand.Read},
        {StepCommand.Name, StepCommand.Read},
        // Free-form curve/surface body statements
        {ParameterCommand.Name, ParameterCommand.Read},
        {TrimCommand.Name, TrimCommand.Read},
        {HoleCommand.Name, HoleCommand.Read},
        {SpecialCurveCommand.Name, SpecialCurveCommand.Read},
        {SpecialPointCommand.Name, SpecialPointCommand.Read},
        {EndFreeFormGeometryCommand.Name, EndFreeFormGeometryCommand.Read},
        // Grouping
        {GroupCommand.Name, GroupCommand.Read},
        {SmoothingGroupCommand.Name, SmoothingGroupCommand.Read},
        {MergingGroupCommand.Name, MergingGroupCommand.Read},
        {ObjectCommand.Name, ObjectCommand.Read},
        // Display/render attributes
        {BevelCommand.Name, BevelCommand.Read},
        {ColorInterpolationCommand.Name, ColorInterpolationCommand.Read},
        {DissolveInterpolationCommand.Name, DissolveInterpolationCommand.Read},
        {LevelOfDetailCommand.Name, LevelOfDetailCommand.Read},
        {MaterialLibraryCommand.Name, MaterialLibraryCommand.Read},
        {UseMapCommand.Name, UseMapCommand.Read},
        {UseMaterialCommand.Name, UseMaterialCommand.Read},
    };

    public Obj Execute(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return Execute(fs);
    }

    public Obj Execute(Stream stream)
    {
        var obj = new Obj();
        var context = new ObjReaderContext(group: new Group(), elementName: string.Empty, display: null, freeFormGeometry: null);
        using var textReader = new StreamReader(stream);
        while (stream.Position < stream.Length)
        {
            var commandLine = GetCommandLine(textReader);
            if (commandLine.Count == 0)
            {
                continue;
            }
            if (_commands.TryGetValue(commandLine[0], out var readCommand))
            {
                readCommand.Invoke(commandLine, context, ref obj);
            }
            else
            {
                throw new InvalidDataException($"Unknown command '{commandLine[0]}'");
            }
        }
        obj.Groups.Add(context.Group);
        return obj;
    }

    internal static bool GetBool(string value)
    {
        return value switch
        {
            Constants.On => true,
            Constants.Off => false,
            _ => throw new InvalidDataException($"Expected '{Constants.On}' or '{Constants.Off}' but found '{value}'"),
        };
    }

    internal static List<string> GetCommandLine(TextReader textReader)
    {
        var commandLine = new List<string>();
        var line = textReader.ReadLine();
        int tokenStartIndex = -1, i = 0;

        while (i < line?.Length)
        {
            if (char.IsWhiteSpace(line[i]) || line[i] == '#' || line[i] == '\\')
            {
                if (tokenStartIndex != -1)
                {
                    commandLine.Add(line.Substring(tokenStartIndex, i - tokenStartIndex));
                    tokenStartIndex = -1;
                }
                if (i > line.Length - 1 || line[i] == '#')
                {
                    break;
                }
                else if (line[i] == '\\')
                {
                    line = textReader.ReadLine();
                    i = -1;
                }
            }
            else if (i == line.Length - 1)
            {
                commandLine.Add(line.Substring(tokenStartIndex == -1 ? 0 : tokenStartIndex));
            }
            else if (tokenStartIndex == -1)
            {
                tokenStartIndex = i;
            }
            i++;
        }

        return commandLine;
    }
}
