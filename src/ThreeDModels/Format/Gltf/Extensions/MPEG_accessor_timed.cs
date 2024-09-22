using System.Text.Json;
using ThreeDModels.Format.Gltf.Elements;
using ThreeDModels.Format.Gltf.IO;
using static ThreeDModels.Format.Gltf.IO.Utf8JsonReaderHelpers;

namespace ThreeDModels.Format.Gltf.Extensions;

/// <summary>
/// Represents an object that specifies timed accessor format formats.
/// </summary>
public class MPEG_accessor_timed : IGltfProperty
{
    /// <summary>
    /// Indicates if the accessor information may change over time.
    /// </summary>
    public required bool Immutable { get; set; }
    /// <summary>
    /// The index in the bufferViews array to a bufferView element that points to the timed accessor information header.
    /// </summary>
    public int? BufferView { get; set; }
    /// <summary>
    /// The frequency at which the renderer is recommended to poll the underlying buffer for new data.
    /// </summary>
    public float SuggestedUpdateRate { get; set; }
    public Dictionary<string, object?>? Extensions { get; set; }
    public Elements.JsonElement? Extras { get; set; }
}

public class MpegAccessorTimedExtension : IGltfExtension
{
    public string Name => nameof(MPEG_accessor_timed);
    public const bool Default_Immutable = true;
    public const float Default_SuggestedUpdateRate = 25.0f;

    public object? Read(ref Utf8JsonReader jsonReader, GltfReaderContext context, Type parentType)
    {
        bool? immutable = null;
        int? bufferView = null;
        float? suggestedUpdateRate = null;
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
            if (propertyName == nameof(immutable))
            {
                immutable = ReadBoolean(ref jsonReader);
            }
            else if (propertyName == nameof(bufferView))
            {
                bufferView = ReadInteger(ref jsonReader);
            }
            else if (propertyName == nameof(suggestedUpdateRate))
            {
                suggestedUpdateRate = ReadFloat(ref jsonReader);
            }
            else if (propertyName == nameof(extensions))
            {
                extensions = ExtensionsSerialization.Read<MPEG_accessor_timed>(ref jsonReader, context);
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
        if ((immutable == null || (bool)immutable) && bufferView != null)
        {
            throw new InvalidDataException("MPEG_accessor_timed.bufferView shouldn't exist if MPEG_accessor_timed.immutable does not exist or is `false`.");
        }
        return new MPEG_accessor_timed()
        {
            Immutable = immutable ?? Default_Immutable,
            BufferView = bufferView,
            SuggestedUpdateRate = suggestedUpdateRate ?? Default_SuggestedUpdateRate,
            Extensions = extensions,
            Extras = extras,
        };
    }
}
