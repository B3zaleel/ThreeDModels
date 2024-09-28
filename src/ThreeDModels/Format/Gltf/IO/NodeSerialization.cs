using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Node? node)
    {
        if (node == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (node.Matrix != null && (node.Rotation != null || node.Scale != null || node.Translation != null))
        {
            throw new InvalidDataException("Node.matrix should not be defined if any one of Node.rotation, Node.scale, and Node.translation have been defined.");
        }
        if (node.Matrix != null && node.Matrix.Length != Default.Node_Matrix.Length)
        {
            throw new InvalidDataException($"Node.matrix must have {Default.Node_Matrix.Length} elements.");
        }
        if (node.Translation != null && node.Translation.Length != Default.Node_Translation.Length)
        {
            throw new InvalidDataException($"Node.translation must have {Default.Node_Translation.Length} elements.");
        }
        if (node.Rotation != null && node.Rotation.Length != Default.Node_Rotation.Length)
        {
            throw new InvalidDataException($"Node.rotation must have {Default.Node_Rotation.Length} elements.");
        }
        if (node.Scale != null && node.Scale.Length != Default.Node_Scale.Length)
        {
            throw new InvalidDataException($"Node.scale must have {Default.Node_Scale.Length} elements.");
        }
        if (node.Skin != null && node.Mesh == null)
        {
            throw new InvalidDataException("Node.mesh must be defined if Node.skin has been defined.");
        }
        if (node.Weights != null && node.Mesh == null)
        {
            throw new InvalidDataException("Node.mesh must be defined if Node.weights has been defined.");
        }
        if (node.Children != null && node.Children.Count == 0)
        {
            throw new InvalidDataException("Node.children must have at least one element.");
        }
        jsonWriter.WriteStartObject();
        if (node.Camera != null)
        {
            jsonWriter.WriteNumber(ElementName.Node.Camera, node.Camera.Value);
        }
        if (node.Children != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Children);
            WriteIntegerList(ref jsonWriter, context, node.Children);
        }
        if (node.Skin != null)
        {
            jsonWriter.WriteNumber(ElementName.Node.Skin, node.Skin.Value);
        }
        if (node.Matrix != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Matrix);
            WriteFloatList(ref jsonWriter, context, node.Matrix.ToList());
        }
        if (node.Mesh != null)
        {
            jsonWriter.WriteNumber(ElementName.Node.Mesh, node.Mesh.Value);
        }
        if (node.Rotation != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Rotation);
            WriteFloatList(ref jsonWriter, context, node.Rotation.ToList());
        }
        if (node.Scale != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Scale);
            WriteFloatList(ref jsonWriter, context, node.Scale.ToList());
        }
        if (node.Translation != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Translation);
            WriteFloatList(ref jsonWriter, context, node.Translation.ToList());
        }
        if (node.Weights != null)
        {
            jsonWriter.WritePropertyName(ElementName.Node.Weights);
            WriteFloatList(ref jsonWriter, context, node.Weights);
        }
        if (node.Name != null)
        {
            jsonWriter.WriteString(ElementName.Accessor.Name, node.Name);
        }
        if (node.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<Node>(ref jsonWriter, context, node.Extensions);
        }
        if (node.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, node.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
