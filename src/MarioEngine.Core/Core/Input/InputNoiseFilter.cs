namespace MarioEngine.Core.Core.Input;

/// <summary>Debounces jittery inputs to prevent unintended repeats.</summary>
internal sealed class InputNoiseFilter
{
    public float DebounceTime { get; set; } = 0.05f;
    private readonly Dictionary<Key, float> _lastPress = new();

    public bool ShouldAccept(Key key, float timeSinceLastPress)
    {
        _lastPress.TryGetValue(key, out var last);
        if (timeSinceLastPress - last < DebounceTime) return false;
        _lastPress[key] = timeSinceLastPress;
        return true;
    }
}
