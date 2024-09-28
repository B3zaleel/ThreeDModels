using System.Collections;

namespace ThreeDModels.Format.Gltf.Elements;

/// <summary>
/// Represents all collection of string-integer key-value pairs.
/// </summary>
public class IntegerMap : IEnumerable, IGltfProperty
{
    /// <summary>
    /// The collection of keys and their integer values.
    /// </summary>
    private readonly Dictionary<string, int>? _map = [];
    public Dictionary<string, object?>? Extensions { get; set; }
    public JsonElement? Extras { get; set; }

    public void Add(string key, int value)
    {
        _map!.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _map!.ContainsKey(key);
    }

    public int this[string key] => _map![key];

    public bool Remove(string key)
    {
        return _map!.Remove(key);
    }

    public void Clear()
    {
        _map!.Clear();
    }

    public IEnumerator GetEnumerator()
    {
        yield return _map!.GetEnumerator();
    }
}
