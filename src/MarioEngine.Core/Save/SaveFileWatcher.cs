namespace MarioEngine.Core.Save;

/// <summary>Watches save files for external modifications.</summary>
internal sealed class SaveFileWatcher : IDisposable
{
    private FileSystemWatcher? _watcher;

    public event Action? OnSaveFileChanged;

    public void Start()
    {
        _watcher = new FileSystemWatcher("Saves", "save_*.json")
        {
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
            EnableRaisingEvents = true
        };
        _watcher.Changed += (_, _) => OnSaveFileChanged?.Invoke();
        _watcher.Created += (_, _) => OnSaveFileChanged?.Invoke();
    }

    public void Stop()
    {
        _watcher?.Dispose();
        _watcher = null;
    }

    public void Dispose()
    {
        Stop();
    }
}
