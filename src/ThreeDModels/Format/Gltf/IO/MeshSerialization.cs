using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshSerialization
{
    public static Mesh? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<MeshPrimitive>? primitives = null;
        List<float>? weights = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Primitives)))
            {
                primitives = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => MeshPrimitiveSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Weights)))
            {
                weights = ReadFloatList(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Name)))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<Mesh>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(Mesh.Extras)))
            {
                extras = ExtrasSerialization.Read(ref jsonReader, context);
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
