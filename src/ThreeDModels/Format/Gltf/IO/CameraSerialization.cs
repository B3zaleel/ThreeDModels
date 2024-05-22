using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraSerialization
{
    public static Camera? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        CameraOrthographic? orthographic = null;
        CameraPerspective? perspective = null;
        string? type = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Orthographic)))
            {
                orthographic = CameraOrthographicSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Perspective)))
            {
                perspective = CameraPerspectiveSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Type)))
            {
                type = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Name)))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Camera>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (type is null)
        {
            throw new InvalidDataException("Camera.type is a required property.");
        }
        if (orthographic is not null && perspective is not null)
        {
            throw new InvalidDataException("Only one of Camera.orthographic and Camera.perspective can be defined.");
        }
        return new()
        {
            Orthographic = orthographic,
            Perspective = perspective,
            Type = type!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
