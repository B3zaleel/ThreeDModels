using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraSerialization
{
    public static Camera? Read(GltfReaderContext context)
    {
        CameraOrthographic? orthographic = null;
        CameraPerspective? perspective = null;
        string? type = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Orthographic)))
            {
                orthographic = CameraOrthographicSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Perspective)))
            {
                perspective = CameraPerspectiveSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Type)))
            {
                type = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Camera>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Camera.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
