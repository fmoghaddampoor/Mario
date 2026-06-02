namespace MarioEngine.Core.Core;

/// <summary>Manages console command history with up/down navigation.</summary>
internal sealed class CommandHistory
{
    public List<string> History { get; } = new();
    public int MaxHistory { get; set; } = 50;
    public int CurrentIndex { get; private set; } = -1;

    public void Add(string command)
    {
        History.Add(command);
        if (History.Count > MaxHistory) History.RemoveAt(0);
        CurrentIndex = History.Count;
    }

    public string? GetPrevious()
    {
        if (History.Count == 0) return null;
        CurrentIndex = Math.Max(0, CurrentIndex - 1);
        return History[CurrentIndex];
    }

    public string? GetNext()
    {
        if (History.Count == 0) return null;
        CurrentIndex = Math.Min(History.Count, CurrentIndex + 1);
        return CurrentIndex >= History.Count ? null : History[CurrentIndex];
    }
}
