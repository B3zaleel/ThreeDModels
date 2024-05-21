using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.IO;

internal static class MeshPrimitiveSerialization
{
    public static MeshPrimitive? Read(GltfReaderContext context)
    {
        IntegerMap? attributes = null;
        int? indices = null;
        int? material = null;
        int? mode = null;
        List<IntegerMap>? targets = null;
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
            if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Attributes)))
            {
                attributes = IntegerMapSerialization.Read(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Indices)))
            {
                indices = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Material)))
            {
                material = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Mode)))
            {
                mode = ReadInteger(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Indices)))
            {
                targets = ReadList(context, JsonTokenType.StartObject, reader => IntegerMapSerialization.Read(reader)!);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Extensions)))
            {
                extensions = ExtensionsSerialization.Read<MeshPrimitive>(context);
            }
            else if (propertyName == JsonNamingPolicy.CamelCase.ConvertName(nameof(MeshPrimitive.Extras)))
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
