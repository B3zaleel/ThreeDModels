using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraOrthographicSerialization
{
    public static CameraOrthographic? Read(GltfReaderContext context)
    {
        float? xmag = null;
        float? ymag = null;
        float? zfar = null;
        float? znear = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(xmag)))
            {
                xmag = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(ymag)))
            {
                ymag = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(zfar)))
            {
                zfar = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(znear)))
            {
                znear = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(CameraOrthographic.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<CameraOrthographic>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(CameraOrthographic.Extras)))
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
            XMag = (float)xmag!,
            YMag = (float)ymag!,
            ZFar = (float)zfar!,
            ZNear = (float)znear!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
