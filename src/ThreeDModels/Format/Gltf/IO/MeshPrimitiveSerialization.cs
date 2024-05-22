using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshPrimitiveSerialization
{
    public static MeshPrimitive? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        IntegerMap? attributes = null;
        int? indices = null;
        int? material = null;
        int? mode = null;
        List<IntegerMap>? targets = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Attributes)))
            {
                attributes = IntegerMapSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Indices)))
            {
                indices = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Material)))
            {
                material = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Mode)))
            {
                mode = ReadInteger(ref jsonReader);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Indices)))
            {
                targets = ReadList(ref jsonReader, context, JsonTokenType.StartObject, (ref Utf8JsonReader reader, GltfReaderContext ctx) => IntegerMapSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MeshPrimitive>(ref jsonReader, context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Extras)))
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
            Attributes = attributes!,
            Indices = indices,
            Material = material,
            Mode = mode ?? Default.MeshPrimitive_Mode,
            Targets = targets,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
