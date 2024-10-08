using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class CameraOrthographicSerialization
{
    public static CameraOrthographic? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        float? xmag = null;
        float? ymag = null;
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
            if (propertyName == nameof(xmag))
            {
                xmag = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(ymag))
            {
                ymag = ReadFloat(ref jsonReader);
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
                extensions = ExtensionsSerialization.Read<CameraOrthographic>(ref jsonReader, context);
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
            XMag = (float)xmag!,
            YMag = (float)ymag!,
            ZFar = (float)zfar!,
            ZNear = (float)znear!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, CameraOrthographic? cameraOrthographic)
    {
        if (cameraOrthographic is null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WriteNumber(ElementName.CameraOrthographic.XMag, cameraOrthographic.XMag);
        jsonWriter.WriteNumber(ElementName.CameraOrthographic.YMag, cameraOrthographic.YMag);
        jsonWriter.WriteNumber(ElementName.CameraOrthographic.ZFar, cameraOrthographic.ZFar);
        jsonWriter.WriteNumber(ElementName.CameraOrthographic.ZNear, cameraOrthographic.ZNear);
        if (cameraOrthographic.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<CameraOrthographic>(ref jsonWriter, context, cameraOrthographic.Extensions);
        }
        if (cameraOrthographic.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, cameraOrthographic.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
