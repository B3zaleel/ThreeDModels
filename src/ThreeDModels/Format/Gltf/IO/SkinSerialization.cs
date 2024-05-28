using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SkinSerialization
{
    public static Skin? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? inverseBindMatrices = null;
        int? skeleton = null;
        List<int>? joints = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (jsonReader.TokenType == JsonTokenType.PropertyName && jsonReader.Read())
        {
        }
        if (jsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (jsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (jsonReader.Read())
        {
            if (jsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = jsonReader.GetString();
            if (propertyName == nameof(inverseBindMatrices))
            {
                inverseBindMatrices = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(skeleton))
            {
                skeleton = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(joints))
            {
                joints = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Skin>(ref jsonReader, context);
            }
            else if (propertyName == nameof(extras))
            {
                extras = JsonSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        return new()
        {
            InverseBindMatrices = inverseBindMatrices,
            Skeleton = skeleton,
            Joints = joints!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
