using SevenZip.Compression.LZMA;
using ThreeDModels.Format.Ctm.Elements;

namespace ThreeDModels.Format.Ctm.IO;

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
                    RawReader.Read(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            case CompressionMethod.MG1:
                {
                    Mg1Reader.Read(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            case CompressionMethod.MG2:
                {
                    Mg2Reader.Read(reader, verticesCount, trianglesCount, uvMapsCount, attributeMapsCount, ref ctm);
                    break;
                }
            default:
                {
                    throw new InvalidDataException($"Unsupported compression method {method}");
                }
        }
        return ctm;
    }

    internal static Stream UnpackLzmaStream(BinaryReader reader, int unpackedSize)
    {
        var packedSize = reader.ReadInt32();
        var ms = new MemoryStream(unpackedSize);
        Decoder decoder = new();
        decoder.SetDecoderProperties(reader.ReadBytes(Constants.PackedDataPropsSize));
        decoder.Code(reader.BaseStream, ms, packedSize, unpackedSize, null);
        ms.Position = 0;
        return ms;
    }

    internal static string ReadString(BinaryReader reader)
    {
        var length = reader.ReadInt32();
        return new string(reader.ReadChars(length));
    }
}
