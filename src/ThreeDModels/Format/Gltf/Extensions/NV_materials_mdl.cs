using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that enables using MDL materials.
/// </summary>
public class NV_materials_mdl : IGltfProperty
{
    /// <summary>
    /// The list of all MDL modules.
    /// </summary>
    public List<NvMaterialsMdlModule>? Modules { get; set; }
    /// <summary>
    /// The list of all function calls.
    /// </summary>
    public List<NvMaterialsMdlFunctionCall>? FunctionCalls { get; set; }
    /// <summary>
    /// The list of all BSDF measurements.
    /// </summary>
    public List<NvMaterialsMdlBsdfMeasurement>? BsdfMeasurements { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an MDL module.
/// </summary>
public class NvMaterialsMdlModule : IGltfRootProperty
{
    /// <summary>
    /// The URI (or IRI) of the MDL module.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// The ID of the bufferView containing the MDL module.
    /// </summary>
    public int? BufferView { get; set; }
    /// <summary>
    /// The MDL module's media type.
    /// </summary>
    public string? MimeType { get; set; }
    /// <summary>
    /// Relative path of the module.
    /// </summary>
    public string? ModulePath { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents a function call with its list of arguments.
/// </summary>
public class NvMaterialsMdlFunctionCall : IGltfRootProperty
{
    /// <summary>
    /// The ID of the containing module.
    /// </summary>
    public int? Module { get; set; }
    /// <summary>
    /// The unqualified name of the function.
    /// </summary>
    public required string FunctionName { get; set; }
    /// <summary>
    /// The return type of the function.
    /// </summary>
    public required NvMaterialsMdlType Type { get; set; }
    /// <summary>
    /// A list of named value and/or function call arguments.
    /// </summary>
    public List<NvMaterialsMdlFunctionCallArgument>? Arguments { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an MDL type describing either a built-in or user-defined type, or an array of a built-in or user-defined type.
/// </summary>
public class NvMaterialsMdlType : IGltfProperty
{
    /// <summary>
    /// The ID of the containing module.
    /// </summary>
    public int? Module { get; set; }
    /// <summary>
    /// The unqualified name of the type.
    /// </summary>
    public required string TypeName { get; set; }
    /// <summary>
    /// The array size.
    /// </summary>
    public int? ArraySize { get; set; }
    /// <summary>
    /// The name of the type modifier.
    /// </summary>
    public string? Modifier { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents a named function call argument.
/// </summary>
public class NvMaterialsMdlFunctionCallArgument : IGltfProperty
{
    /// <summary>
    /// The name of the named argument.
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// The type of the value argument.
    /// </summary>
    public NvMaterialsMdlType? Type { get; set; }
    /// <summary>
    /// The ID of a function call.
    /// </summary>
    public int? FunctionCall { get; set; }
    /// <summary>
    /// The literal value of the value argument.
    /// </summary>
    public object? Value { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents A BSDF measurement (MBSDF) as defined in the MDL Language Specification.
/// </summary>
public class NvMaterialsMdlBsdfMeasurement : IGltfRootProperty
{
    /// <summary>
    /// The URI (or IRI) of the MBSDF.
    /// </summary>
    public string? Uri { get; set; }
    /// <summary>
    /// The ID of the bufferView containing the MBSDF.
    /// </summary>
    public int? BufferView { get; set; }
    /// <summary>
    /// The BSDF measurement's media type.
    /// </summary>
    public string? MimeType { get; set; }
    public string? Name { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

/// <summary>
/// Represents an object that enables using MDL materials.
/// </summary>
public class NvMaterialsMdlMaterial : IGltfProperty
{
    /// <summary>
    /// The index of the MDL function call.
    /// </summary>
    public required int FunctionCall { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class NvMaterialsMdlExtension : IGltfExtension
{
    public string Name => nameof(NV_materials_mdl);

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        if (parentType == typeof(Gltf))
        {
            List<NvMaterialsMdlModule>? modules = null;
            List<NvMaterialsMdlFunctionCall>? functionCalls = null;
            List<NvMaterialsMdlBsdfMeasurement>? bsdfMeasurements = null;
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
                if (propertyName == nameof(modules))
                {
                    modules = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => NvMaterialsMdlModuleSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(functionCalls))
                {
                    functionCalls = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => NvMaterialsMdlFunctionCallSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(bsdfMeasurements))
                {
                    bsdfMeasurements = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => NvMaterialsMdlBsdfMeasurementSerialization.Read(ref reader, ctx)!);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<NV_materials_mdl>(ref jsonReader, context);
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
            if (modules != null && modules.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.modules[i] must not be empty.");
            }
            if (functionCalls != null && functionCalls.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.functionCalls[i] must not be empty.");
            }
            if (bsdfMeasurements != null && bsdfMeasurements.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.bsdfMeasurements[i] must not be empty.");
            }
            return new NV_materials_mdl()
            {
                Modules = modules,
                FunctionCalls = functionCalls,
                BsdfMeasurements = bsdfMeasurements,
                Extensions = extensions,
                Extras = extras,
            };
        }
        else if (parentType == typeof(Material))
        {
            int? functionCall = null;
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
                if (propertyName == nameof(functionCall))
                {
                    functionCall = ReadInteger(ref jsonReader);
                }
                else if (propertyName == nameof(extensions))
                {
                    extensions = ExtensionsSerialization.Read<NvMaterialsMdlMaterial>(ref jsonReader, context);
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
            if (functionCall == null)
            {
                throw new InvalidDataException("NV_materials_mdl.functionCall is a required property.");
            }
            return new NvMaterialsMdlMaterial()
            {
                FunctionCall = (int)functionCall,
                Extensions = extensions,
                Extras = extras,
            };
        }
        throw new InvalidDataException("NV_materials_mdl must be used in a Gltf root or a Material.");
    }
}

public class NvMaterialsMdlModuleSerialization
{
    public static NvMaterialsMdlModule? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        int? bufferView = null;
        string? mimeType = null;
        string? modulePath = null;
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
            if (propertyName == nameof(uri))
            {
                uri = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(mimeType))
            {
                mimeType = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(modulePath))
            {
                modulePath = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<NvMaterialsMdlModule>(ref jsonReader, context);
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
        if (uri != null && bufferView != null)
        {
            throw new InvalidDataException("Both NV_materials_mdl.modules[i].uri and NV_materials_mdl.modules[i].bufferView cannot be defined.");
        }
        if (bufferView != null && (mimeType == null || modulePath == null))
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].bufferView requires NV_materials_mdl.modules[i].mimeType and NV_materials_mdl.modules[i].modulePath.");
        }
        if (uri != null && uri.StartsWith("data:") && modulePath == null)
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].uri is a data URI and requires NV_materials_mdl.modules[i].modulePath.");
        }
        return new()
        {
            Uri = uri,
            BufferView = bufferView,
            MimeType = mimeType,
            ModulePath = modulePath,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class NvMaterialsMdlFunctionCallSerialization
{
    public static NvMaterialsMdlFunctionCall? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? module = null;
        string? functionName = null;
        NvMaterialsMdlType? type = null;
        List<NvMaterialsMdlFunctionCallArgument>? arguments = null;
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
            if (propertyName == nameof(module))
            {
                module = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(functionName))
            {
                functionName = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(type))
            {
                type = NvMaterialsMdlTypeSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(arguments))
            {
                arguments = ReadObjectList(ref jsonReader, context, (ref Utf8JsonReader reader, GltfReaderContext ctx) => NvMaterialsMdlFunctionCallArgumentSerialization.Read(ref reader, ctx)!);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<NvMaterialsMdlFunctionCall>(ref jsonReader, context);
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
        if (functionName == null)
        {
            throw new InvalidDataException("NV_materials_mdl.functionCalls[i].functionName is a required property.");
        }
        if (type == null)
        {
            throw new InvalidDataException("NV_materials_mdl.functionCalls[i].type is a required property.");
        }
        return new()
        {
            Module = module,
            FunctionName = functionName,
            Type = type,
            Arguments = arguments,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class NvMaterialsMdlTypeSerialization
{
    public static NvMaterialsMdlType? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        int? module = null;
        string? typeName = null;
        int? arraySize = null;
        string? modifier = null;
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
            if (propertyName == nameof(module))
            {
                module = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(typeName))
            {
                typeName = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(arraySize))
            {
                arraySize = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(modifier))
            {
                modifier = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<NvMaterialsMdlType>(ref jsonReader, context);
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
        if (typeName == null)
        {
            throw new InvalidDataException("typeName is a required property.");
        }
        return new()
        {
            Module = module,
            TypeName = typeName,
            ArraySize = arraySize,
            Modifier = modifier,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class NvMaterialsMdlFunctionCallArgumentSerialization
{
    public static NvMaterialsMdlFunctionCallArgument? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? name = null;
        NvMaterialsMdlType? type = null;
        int? functionCall = null;
        object? value = null;
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
            else if (propertyName == nameof(type))
            {
                type = NvMaterialsMdlTypeSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(functionCall))
            {
                functionCall = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(value))
            {
                value = JsonSerialization.Read(ref jsonReader, context);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<NvMaterialsMdlFunctionCallArgument>(ref jsonReader, context);
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
        if (functionCall == null)
        {
            throw new InvalidDataException("NV_materials_mdl.functionCall is a required property.");
        }
        return new()
        {
            Name = name,
            Type = type,
            FunctionCall = functionCall,
            Value = value,
            Extensions = extensions,
            Extras = extras,
        };
    }
}

public class NvMaterialsMdlBsdfMeasurementSerialization
{
    public static NvMaterialsMdlBsdfMeasurement? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context)
    {
        string? uri = null;
        int? bufferView = null;
        string? mimeType = null;
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
            if (propertyName == nameof(uri))
            {
                uri = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(mimeType))
            {
                mimeType = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(name))
            {
                name = ReadString(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<NvMaterialsMdlBsdfMeasurement>(ref jsonReader, context);
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
        if (uri != null && bufferView != null)
        {
            throw new InvalidDataException("Both NV_materials_mdl.bsdfMeasurements[i].uri and NV_materials_mdl.bsdfMeasurements[i].bufferView cannot be defined.");
        }
        if (bufferView != null && mimeType == null)
        {
            throw new InvalidDataException("NV_materials_mdl.bsdfMeasurements[i].bufferView requires NV_materials_mdl.bsdfMeasurements[i].mimeType.");
        }
        return new()
        {
            Uri = uri,
            BufferView = bufferView,
            MimeType = mimeType,
            Name = name,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
