using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class NodeSerialization
{
    public static Node? Read(GltfReaderContext context)
    {
        int? camera = null;
        int? children = null;
        int? skin = null;
        float[]? matrix = null;
        int? mesh = null;
        float[]? rotation = null;
        float[]? scale = null;
        float[]? translation = null;
        List<float>? weights = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Camera)))
            {
                camera = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Children)))
            {
                children = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Skin)))
            {
                skin = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Matrix)))
            {
                matrix = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Mesh)))
            {
                mesh = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Rotation)))
            {
                rotation = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Scale)))
            {
                scale = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Translation)))
            {
                translation = ReadFloatList(context)?.ToArray();
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Weights)))
            {
                weights = ReadFloatList(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Node>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Node.Extras)))
            {
                extras = ExtrasSerialization.Read(context);
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
