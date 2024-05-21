using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraPerspectiveSerialization
{
    public static CameraPerspective? Read(GltfReaderContext context)
    {
        float? aspectRatio = null;
        float? yfov = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(aspectRatio)))
            {
                aspectRatio = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(yfov)))
            {
                yfov = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(zfar)))
            {
                zfar = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(znear)))
            {
                znear = ReadFloat(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(CameraPerspective.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<CameraPerspective>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(CameraPerspective.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
            }
            else
            {
                throw new InvalidDataException($"Unknown property: {propertyName}");
            }
        }
        if (yfov == null || znear == null)
        {
            throw new InvalidDataException("CameraPerspective.yfov and CameraPerspective.znear are required properties.");
        }
        if (aspectRatio != null && aspectRatio < Default.ExclusiveMinimum)
        {
            throw new InvalidDataException("CameraPerspective.aspectRatio must be greater than or equal to 0.0.");
        }
        if (yfov < Default.ExclusiveMinimum)
        {
            throw new InvalidDataException("CameraPerspective.yfov must be greater than or equal to 0.0.");
        }
        if (zfar != null && zfar <= znear)
        {
            throw new InvalidDataException("CameraPerspective.zfar must be greater than CameraPerspective.znear.");
        }
        if (znear < Default.ExclusiveMinimum)
        {
            throw new InvalidDataException("CameraPerspective.znear must be greater than or equal to 0.0.");
        }

        return new()
        {
            AspectRatio = aspectRatio,
            YFov = (float)yfov!,
            ZFar = zfar,
            ZNear = (float)znear!,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
