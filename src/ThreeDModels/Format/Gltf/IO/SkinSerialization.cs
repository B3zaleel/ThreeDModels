using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class SkinSerialization
{
    public static Skin? Read(GltfReaderContext context)
    {
        int? inverseBindMatrices = null;
        int? skeleton = null;
        List<int>? joints = null;
        string? name = null;
        Dictionary<string, object?>? extensions = null;
        object? extras = null;
        if (context.JsonReader.TokenType == JsonTokenType.PropertyName && context.JsonReader.Read())
        {
        }
        if (context.JsonReader.TokenType == JsonTokenType.Null)
        {
            return null;
        }
        else if (context.JsonReader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidDataException("Failed to find start of property.");
        }
        while (context.JsonReader.Read())
        {
            if (context.JsonReader.TokenType == JsonTokenType.EndObject)
            {
                break;
            }
            var propertyName = context.JsonReader.GetString();
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.InverseBindMatrices)))
            {
                inverseBindMatrices = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.Skeleton)))
            {
                skeleton = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.Joints)))
            {
                joints = ReadIntegerList(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Skin>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Skin.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
