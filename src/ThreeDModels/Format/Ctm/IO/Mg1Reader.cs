using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm.IO;

internal static class Mg1Reader
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
        if (trianglesCount > 0 && reader.ReadInt32() != Identifier.Indices)
        {
            throw new InvalidDataException("Invalid indices section");
        }
        using var trianglesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, trianglesCount * 3 * sizeof(int)));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < trianglesCount; j++)
            {
                var deltaIndex = trianglesReader.ReadInt32();
                if (i == 0)
                {
                    ctm.Triangles.Add(new Triangle()
                    {
                        Vertex1 = j > 0 ? deltaIndex + ctm.Triangles[j - 1].Vertex1 : deltaIndex,
                        Vertex2 = 0,
                        Vertex3 = 0,
                    });
                }
                else if (i == 1)
                {
                    ctm.Triangles[j].Vertex2 = j > 0 && ctm.Triangles[j].Vertex1 == ctm.Triangles[j - 1].Vertex1 ? deltaIndex + ctm.Triangles[j - 1].Vertex2 : deltaIndex + ctm.Triangles[j].Vertex1;
                }
                else
                {
                    ctm.Triangles[j].Vertex3 = deltaIndex + ctm.Triangles[j].Vertex1;
                }
            }
        }
    }

    private static void ReadVertices(BinaryReader reader, int verticesCount, ref Ctm ctm)
    {
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.Vertices)
        {
            throw new InvalidDataException("Invalid vertices section");
        }
        using var verticesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 3 * sizeof(float)));
        for (var i = 0; i < verticesCount; i++)
        {
            ctm.Vertices.Add(new Vertex()
            {
                X = verticesReader.ReadSingle(),
                Y = verticesReader.ReadSingle(),
                Z = verticesReader.ReadSingle(),
            });
        }
    }

    private static void ReadVertexNormals(BinaryReader reader, int verticesCount, ref Ctm ctm)
    {
        if ((ctm.Flags & Flag.HasNormals) == Flag.HasNormals)
        {
            if (verticesCount > 0 && reader.ReadInt32() != Identifier.Normals)
            {
                throw new InvalidDataException("Invalid normals section");
            }
            using var normalsReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 3 * sizeof(float)));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (i == 0)
                    {
                        ctm.Vertices[j].Normal = new Normal()
                        {
                            X = normalsReader.ReadSingle(),
                            Y = 0,
                            Z = 0,
                        };
                    }
                    else if (i == 1)
                    {
                        ctm.Vertices[j].Normal!.Y = normalsReader.ReadSingle();
                    }
                    else
                    {
                        ctm.Vertices[j].Normal!.Z = normalsReader.ReadSingle();
                    }

                }
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
            using var uvCoordinatesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 2 * sizeof(float)));
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < verticesCount; k++)
                {
                    if (j == 0)
                    {
                        uvMap.Coordinates.Add(new UvCoordinate()
                        {
                            U = uvCoordinatesReader.ReadSingle(),
                            V = 0,
                        });
                    }
                    else
                    {
                        uvMap.Coordinates[k].V = uvCoordinatesReader.ReadSingle();
                    }
                }
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
            using var attributeValuesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 4 * sizeof(float)));
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < verticesCount; k++)
                {
                    if (j == 0)
                    {
                        attributeMap.Values.Add(new AttributeValue()
                        {
                            A = attributeValuesReader.ReadSingle(),
                            B = 0,
                            C = 0,
                            D = 0,
                        });
                    }
                    else if (j == 1)
                    {
                        attributeMap.Values[k].B = attributeValuesReader.ReadSingle();
                    }
                    else if (j == 2)
                    {
                        attributeMap.Values[k].C = attributeValuesReader.ReadSingle();
                    }
                    else
                    {
                        attributeMap.Values[k].D = attributeValuesReader.ReadSingle();
                    }
                }
            }
            ctm.AttributeMaps.Add(attributeMap);
        }
    }
}
