using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class AnimationChannelTargetSerialization
{
    public static AnimationChannelTarget? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? node = null;
        string? path = null;
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
            if (propertyName == nameof(node))
            {
                node = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(path))
            {
                path = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<AnimationChannelTarget>(ref jsonReader, context);
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
        if (path is null)
        {
            throw new InvalidDataException("AnimationChannelTarget.path is a required property.");
        }
        return new()
        {
            Node = node,
            Path = path!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, AnimationChannelTarget? animationChannelTarget)
    {
        if (animationChannelTarget is null)
        {
            return;
        }
        jsonWriter.WriteStartObject();
        if (animationChannelTarget.Node is not null)
        {
            jsonWriter.WriteNumber(ElementName.AnimationChannelTarget.Node, (int)animationChannelTarget.Node);
        }
        jsonWriter.WriteString(ElementName.AnimationChannelTarget.Path, animationChannelTarget.Path);
        if (animationChannelTarget.Extensions is not null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<AnimationChannelTarget>(ref jsonWriter, context, animationChannelTarget.Extensions);
        }
        if (animationChannelTarget.Extras is not null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, animationChannelTarget.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
