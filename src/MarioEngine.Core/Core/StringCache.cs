namespace MarioEngine.Core.Core;

/// <summary>Deduplicates repeated string allocations.</summary>
internal sealed class StringCache
{
    private readonly Dictionary<string, string> _cache = new(StringComparer.Ordinal);

    public string GetOrAdd(string value)
    {
        if (_cache.TryGetValue(value, out var cached)) return cached;
        _cache[value] = value;
        return value;
    }
}
