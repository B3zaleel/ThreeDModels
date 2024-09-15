using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm.IO;

internal static class RawReader
{
    internal static void Read(BinaryReader reader, int verticesCount, int trianglesCount, int uvMapsCount, int attributeMapsCount, ref Ctm ctm)
    {
        ReadTriangles(reader, trianglesCount, ref ctm);
        ReadVertices(reader, verticesCount, ref ctm);
        ReadVertexNormals(reader, verticesCount, ref ctm);
        ReadUvMaps(reader, uvMapsCount, verticesCount, ref ctm);
        ReadAttributeMaps(reader, attributeMapsCount, verticesCount, ref ctm);
    }

    private static void ReadTriangles(BinaryReader reader, int trianglesCount, ref Ctm ctm)
    {
        for (var i = 0; i < trianglesCount; i++)
        {
            if (reader.ReadInt32() != Identifier.Indices)
            {
                throw new InvalidDataException("Invalid indices section");
            }
            ctm.Triangles.Add(new Triangle()
            {
                Vertex1 = reader.ReadInt32(),
                Vertex2 = reader.ReadInt32(),
                Vertex3 = reader.ReadInt32(),
            });
        }
    }

    private static void ReadVertices(BinaryReader reader, int verticesCount, ref Ctm ctm)
    {
        for (var i = 0; i < verticesCount; i++)
        {
            if (reader.ReadInt32() != Identifier.Vertices)
            {
                throw new InvalidDataException("Invalid vertices section");
            }
            ctm.Vertices.Add(new Vertex()
            {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle(),
                Z = reader.ReadSingle(),
            });
        }
    }

    private static void ReadVertexNormals(BinaryReader reader, int verticesCount, ref Ctm ctm)
    {
        if ((ctm.Flags & Flag.HasNormals) == Flag.HasNormals)
        {
            for (var i = 0; i < verticesCount; i++)
            {
                if (reader.ReadInt32() != Identifier.Normals)
                {
                    throw new InvalidDataException("Invalid normals section");
                }
                ctm.Vertices[i].Normal = new Normal()
                {
                    X = reader.ReadSingle(),
                    Y = reader.ReadSingle(),
                    Z = reader.ReadSingle(),
                };
            }
        }
    }

    private static void ReadUvMaps(BinaryReader reader, int uvMapsCount, int verticesCount, ref Ctm ctm)
    {
        for (var i = 0; i < uvMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.UVMaps)
            {
                throw new InvalidDataException("Invalid uv maps section");
            }
            var uvMap = new UvMap()
            {
                Name = CtmReader.ReadString(reader),
                Filename = CtmReader.ReadString(reader),
                Coordinates = new List<UvCoordinate>(verticesCount),
            };
            for (var j = 0; j < verticesCount; j++)
            {
                uvMap.Coordinates.Add(new UvCoordinate()
                {
                    U = reader.ReadSingle(),
                    V = reader.ReadSingle(),
                });
            }
            ctm.UvMaps.Add(uvMap);
        }
    }

    private static void ReadAttributeMaps(BinaryReader reader, int attributeMapsCount, int verticesCount, ref Ctm ctm)
    {
        for (var i = 0; i < attributeMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.AttributeMaps)
            {
                throw new InvalidDataException("Invalid attribute maps section");
            }
            var attributeMap = new AttributeMap()
            {
                Name = CtmReader.ReadString(reader),
                Values = new List<AttributeValue>(verticesCount),
            };
            for (var j = 0; j < verticesCount; j++)
            {
                attributeMap.Values.Add(new AttributeValue()
                {
                    A = reader.ReadSingle(),
                    B = reader.ReadSingle(),
                    C = reader.ReadSingle(),
                    D = reader.ReadSingle(),
                });
            }
            ctm.AttributeMaps.Add(attributeMap);
        }
    }
}
