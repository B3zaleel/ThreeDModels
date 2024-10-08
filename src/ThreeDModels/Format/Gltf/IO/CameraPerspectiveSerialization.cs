using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraPerspectiveSerialization
{
    public static CameraPerspective? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float? aspectRatio = null;
        float? yfov = null;
        float? zfar = null;
        float? znear = null;
        Dictionary<string, object?>? extensions = null;
        Elements.JsonElement? extras = null;
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
            if (propertyName == nameof(aspectRatio))
            {
                aspectRatio = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(yfov))
            {
                yfov = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(zfar))
            {
                zfar = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(znear))
            {
                znear = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<CameraPerspective>(ref jsonReader, context);
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, CameraPerspective? cameraPerspective)
    {
        if (cameraPerspective == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (cameraPerspective.AspectRatio != null)
        {
            jsonWriter.WriteNumber(ElementName.CameraPerspective.AspectRatio, (float)cameraPerspective.AspectRatio);
        }
        jsonWriter.WriteNumber(ElementName.CameraPerspective.YFov, cameraPerspective.YFov);
        if (cameraPerspective.ZFar != null)
        {
            jsonWriter.WriteNumber(ElementName.CameraOrthographic.ZFar, (float)cameraPerspective.ZFar);
        }
        jsonWriter.WriteNumber(ElementName.CameraOrthographic.ZNear, cameraPerspective.ZNear);
        if (cameraPerspective.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<CameraPerspective>(ref jsonWriter, context, cameraPerspective.Extensions);
        }
        if (cameraPerspective.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, cameraPerspective.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
