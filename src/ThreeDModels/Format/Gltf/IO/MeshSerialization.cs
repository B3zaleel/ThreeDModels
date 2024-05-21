using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshSerialization
{
    public static Mesh? Read(GltfReaderContext context)
    {
        List<MeshPrimitive>? primitives = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Primitives)))
            {
                primitives = ReadList(context, JsonTokenType.StartObject, reader => MeshPrimitiveSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Weights)))
            {
                weights = ReadFloatList(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Name)))
            {
                name = ReadString(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Mesh>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Extras)))
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
            Primitives = primitives!,
            Weights = weights!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
