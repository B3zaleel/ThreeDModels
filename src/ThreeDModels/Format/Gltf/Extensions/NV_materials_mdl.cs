using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonWriterHelpers;

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

    public void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, Type parentType, object? element)
    {
        if (parentType == typeof(Gltf))
        {
            var nvMaterialsMdl = (NV_materials_mdl?)element;
            if (nvMaterialsMdl == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            if (nvMaterialsMdl.Modules != null && nvMaterialsMdl.Modules.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.modules must not be empty.");
            }
            if (nvMaterialsMdl.FunctionCalls != null && nvMaterialsMdl.FunctionCalls.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.functionCalls must not be empty.");
            }
            if (nvMaterialsMdl.BsdfMeasurements != null && nvMaterialsMdl.BsdfMeasurements.Count == 0)
            {
                throw new InvalidDataException("NV_materials_mdl.bsdfMeasurements must not be empty.");
            }
            jsonWriter.WriteStartObject();
            if (nvMaterialsMdl.Modules != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.Modules);
                WriteList(ref jsonWriter, context, nvMaterialsMdl.Modules, NvMaterialsMdlModuleSerialization.Write);
            }
            if (nvMaterialsMdl.FunctionCalls != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.FunctionCalls);
                WriteList(ref jsonWriter, context, nvMaterialsMdl.FunctionCalls, NvMaterialsMdlFunctionCallSerialization.Write);
            }
            if (nvMaterialsMdl.BsdfMeasurements != null)
            {
                jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.BsdfMeasurements);
                WriteList(ref jsonWriter, context, nvMaterialsMdl.BsdfMeasurements, NvMaterialsMdlBsdfMeasurementSerialization.Write);
            }
            if (nvMaterialsMdl.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<NV_materials_mdl>(ref jsonWriter, context, nvMaterialsMdl.Extensions);
            }
            if (nvMaterialsMdl.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdl.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else if (parentType == typeof(Material))
        {
            var nvMaterialsMdlMaterial = (NvMaterialsMdlMaterial?)element;
            if (nvMaterialsMdlMaterial == null)
            {
                jsonWriter.WriteNullValue();
                return;
            }
            jsonWriter.WriteStartObject();
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.FunctionCall);
            jsonWriter.WriteNumberValue(nvMaterialsMdlMaterial.FunctionCall);
            if (nvMaterialsMdlMaterial.Extensions != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
                ExtensionsSerialization.Write<NvMaterialsMdlMaterial>(ref jsonWriter, context, nvMaterialsMdlMaterial.Extensions);
            }
            if (nvMaterialsMdlMaterial.Extras != null)
            {
                jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
                JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlMaterial.Extras);
            }
            jsonWriter.WriteEndObject();
        }
        else
        {
            throw new InvalidDataException("NV_materials_mdl must be used in a Gltf root or a Material.");
        }
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, NvMaterialsMdlModule? nvMaterialsMdlModule)
    {
        if (nvMaterialsMdlModule == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (nvMaterialsMdlModule.Uri != null && nvMaterialsMdlModule.BufferView != null)
        {
            throw new InvalidDataException("Both NV_materials_mdl.modules[i].uri and NV_materials_mdl.modules[i].bufferView cannot be defined.");
        }
        if (nvMaterialsMdlModule.BufferView != null && (nvMaterialsMdlModule.MimeType == null || nvMaterialsMdlModule.ModulePath == null))
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].bufferView requires NV_materials_mdl.modules[i].mimeType and NV_materials_mdl.modules[i].modulePath.");
        }
        if (nvMaterialsMdlModule.Uri != null && nvMaterialsMdlModule.Uri.StartsWith("data:") && nvMaterialsMdlModule.ModulePath == null)
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].uri is a data URI and requires NV_materials_mdl.modules[i].modulePath.");
        }
        if (nvMaterialsMdlModule.Uri != null && nvMaterialsMdlModule.BufferView != null)
        {
            throw new InvalidDataException("Both NV_materials_mdl.modules[i].uri and NV_materials_mdl.modules[i].bufferView cannot be defined.");
        }
        if (nvMaterialsMdlModule.BufferView != null && (nvMaterialsMdlModule.MimeType == null || nvMaterialsMdlModule.ModulePath == null))
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].bufferView requires NV_materials_mdl.modules[i].mimeType and NV_materials_mdl.modules[i].modulePath.");
        }
        if (nvMaterialsMdlModule.Uri != null && nvMaterialsMdlModule.Uri.StartsWith("data:") && nvMaterialsMdlModule.ModulePath == null)
        {
            throw new InvalidDataException("NV_materials_mdl.modules[i].uri is a data URI and requires NV_materials_mdl.modules[i].modulePath.");
        }
        jsonWriter.WriteStartObject();
        if (nvMaterialsMdlModule.Uri != null)
        {
            jsonWriter.WritePropertyName(ElementName.Buffer.Uri);
            jsonWriter.WriteStringValue(nvMaterialsMdlModule.Uri);
        }
        if (nvMaterialsMdlModule.BufferView != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.BufferView);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlModule.BufferView);
        }
        if (nvMaterialsMdlModule.MimeType != null)
        {
            jsonWriter.WritePropertyName(ElementName.Image.MimeType);
            jsonWriter.WriteStringValue(nvMaterialsMdlModule.MimeType);
        }
        if (nvMaterialsMdlModule.ModulePath != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlModule.ModulePath);
            jsonWriter.WriteStringValue(nvMaterialsMdlModule.ModulePath);
        }
        if (nvMaterialsMdlModule.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(nvMaterialsMdlModule.Name);
        }
        if (nvMaterialsMdlModule.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<NvMaterialsMdlModule>(ref jsonWriter, context, nvMaterialsMdlModule.Extensions);
        }
        if (nvMaterialsMdlModule.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlModule.Extras);
        }
        jsonWriter.WriteEndObject();
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, NvMaterialsMdlFunctionCall? nvMaterialsMdlFunctionCall)
    {
        if (nvMaterialsMdlFunctionCall == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (nvMaterialsMdlFunctionCall.Module != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlFunctionCall.Module);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlFunctionCall.Module);
        }
        jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlFunctionCall.FunctionName);
        jsonWriter.WriteStringValue(nvMaterialsMdlFunctionCall.FunctionName);
        if (nvMaterialsMdlFunctionCall.Type != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Type);
            NvMaterialsMdlTypeSerialization.Write(ref jsonWriter, context, nvMaterialsMdlFunctionCall.Type);
        }
        if (nvMaterialsMdlFunctionCall.Arguments != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlFunctionCall.Arguments);
            WriteList(ref jsonWriter, context, nvMaterialsMdlFunctionCall.Arguments, NvMaterialsMdlFunctionCallArgumentSerialization.Write);
        }
        if (nvMaterialsMdlFunctionCall.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(nvMaterialsMdlFunctionCall.Name);
        }
        if (nvMaterialsMdlFunctionCall.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<ADOBE_materials_clearcoat_tint>(ref jsonWriter, context, nvMaterialsMdlFunctionCall.Extensions);
        }
        if (nvMaterialsMdlFunctionCall.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlFunctionCall.Extras);
        }
        jsonWriter.WriteEndObject();
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, NvMaterialsMdlType? nvMaterialsMdlType)
    {
        if (nvMaterialsMdlType == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (nvMaterialsMdlType.Module != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlFunctionCall.Module);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlType.Module);
        }
        jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlType.TypeName);
        jsonWriter.WriteStringValue(nvMaterialsMdlType.TypeName);
        if (nvMaterialsMdlType.ArraySize != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlType.ArraySize);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlType.ArraySize);
        }
        if (nvMaterialsMdlType.Modifier != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlType.Modifier);
            jsonWriter.WriteStringValue(nvMaterialsMdlType.Modifier);
        }
        if (nvMaterialsMdlType.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<NvMaterialsMdlType>(ref jsonWriter, context, nvMaterialsMdlType.Extensions);
        }
        if (nvMaterialsMdlType.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlType.Extras);
        }
        jsonWriter.WriteEndObject();
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, NvMaterialsMdlFunctionCallArgument? nvMaterialsMdlFunctionCallArgument)
    {
        if (nvMaterialsMdlFunctionCallArgument == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        jsonWriter.WriteStartObject();
        if (nvMaterialsMdlFunctionCallArgument.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(nvMaterialsMdlFunctionCallArgument.Name);
        }
        if (nvMaterialsMdlFunctionCallArgument.Type != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Type);
            NvMaterialsMdlTypeSerialization.Write(ref jsonWriter, context, nvMaterialsMdlFunctionCallArgument.Type);
        }
        if (nvMaterialsMdlFunctionCallArgument.FunctionCall != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.FunctionCall);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlFunctionCallArgument.FunctionCall);
        }
        if (nvMaterialsMdlFunctionCallArgument.Value != null)
        {
            jsonWriter.WritePropertyName(ElementName.Extensions.NV_materials_mdl.NvMaterialsMdlFunctionCallArgument.Value);
            JsonSerialization.Write(ref jsonWriter, context, (Elements.JsonElement?)nvMaterialsMdlFunctionCallArgument.Value);
        }
        if (nvMaterialsMdlFunctionCallArgument.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<NvMaterialsMdlFunctionCallArgument>(ref jsonWriter, context, nvMaterialsMdlFunctionCallArgument.Extensions);
        }
        if (nvMaterialsMdlFunctionCallArgument.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlFunctionCallArgument.Extras);
        }
        jsonWriter.WriteEndObject();
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

    public static void Write(ref Utf8JsonWriter jsonWriter, GltfWriterContext context, NvMaterialsMdlBsdfMeasurement? nvMaterialsMdlBsdfMeasurement)
    {
        if (nvMaterialsMdlBsdfMeasurement == null)
        {
            jsonWriter.WriteNullValue();
            return;
        }
        if (nvMaterialsMdlBsdfMeasurement.Uri != null && nvMaterialsMdlBsdfMeasurement.BufferView != null)
        {
            throw new InvalidDataException("Both NV_materials_mdl.bsdfMeasurements[i].uri and NV_materials_mdl.bsdfMeasurements[i].bufferView cannot be defined.");
        }
        if (nvMaterialsMdlBsdfMeasurement.BufferView != null && nvMaterialsMdlBsdfMeasurement.MimeType == null)
        {
            throw new InvalidDataException("NV_materials_mdl.bsdfMeasurements[i].bufferView requires NV_materials_mdl.bsdfMeasurements[i].mimeType.");
        }
        jsonWriter.WriteStartObject();
        if (nvMaterialsMdlBsdfMeasurement.Uri != null)
        {
            jsonWriter.WritePropertyName(ElementName.Buffer.Uri);
            jsonWriter.WriteStringValue(nvMaterialsMdlBsdfMeasurement.Uri);
        }
        if (nvMaterialsMdlBsdfMeasurement.BufferView != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.BufferView);
            jsonWriter.WriteNumberValue((int)nvMaterialsMdlBsdfMeasurement.BufferView);
        }
        if (nvMaterialsMdlBsdfMeasurement.MimeType != null)
        {
            jsonWriter.WritePropertyName(ElementName.Image.MimeType);
            jsonWriter.WriteStringValue(nvMaterialsMdlBsdfMeasurement.MimeType);
        }
        if (nvMaterialsMdlBsdfMeasurement.Name != null)
        {
            jsonWriter.WritePropertyName(ElementName.Accessor.Name);
            jsonWriter.WriteStringValue(nvMaterialsMdlBsdfMeasurement.Name);
        }
        if (nvMaterialsMdlBsdfMeasurement.Extensions != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extensions);
            ExtensionsSerialization.Write<NvMaterialsMdlBsdfMeasurement>(ref jsonWriter, context, nvMaterialsMdlBsdfMeasurement.Extensions);
        }
        if (nvMaterialsMdlBsdfMeasurement.Extras != null)
        {
            jsonWriter.WritePropertyName(ElementName.Gltf.Extras);
            JsonSerialization.Write(ref jsonWriter, context, nvMaterialsMdlBsdfMeasurement.Extras);
        }
        jsonWriter.WriteEndObject();
    }
}
