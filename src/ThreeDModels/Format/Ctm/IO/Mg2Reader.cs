using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm.IO;

internal static class Mg2Reader
{
    internal static void Read(BinaryReader reader, int verticesCount, int trianglesCount, int uvMapsCount, int attributeMapsCount, ref Ctm ctm)
    {
        if (reader.ReadInt32() != Identifier.MG2H)
        {
            throw new InvalidDataException("Invalid MG2 header");
        }
        var header = ReadHeader(reader);
        ReadVertices(reader, header, verticesCount, ref ctm);
        ReadTriangles(reader, trianglesCount, ref ctm);
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

    private static Mg2Header ReadHeader(BinaryReader reader)
    {
        var vertexPrecision = reader.ReadSingle();
        var normalPrecision = reader.ReadSingle();
        var lbX = reader.ReadSingle();
        var lbY = reader.ReadSingle();
        var lbZ = reader.ReadSingle();
        var ubX = reader.ReadSingle();
        var ubY = reader.ReadSingle();
        var ubZ = reader.ReadSingle();
        var divX = reader.ReadInt32();
        var divY = reader.ReadInt32();
        var divZ = reader.ReadInt32();
        return new Mg2Header(vertexPrecision, normalPrecision, new Data3D<float>(X: lbX, Y: lbY, Z: lbZ), new Data3D<float>(X: ubX, Y: ubY, Z: ubZ), new Data3D<int>(X: divX, Y: divY, Z: divZ));
    }

    private static void ReadVertices(BinaryReader reader, Mg2Header header, int verticesCount, ref Ctm ctm)
    {
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.Vertices)
        {
            throw new InvalidDataException("Invalid vertices section");
        }
        using var verticesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 3 * sizeof(float)));
        var gridIndices = new List<int>(verticesCount);
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.GridIndices)
        {
            throw new InvalidDataException("Invalid grid indices section");
        }
        using var gridIndicesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * sizeof(int)));
        for (var i = 0; i < verticesCount; i++)
        {
            var deltaGridIndex = gridIndicesReader.ReadInt32();
            gridIndices.Add(i == 0 ? deltaGridIndex : deltaGridIndex + gridIndices[i - 1]);
        }
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                var deltaIndex = verticesReader.ReadInt32();
                if (i == 0)
                {
                    var dx = j > 0 && gridIndices[j] == gridIndices[j - 1]
                        ? deltaIndex + ((ctm.Vertices[j - 1].X - GridOrigin(header.LowerBoundCoordinates.X, header.UpperBoundCoordinates.X, header.GridDivisions.X, gridIndices[j - 1])) / header.VertexPrecision)
                        : deltaIndex;
                    ctm.Vertices.Add(new Vertex()
                    {
                        X = header.VertexPrecision * dx + GridOrigin(header.LowerBoundCoordinates.X, header.UpperBoundCoordinates.X, header.GridDivisions.X, gridIndices[j]),
                        Y = 0,
                        Z = 0,
                    });
                }
                else if (i == 1)
                {
                    ctm.Vertices[j].Y = header.VertexPrecision * deltaIndex + GridOrigin(header.LowerBoundCoordinates.Y, header.UpperBoundCoordinates.Y, header.GridDivisions.Y, gridIndices[j]);
                }
                else
                {
                    ctm.Vertices[j].Z = header.VertexPrecision * deltaIndex + GridOrigin(header.LowerBoundCoordinates.Z, header.UpperBoundCoordinates.Z, header.GridDivisions.Z, gridIndices[j]);
                }
            }
        }
    }

    private static void ReadVertexNormals(BinaryReader reader, int verticesCount, ref Ctm ctm)
    {
        if ((ctm.Flags & Flag.HasNormals) == Flag.HasNormals)
        {
            // TODO: Implement normal decoding
            CtmReader.UnpackLzmaStream(reader, verticesCount * 2 * sizeof(float));
            throw new NotImplementedException("CTM MG2 Normal decoding is not implemented");
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
            var uvCoordinatePrecision = reader.ReadSingle();
            using var uvCoordinatesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 2 * sizeof(float)));
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < verticesCount; k++)
                {
                    var deltaCoordinate = uvCoordinatesReader.ReadInt32();
                    if (j == 0)
                    {
                        uvMap.Coordinates.Add(new UvCoordinate()
                        {
                            U = k == 0 ? uvCoordinatePrecision * deltaCoordinate : uvCoordinatePrecision * (deltaCoordinate + uvMap.Coordinates[k - 1].U),
                            V = 0,
                        });
                    }
                    else
                    {
                        uvMap.Coordinates[k].V = k == 0 ? uvCoordinatePrecision * deltaCoordinate : uvCoordinatePrecision * (deltaCoordinate + uvMap.Coordinates[k - 1].V);
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
            var attributeValuePrecision = reader.ReadSingle();
            using var attributeValuesReader = new BinaryReader(CtmReader.UnpackLzmaStream(reader, verticesCount * 4 * sizeof(float)));
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < verticesCount; k++)
                {
                    var deltaValue = attributeValuesReader.ReadInt32();
                    if (j == 0)
                    {
                        attributeMap.Values.Add(new AttributeValue()
                        {
                            A = k == 0 ? attributeValuePrecision * deltaValue : attributeValuePrecision * (deltaValue + attributeMap.Values[k - 1].A),
                            B = 0,
                            C = 0,
                            D = 0,
                        });
                    }
                    else if (j == 1)
                    {
                        attributeMap.Values[k].B = k == 0 ? attributeValuePrecision * deltaValue : attributeValuePrecision * (deltaValue + attributeMap.Values[k - 1].B);
                    }
                    else if (j == 2)
                    {
                        attributeMap.Values[k].C = k == 0 ? attributeValuePrecision * deltaValue : attributeValuePrecision * (deltaValue + attributeMap.Values[k - 1].C);
                    }
                    else
                    {
                        attributeMap.Values[k].D = k == 0 ? attributeValuePrecision * deltaValue : attributeValuePrecision * (deltaValue + attributeMap.Values[k - 1].D);
                    }
                }
            }
            ctm.AttributeMaps.Add(attributeMap);
        }
    }

    internal static float GridOrigin(float lb, float ub, int div, int g)
    {
        return lb + ((ub - lb) / div * g);
    }
}
