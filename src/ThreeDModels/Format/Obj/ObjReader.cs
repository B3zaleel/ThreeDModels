using ThreeDModels.Format.Obj.Display;
using ThreeDModels.Format.Obj.Elements;
using ThreeDModels.Format.Obj.Grouping;
using ThreeDModels.Format.Obj.Vertex;

namespace ThreeDModels.Format.Obj;

public class ObjReader
{
    public Obj Execute(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return Execute(fs);
    }

    public Obj Execute(Stream stream)
    {
        var obj = new Obj();
        var group = new Group();
        var elementName = string.Empty;
        DisplayAttributes? display = null;
        FreeFormGeometry? freeFormGeometry = null;
        using var textReader = new StreamReader(stream);
        while (stream.Position < stream.Length)
        {
            var commandLine = GetCommandLine(textReader);
            if (commandLine.Count == 0)
            {
                continue;
            }
            switch (commandLine[0])
            {
                #region Vertex data
                case "v":
                    {
                        obj.GeometricVertices.Add(new GeometricVertex
                        {
                            X = float.Parse(commandLine[1]),
                            Y = float.Parse(commandLine[2]),
                            Z = float.Parse(commandLine[3]),
                            W = commandLine.Count > 4 ? float.Parse(commandLine[4]) : 1,
                        });
                        break;
                    }
                case "vt":
                    {
                        obj.TextureVertices.Add(new TextureVertex
                        {
                            U = float.Parse(commandLine[1]),
                            V = commandLine.Count > 2 ? float.Parse(commandLine[2]) : 0.0f,
                            W = commandLine.Count > 3 ? float.Parse(commandLine[3]) : 0.0f,
                        });
                        break;
                    }
                case "vn":
                    {
                        obj.VertexNormals.Add(new VertexNormal
                        {
                            I = float.Parse(commandLine[1]),
                            J = float.Parse(commandLine[2]),
                            K = float.Parse(commandLine[3]),
                        });
                        break;
                    }
                case "vp":
                    {
                        obj.ParameterSpaceVertices.Add(new ParameterSpaceVertex
                        {
                            U = float.Parse(commandLine[1]),
                            V = commandLine.Count > 2 ? float.Parse(commandLine[2]) : 0.0f,
                            W = commandLine.Count > 3 ? float.Parse(commandLine[3]) : 1.0f,
                        });
                        break;
                    }
                #endregion
                #region Free-form curve/surface elements
                case "p":
                    {
                        var point = new Point()
                        {
                            VertexIndices = [],
                            Name = elementName,
                            Display = display,
                        };
                        for (int i = 1; i < commandLine.Count; i++)
                        {
                            point.VertexIndices.Add(int.Parse(commandLine[i]));
                        }
                        group.Points.Add(point);
                        break;
                    }
                case "l":
                    {
                        var line = new Line()
                        {
                            VertexIndices = [],
                            Name = elementName,
                            Display = display,
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
                        group.Lines.Add(line);
                        break;
                    }
                case "f":
                    {
                        var face = new Face()
                        {
                            VertexReferences = [],
                            Name = elementName,
                            Display = display,
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
                        group.Faces.Add(face);
                        break;
                    }
                case "curv":
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
                        var curve = new Curve()
                        {
                            ParameterRange = new Range<float>(start: float.Parse(commandLine[1]), end: float.Parse(commandLine[2])),
                            ControlVertexIndices = [],
                            Name = elementName,
                            Display = display,
                        };
                        for (int i = 3; i < commandLine.Count; i++)
                        {
                            curve.ControlVertexIndices.Add(int.Parse(commandLine[i]));
                        }
                        freeFormGeometry = curve;
                        break;
                    }
                case "curv2":
                    {
                        if (commandLine.Count < 3)
                        {
                            throw new InvalidDataException("Curve2D (curv2) must have at least 2 control points");
                        }
                        var curve2D = new Curve2D()
                        {
                            ParameterVertexIndices = [],
                            Name = elementName,
                            Display = display,
                        };
                        for (int i = 1; i < commandLine.Count; i++)
                        {
                            curve2D.ParameterVertexIndices.Add(int.Parse(commandLine[i]));
                        }
                        freeFormGeometry = curve2D;
                        break;
                    }
                case "surf":
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
                            Name = elementName,
                            Display = display,
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
                        freeFormGeometry = surface;
                        break;
                    }
                #endregion
                #region Free-form curve/surface attributes
                case "cstype":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("CurveSurfaceType (cstype) must follow a curve or surface element");
                        }
                        if (commandLine.Count > 2)
                        {
                            freeFormGeometry.IsRational = commandLine[1] == "rat";
                            freeFormGeometry.Type = Enum.Parse<CurveSurfaceType>(commandLine[2]);
                        }
                        else
                        {
                            freeFormGeometry.Type = Enum.Parse<CurveSurfaceType>(commandLine[1]);
                        }
                        break;
                    }
                case "deg":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("Degree (deg) must follow a curve or surface element");
                        }
                        freeFormGeometry.DegreeU = int.Parse(commandLine[1]);
                        freeFormGeometry.DegreeV = commandLine.Count > 2 ? int.Parse(commandLine[2]) : null;
                        break;
                    }
                case "bmat":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("BasisMatrix (bmat) must follow a curve or surface element");
                        }
                        var matrix = new List<float>();
                        for (int i = 2; i < commandLine.Count; i++)
                        {
                            matrix.Add(float.Parse(commandLine[i]));
                        }
                        if (commandLine[1] == "u")
                        {
                            freeFormGeometry.BasisMatrixU = matrix;
                        }
                        else
                        {
                            freeFormGeometry.BasisMatrixV = commandLine[1] == "v"
                                ? matrix
                                : throw new InvalidDataException($"BasisMatrix (bmat) has an invalid direction '{commandLine[1]}'");
                        }
                        break;
                    }
                case "step":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("Step (step) must follow a curve or surface element");
                        }
                        freeFormGeometry.StepU = int.Parse(commandLine[1]);
                        freeFormGeometry.StepV = commandLine.Count > 2 ? int.Parse(commandLine[2]) : null;
                        break;
                    }
                #endregion
                #region Free-form curve/surface body statements
                case "parm":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("Parm (parm) must follow a curve or surface element");
                        }
                        var matrix = new List<float>();
                        for (int i = 2; i < commandLine.Count; i++)
                        {
                            matrix.Add(float.Parse(commandLine[i]));
                        }
                        if (commandLine[1] == "u")
                        {
                            freeFormGeometry.ParameterU = matrix;
                        }
                        else
                        {
                            freeFormGeometry.ParameterV = commandLine[1] == "v"
                                ? matrix
                                : throw new InvalidDataException($"BasisMatrix (bmat) has an invalid direction '{commandLine[1]}'");
                        }
                        break;
                    }
                case "trim":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("Trim (trim) must follow a curve or surface element");
                        }
                        var curve2Ds = new List<Curve2DReference>();
                        for (int i = 1; i < commandLine.Count; i += 3)
                        {
                            curve2Ds.Add(new()
                            {
                                Start = float.Parse(commandLine[i]),
                                End = float.Parse(commandLine[i + 1]),
                                Curve2DIndex = int.Parse(commandLine[i + 2]),
                            });
                        }
                        freeFormGeometry.TrimmingCurves ??= [];
                        freeFormGeometry.TrimmingCurves.Add(curve2Ds);
                        break;
                    }
                case "hole":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("Hole (hole) must follow a curve or surface element");
                        }
                        var curve2Ds = new List<Curve2DReference>();
                        for (int i = 1; i < commandLine.Count; i += 3)
                        {
                            curve2Ds.Add(new()
                            {
                                Start = float.Parse(commandLine[i]),
                                End = float.Parse(commandLine[i + 1]),
                                Curve2DIndex = int.Parse(commandLine[i + 2]),
                            });
                        }
                        freeFormGeometry.Holes ??= [];
                        freeFormGeometry.Holes.Add(curve2Ds);
                        break;
                    }
                case "scrv":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("SpecialCurve (scrv) must follow a curve or surface element");
                        }
                        var curve2Ds = new List<Curve2DReference>();
                        for (int i = 1; i < commandLine.Count; i += 3)
                        {
                            curve2Ds.Add(new()
                            {
                                Start = float.Parse(commandLine[i]),
                                End = float.Parse(commandLine[i + 1]),
                                Curve2DIndex = int.Parse(commandLine[i + 2]),
                            });
                        }
                        freeFormGeometry.SpecialCurves ??= [];
                        freeFormGeometry.SpecialCurves.Add(curve2Ds);
                        break;
                    }
                case "sp":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("SpecialPoint (sp) must follow a curve or surface element");
                        }
                        var geometricPointsIndices = new List<int>();
                        for (int i = 1; i < commandLine.Count; i += 3)
                        {
                            geometricPointsIndices.Add(int.Parse(commandLine[i]));
                        }
                        freeFormGeometry.SpecialCurves ??= [];
                        freeFormGeometry.SpecialPoints.Add(geometricPointsIndices);
                        break;
                    }
                case "end":
                    {
                        if (freeFormGeometry == null)
                        {
                            throw new InvalidDataException("The curve or surface element is not initialized");
                        }
                        else if (freeFormGeometry is Curve curve)
                        {
                            group.Curves.Add(curve);
                        }
                        else if (freeFormGeometry is Curve2D curve2D)
                        {
                            group.Curves2D.Add(curve2D);
                        }
                        else if (freeFormGeometry is Surface surface)
                        {
                            group.Surfaces.Add(surface);
                        }
                        else
                        {
                            throw new InvalidDataException("Unknown free-form geometry type");
                        }
                        freeFormGeometry = null;
                        break;
                    }
                #endregion
                #region Grouping
                case "g":
                    {
                        if (group != null)
                        {
                            obj.Groups.Add(group);
                        }
                        group = new Group()
                        {
                            Names = commandLine.Count == 1 ? [Constants.DefaultGroup] : commandLine.GetRange(1, commandLine.Count - 1),
                        };
                        display = null;
                        break;
                    }
                case "sg":
                    {
                        int groupNumber = -1;
                        if (!int.TryParse(commandLine[1], out groupNumber) && commandLine[1] != Constants.Off || groupNumber < 0 || groupNumber > group.Names!.Count)
                        {
                            throw new InvalidDataException("Invalid smoothing group number");
                        }
                        if (commandLine[1] == Constants.Off || groupNumber == 0)
                        {
                            group.SmoothingGroupIndices = [];
                            break;
                        }
                        else if (!group.SmoothingGroupIndices.Contains(groupNumber - 1))
                        {
                            group.SmoothingGroupIndices.Add(groupNumber - 1);
                        }
                        break;
                    }
                case "mg":
                    {
                        int groupNumber = -1;
                        if (!int.TryParse(commandLine[1], out groupNumber) && commandLine[1] != Constants.Off || groupNumber < 0 || groupNumber > group.Names!.Count)
                        {
                            throw new InvalidDataException("Invalid merging group number");
                        }
                        if (commandLine[1] == Constants.Off || groupNumber == 0)
                        {
                            group.MergingGroups = [];
                            break;
                        }
                        else if (!group.MergingGroups.Any(mergingGroup => mergingGroup.GroupIndex == groupNumber - 1))
                        {
                            var resolution = float.Parse(commandLine[2]);
                            group.MergingGroups.Add(new MergingGroup { GroupIndex = groupNumber - 1, Resolution = resolution });
                        }
                        break;
                    }
                case "o":
                    {
                        elementName = commandLine[1];
                        break;
                    }
                #endregion
                #region Display/render attributes
                case "bevel":
                    {
                        display ??= new DisplayAttributes();
                        display.BevelInterpolationEnabled = GetBool(commandLine[1]);
                        break;
                    }
                case "c_interp":
                    {
                        display ??= new DisplayAttributes();
                        display.ColorInterpolationEnabled = GetBool(commandLine[1]);
                        break;
                    }
                case "d_interp":
                    {
                        display ??= new DisplayAttributes();
                        display.DissolveInterpolationEnabled = GetBool(commandLine[1]);
                        break;
                    }
                case "lod":
                    {
                        display ??= new DisplayAttributes();
                        display.LevelOfDetail = int.Parse(commandLine[1]);
                        break;
                    }
                case "maplib":
                    {
                        // TODO: Implement maplib
                        break;
                    }
                case "mtllib":
                    {
                        for (int i = 1; i < commandLine.Count; i++)
                        {
                            obj.MaterialLibraries.Add(GetMaterials(commandLine[i]));
                        }
                        break;
                    }
                case "usemap":
                    {
                        display ??= new DisplayAttributes();
                        display.TextureMapName = commandLine.Count > 1 && commandLine[1] != Constants.Off ? commandLine[1] : string.Empty;
                        break;
                    }
                case "usemtl":
                    {
                        display ??= new DisplayAttributes();
                        display.MaterialName = commandLine.Count > 1 ? commandLine[1] : string.Empty;
                        break;
                    }
                case "shadow_obj":
                    {
                        // TODO: Implement shadow_obj
                        break;
                    }
                case "trace_obj":
                    {
                        // TODO: Implement trace_obj
                        break;
                    }
                case "ctech":
                    {
                        // TODO: Implement ctech
                        break;
                    }
                case "stech":
                    {
                        // TODO: Implement stech
                        break;
                    }
                #endregion
                default:
                    {
                        throw new InvalidDataException($"Unknown command '{commandLine[0]}'");
                    }
            }
        }
        if (group != null)
        {
            obj.Groups.Add(group);
        }
        return obj;
    }

    public List<Material> GetMaterials(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return GetMaterials(fs);
    }

    public List<Material> GetMaterials(Stream stream)
    {
        var materials = new List<Material>();
        Material? material = null;
        using var textReader = new StreamReader(stream);
        while (stream.Position < stream.Length)
        {
            var commandLine = GetCommandLine(textReader);
            if (commandLine.Count == 0)
            {
                continue;
            }
            switch (commandLine[0])
            {
                case "newmtl":
                    {
                        if (material != null)
                        {
                            materials.Add(material);
                        }
                        material = new Material { Name = commandLine[1] };
                        break;
                    }
                case "Ka":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Ambient color (Ka) must follow a material definition (newmtl)");
                        }
                        material.Ambient = new Color
                        {
                            Red = float.Parse(commandLine[1]),
                            Green = float.Parse(commandLine[2]),
                            Blue = float.Parse(commandLine[3]),
                        };
                        break;
                    }
                case "Kd":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Diffuse color (Kd) must follow a material definition (newmtl)");
                        }
                        material.Diffuse = new Color
                        {
                            Red = float.Parse(commandLine[1]),
                            Green = float.Parse(commandLine[2]),
                            Blue = float.Parse(commandLine[3]),
                        };
                        break;
                    }
                case "Ks":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Specular color (Ks) must follow a material definition (newmtl)");
                        }
                        material.Specular = new Color
                        {
                            Red = float.Parse(commandLine[1]),
                            Green = float.Parse(commandLine[2]),
                            Blue = float.Parse(commandLine[3]),
                        };
                        break;
                    }
                case "Ns":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Specular exponent (Ns) must follow a material definition (newmtl)");
                        }
                        material.SpecularExponent = float.Parse(commandLine[1]);
                        break;
                    }
                case "d":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Transparency (d) must follow a material definition (newmtl)");
                        }
                        material.Transparency = 1 - float.Parse(commandLine[1]);
                        break;
                    }
                case "Tr":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Transparency (Tr) must follow a material definition (newmtl)");
                        }
                        material.Transparency = float.Parse(commandLine[1]);
                        break;
                    }
                case "illum":
                    {
                        if (material == null)
                        {
                            throw new InvalidDataException("Illumination model (illum) must follow a material definition (newmtl)");
                        }
                        material.IlluminationModel = byte.Parse(commandLine[1]);
                        break;
                    }
                default:
                    {
                        throw new InvalidDataException($"Unknown command '{commandLine[0]}'");
                    }
            }
        }
        if (material != null)
        {
            materials.Add(material);
        }
        return materials;
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
