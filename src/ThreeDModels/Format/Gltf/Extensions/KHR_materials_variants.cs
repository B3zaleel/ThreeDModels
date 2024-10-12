using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

public class KHR_materials_variants : IGltfProperty
{
    /// <summary>
    /// A list of objects defining a valid material variant.
    /// </summary>
    public required List<KhrMaterialsVariantsVariant> Variants { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object defining a valid material variant.
/// </summary>
public class KhrMaterialsVariantsVariant : IGltfRootProperty
{
    /// <summary>
    /// The name of the material variant.
    /// </summary>
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KHR_materials_variants_mesh_primitive : IGltfProperty
{
    /// <summary>
    /// A list of material to variant mappings.
    /// </summary>
    public required List<KhrMaterialsVariantsMapping> Mappings { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents a mapping of an indexed material to a set of variants.
/// </summary>
public class KhrMaterialsVariantsMapping : IGltfProperty
{
    /// <summary>
    /// A list of variant index values.
    /// </summary>
    public required List<int> Variants { get; set; }
    /// <summary>
    /// The material associated with the set of variants.
    /// </summary>
    public required int Material { get; set; }
    /// <summary>
    /// The user-defined name of this variant material mapping.
    /// </summary>
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class KhrMaterialsVariantsExtension : IGltfExtension
{
    public string Name => nameof(KHR_materials_variants);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<KhrMaterialsVariantsVariant>? variants = null;
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
                if (propertyName == nameof(variants))
                {
                    variants = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => KhrMaterialsVariantsVariantSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<KHR_materials_variants>(ref jsonReader, context);
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
            if (variants == null || variants.Count < 1)
            {
                throw new InvalidDataException("KHR_materials_variants.variants must have at least one variant.");
            }
            return new KHR_materials_variants()
            {
                Variants = variants!,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(MeshPrimitive))
        {
            List<KhrMaterialsVariantsMapping>? mappings = null;
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
                if (propertyName == nameof(mappings))
                {
                    mappings = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => KhrMaterialsVariantsMappingSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<KHR_materials_variants>(ref jsonReader, context);
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
            if (mappings == null || mappings.Count < 1)
            {
                throw new InvalidDataException("KHR_materials_variants.mappings must have at least 1 mapping.");
            }
            return new KHR_materials_variants_mesh_primitive()
            {
                Mappings = mappings!,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("KHR_materials_variants must be used in a either the Gltf root or a MeshPrimitive.");
    }

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType == typeof(Gltf))
        {
            var khrMaterialsVariants = (KHR_materials_variants?)element;
            if (khrMaterialsVariants == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            if (khrMaterialsVariants.Variants.Count < 1)
            {
                throw new InvalidDataException("KHR_materials_variants.variants must have at least one variant.");
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_variants.Variants);
            WriteList(ref jsonWriter, context, khrMaterialsVariants.Variants, KhrMaterialsVariantsVariantSerialization.Write);
            if (khrMaterialsVariants.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<KHR_materials_variants>(ref jsonWriter, context, khrMaterialsVariants.Extensions);
            }
            if (khrMaterialsVariants.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, khrMaterialsVariants.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else if (parentType == typeof(MeshPrimitive))
        {
            var khrMaterialsVariantsMeshPrimitive = (KHR_materials_variants_mesh_primitive?)element;
            if (khrMaterialsVariantsMeshPrimitive == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            if (khrMaterialsVariantsMeshPrimitive.Mappings == null || khrMaterialsVariantsMeshPrimitive.Mappings.Count < 1)
            {
                throw new InvalidDataException("KHR_materials_variants.mappings must have at least 1 mapping.");
            }
            jsonWriter.WriteStartObject();
            if (khrMaterialsVariantsMeshPrimitive.Mappings != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_variants.Mappings);
                WriteList(ref jsonWriter, context, khrMaterialsVariantsMeshPrimitive.Mappings, KhrMaterialsVariantsMappingSerialization.Write);
            }
            if (khrMaterialsVariantsMeshPrimitive.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<KHR_materials_variants_mesh_primitive>(ref jsonWriter, context, khrMaterialsVariantsMeshPrimitive.Extensions);
            }
            if (khrMaterialsVariantsMeshPrimitive.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, khrMaterialsVariantsMeshPrimitive.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else
        {
            throw new InvalidDataException("KHR_materials_variants must be used in a either the Gltf root or a MeshPrimitive.");
        }
    }
}

public class KhrMaterialsVariantsVariantSerialization
{
    public static KhrMaterialsVariantsVariant? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
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
            if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KhrMaterialsVariantsVariant>(ref jsonReader, context);
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
        if (name == null)
        {
            throw new InvalidDataException("KhrMaterialsVariantsVariant.name is a required property.");
        }
        return new()
        {
            Name = name!,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, KhrMaterialsVariantsVariant? khrMaterialsVariantsVariant)
    {
        if (khrMaterialsVariantsVariant == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (khrMaterialsVariantsVariant.Name == null)
        {
            throw new InvalidDataException("KhrMaterialsVariantsVariant.name is a required property.");
        }
        jsonWriter.WritePropertyName(ElementName.Accessor.Name);
        jsonWriter.WriteStringValue(khrMaterialsVariantsVariant.Name);
        if (khrMaterialsVariantsVariant.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KhrMaterialsVariantsVariant>(ref jsonWriter, context, khrMaterialsVariantsVariant.Extensions);
        }
        if (khrMaterialsVariantsVariant.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsVariantsVariant.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}

public class KhrMaterialsVariantsMappingSerialization
{
    public static KhrMaterialsVariantsMapping? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        List<int>? variants = null;
        int? material = null;
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
            if (propertyName == nameof(variants))
            {
                variants = ReadIntegerList(ref jsonReader, context);
            }
            else if (propertyName == nameof(material))
            {
                material = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<KhrMaterialsVariantsMapping>(ref jsonReader, context);
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
        if (variants == null || variants.Count < 1)
        {
            throw new InvalidDataException("KHR_materials_variants.mappings[i].variants must have at least one variant.");
        }
        if (material == null)
        {
            throw new InvalidDataException("KHR_materials_variants.mappings[i].material is a required property.");
        }
        return new()
        {
            Variants = variants!,
            Material = (int)material!,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, KhrMaterialsVariantsMapping? khrMaterialsVariantsMapping)
    {
        if (khrMaterialsVariantsMapping == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (khrMaterialsVariantsMapping.Variants.Count < 1)
        {
            throw new InvalidDataException("KHR_materials_variants.mappings[i].variants must have at least one variant.");
        }
        jsonWriter.WriteStartObject();
        jsonWriter.WritePropertyName(ElementName.Extensions.KHR_materials_variants.Variants);
        WriteIntegerList(ref jsonWriter, context, khrMaterialsVariantsMapping.Variants);
        jsonWriter.WritePropertyName(ElementName.MeshPrimitive.Material);
        jsonWriter.WriteNumberValue(khrMaterialsVariantsMapping.Material);
        if (khrMaterialsVariantsMapping.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(khrMaterialsVariantsMapping.Name);
        }
        if (khrMaterialsVariantsMapping.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<KhrMaterialsVariantsMapping>(ref jsonWriter, context, khrMaterialsVariantsMapping.Extensions);
        }
        if (khrMaterialsVariantsMapping.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, khrMaterialsVariantsMapping.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
