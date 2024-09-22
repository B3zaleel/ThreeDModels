using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class NodeSerialization
{
    public static Node? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? camera = null;
        List<int>? children = null;
        int? skin = null;
        float[]? matrix = null;
        int? mesh = null;
        float[]? rotation = null;
        float[]? scale = null;
        float[]? translation = null;
        List<float>? weights = null;
        string? name = null;
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
            if (propertyName == nameof(camera))
            {
                camera = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(children))
            {
                children = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(skin))
            {
                skin = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(matrix))
            {
                matrix = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(mesh))
            {
                mesh = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(rotation))
            {
                rotation = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(scale))
            {
                scale = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(translation))
            {
                translation = ReadFloatList(ref jsonReader, context)?.ToArray();
            }
            else if (propertyName == nameof(weights))
            {
                weights = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<Node>(ref jsonReader, context);
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
        if (matrix != null && (rotation != null || scale != null || translation != null))
        {
            throw new InvalidDataException("Node.matrix should not be defined if any one of Node.rotation, Node.scale, and Node.translation have been defined.");
        }
        if (matrix != null && matrix.Length != Default.Node_Matrix.Length)
        {
            throw new InvalidDataException($"Node.matrix must have {Default.Node_Matrix.Length} elements.");
        }
        if (translation != null && translation.Length != Default.Node_Translation.Length)
        {
            throw new InvalidDataException($"Node.translation must have {Default.Node_Translation.Length} elements.");
        }
        if (rotation != null && rotation.Length != Default.Node_Rotation.Length)
        {
            throw new InvalidDataException($"Node.rotation must have {Default.Node_Rotation.Length} elements.");
        }
        if (scale != null && scale.Length != Default.Node_Scale.Length)
        {
            throw new InvalidDataException($"Node.scale must have {Default.Node_Scale.Length} elements.");
        }
        if (skin != null && mesh == null)
        {
            throw new InvalidDataException("Node.mesh must be defined if Node.skin has been defined.");
        }
        if (weights != null && mesh == null)
        {
            throw new InvalidDataException("Node.mesh must be defined if Node.weights has been defined.");
        }
        if (children != null && children.Count == 0)
        {
            throw new InvalidDataException("Node.children must have at least one element.");
        }

        return new()
        {
            Camera = camera,
            Children = children,
            Skin = skin,
            Matrix = matrix,
            Mesh = mesh,
            Rotation = rotation,
            Scale = scale,
            Translation = translation,
            Weights = weights!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
