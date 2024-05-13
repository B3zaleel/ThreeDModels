using SevenZip.Compression.LZMA;
using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm;

public class CtmReader
{
    public Ctm Execute(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        return Execute(fs);
    }

    public Ctm Execute(Stream stream)
    {
        using var reader = new BinaryReader(stream);
        if (reader.ReadInt32() != Identifier.Magic)
        {
            throw new InvalidDataException("Invalid magic identifier");
        }
        if (reader.ReadInt32() > Constants.Version)
        {
            throw new InvalidDataException("Unsupported version");
        }
        var method = (CompressionMethod)reader.ReadInt32();
        var ctm = new Ctm()
        {
            CompressionMethod = method,
        };
        var verticesCount = reader.ReadInt32();
        var trianglesCount = reader.ReadInt32();
        var uvMapsCount = reader.ReadInt32();
        var attributeMapsCount = reader.ReadInt32();
        ctm.Vertices = new List<Vertex>(verticesCount);
        ctm.Triangles = new List<Triangle>(trianglesCount);
        ctm.UvMaps = new List<UvMap>(uvMapsCount);
        ctm.AttributeMaps = new List<AttributeMap>(attributeMapsCount);
        ctm.Flags = reader.ReadInt32();
        ctm.Comment = ReadString(reader);
        switch (method)
        {
            case CompressionMethod.RAW:
                {
                    ReadBodyAsRaw(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            case CompressionMethod.MG1:
                {
                    ReadBodyAsMg1(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            case CompressionMethod.MG2:
                {
                    ReadBodyAsMg2(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            default:
                {
                    throw new InvalidDataException($"Unsupported compression method {method}");
                }
        }
        return ctm;
    }

    internal void ReadBodyAsRaw(BinaryReader reader, int verticesCount, int trianglesCount, int uvMapsCount, int attributeMapsCount, ref Ctm ctm)
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
        for (var i = 0; i < uvMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.UVMaps)
            {
                throw new InvalidDataException("Invalid uv maps section");
            }
            var uvMap = new UvMap()
            {
                Name = ReadString(reader),
                Filename = ReadString(reader),
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
        for (var i = 0; i < attributeMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.AttributeMaps)
            {
                throw new InvalidDataException("Invalid attribute maps section");
            }
            var attributeMap = new AttributeMap()
            {
                Name = ReadString(reader),
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

    internal void ReadBodyAsMg1(BinaryReader reader, int verticesCount, int trianglesCount, int uvMapsCount, int attributeMapsCount, ref Ctm ctm)
    {
        if (trianglesCount > 0 && reader.ReadInt32() != Identifier.Indices)
        {
            throw new InvalidDataException("Invalid indices section");
        }
        using var trianglesReader = new BinaryReader(Unpack(reader, trianglesCount * 3 * sizeof(int)));
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
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.Vertices)
        {
            throw new InvalidDataException("Invalid vertices section");
        }
        using var verticesReader = new BinaryReader(Unpack(reader, verticesCount * 3 * sizeof(float)));
        for (var i = 0; i < verticesCount; i++)
        {
            ctm.Vertices.Add(new Vertex()
            {
                X = verticesReader.ReadSingle(),
                Y = verticesReader.ReadSingle(),
                Z = verticesReader.ReadSingle(),
            });
        }
        if ((ctm.Flags & Flag.HasNormals) == Flag.HasNormals)
        {
            if (verticesCount > 0 && reader.ReadInt32() != Identifier.Normals)
            {
                throw new InvalidDataException("Invalid normals section");
            }
            using var normalsReader = new BinaryReader(Unpack(reader, verticesCount * 3 * sizeof(float)));
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
        for (var i = 0; i < uvMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.UVMaps)
            {
                throw new InvalidDataException("Invalid uv maps section");
            }
            var uvMap = new UvMap()
            {
                Name = ReadString(reader),
                Filename = ReadString(reader),
                Coordinates = new List<UvCoordinate>(verticesCount),
            };
            using var uvCoordinatesReader = new BinaryReader(Unpack(reader, verticesCount * 2 * sizeof(float)));
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
        for (var i = 0; i < attributeMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.AttributeMaps)
            {
                throw new InvalidDataException("Invalid attribute maps section");
            }
            var attributeMap = new AttributeMap()
            {
                Name = ReadString(reader),
                Values = new List<AttributeValue>(verticesCount),
            };
            using var attributeValuesReader = new BinaryReader(Unpack(reader, verticesCount * 4 * sizeof(float)));
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

    internal void ReadBodyAsMg2(BinaryReader reader, int verticesCount, int trianglesCount, int uvMapsCount, int attributeMapsCount, ref Ctm ctm)
    {
        if (reader.ReadInt32() != Identifier.MG2H)
        {
            throw new InvalidDataException("Invalid MG2 header");
        }
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
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.Vertices)
        {
            throw new InvalidDataException("Invalid vertices section");
        }
        using var verticesReader = new BinaryReader(Unpack(reader, verticesCount * 3 * sizeof(float)));
        var gridIndices = new List<int>(verticesCount);
        if (verticesCount > 0 && reader.ReadInt32() != Identifier.GridIndices)
        {
            throw new InvalidDataException("Invalid grid indices section");
        }
        using var gridIndicesReader = new BinaryReader(Unpack(reader, verticesCount * sizeof(int)));
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
                        ? deltaIndex + ((ctm.Vertices[j - 1].X - GridOrigin(lbX, ubX, divX, gridIndices[j - 1])) / vertexPrecision)
                        : deltaIndex;
                    ctm.Vertices.Add(new Vertex()
                    {
                        X = vertexPrecision * dx + GridOrigin(lbX, ubX, divX, gridIndices[j]),
                        Y = 0,
                        Z = 0,
                    });
                }
                else if (i == 1)
                {
                    ctm.Vertices[j].Y = vertexPrecision * deltaIndex + GridOrigin(lbY, ubY, divY, gridIndices[j]);
                }
                else
                {
                    ctm.Vertices[j].Z = vertexPrecision * deltaIndex + GridOrigin(lbZ, ubZ, divZ, gridIndices[j]);
                }
            }
        }
        if (trianglesCount > 0 && reader.ReadInt32() != Identifier.Indices)
        {
            throw new InvalidDataException("Invalid indices section");
        }
        using var trianglesReader = new BinaryReader(Unpack(reader, trianglesCount * 3 * sizeof(int)));
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
        if ((ctm.Flags & Flag.HasNormals) == Flag.HasNormals)
        {
            // TODO: Implement normal decoding
            Unpack(reader, verticesCount * 2 * sizeof(float));
            throw new NotImplementedException();
        }
        for (var i = 0; i < uvMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.UVMaps)
            {
                throw new InvalidDataException("Invalid uv maps section");
            }
            var uvMap = new UvMap()
            {
                Name = ReadString(reader),
                Filename = ReadString(reader),
                Coordinates = new List<UvCoordinate>(verticesCount),
            };
            var uvCoordinatePrecision = reader.ReadSingle();
            using var uvCoordinatesReader = new BinaryReader(Unpack(reader, verticesCount * 2 * sizeof(float)));
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
        for (var i = 0; i < attributeMapsCount; i++)
        {
            if (reader.ReadInt32() != Identifier.AttributeMaps)
            {
                throw new InvalidDataException("Invalid attribute maps section");
            }
            var attributeMap = new AttributeMap()
            {
                Name = ReadString(reader),
                Values = new List<AttributeValue>(verticesCount),
            };
            var attributeValuePrecision = reader.ReadSingle();
            using var attributeValuesReader = new BinaryReader(Unpack(reader, verticesCount * 4 * sizeof(float)));
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

    internal Stream Unpack(BinaryReader reader, int unpackedSize)
    {
        var packedSize = reader.ReadInt32();
        var ms = new MemoryStream(unpackedSize);
        Decoder decoder = new();
        decoder.SetDecoderProperties(reader.ReadBytes(Constants.PackedDataPropsSize));
        decoder.Code(reader.BaseStream, ms, packedSize, unpackedSize, null);
        ms.Position = 0;
        // packedSize += sizeof(int) + Constants.PackedDataPropsSize;
        return ms;
    }

    internal float GridOrigin(float lb, float ub, int div, int g)
    {
        return lb + ((ub - lb) / div * g);
    }

    internal static string ReadString(BinaryReader reader)
    {
        var length = reader.ReadInt32();
        return new string(reader.ReadChars(length));
    }
}
