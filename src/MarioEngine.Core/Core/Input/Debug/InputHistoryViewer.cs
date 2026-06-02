namespace MarioEngine.Core.Core.Input.Debug;

/// <summary>Records and displays recent input actions for debugging.</summary>
internal sealed class InputHistoryViewer
{
    private readonly Queue<(float Time, string Action)> _history = new();
    public int MaxHistory { get; set; } = 100;

    public void Record(string action)
    {
        _history.Enqueue(((float)DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond, action));
        while (_history.Count > MaxHistory) _history.Dequeue();
    }

    public void Render()
    {
        foreach (var entry in _history)
        {
            // Draw each entry in debug overlay
        }
    }
}
