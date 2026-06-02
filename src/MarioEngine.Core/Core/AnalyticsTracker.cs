namespace MarioEngine.Core.Core;

/// <summary>Tracks game event counters for analytics.</summary>
internal sealed class AnalyticsTracker
{
    private readonly Dictionary<string, int> _counters = new();

    public void Increment(string eventName)
    {
        _counters.TryGetValue(eventName, out var val);
        _counters[eventName] = val + 1;
    }

    public void Report()
    {
        foreach (var kv in _counters)
        {
            // Log or upload kv.Key, kv.Value
        }
    }
}
