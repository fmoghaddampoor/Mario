namespace MarioEngine.Core.Core;

/// <summary>Watches asset directories for file changes to trigger hot reload.</summary>
internal sealed class HotReloadWatcher : IDisposable
{
    private FileSystemWatcher? _watcher;

    public event Action<string>? OnFileChanged;

    public void Watch(string directory)
    {
        Stop();
        if (!Directory.Exists(directory)) return;
        _watcher = new FileSystemWatcher(directory)
        {
            EnableRaisingEvents = true,
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.LastWrite
        };
        _watcher.Changed += (s, e) => OnFileChanged?.Invoke(e.FullPath);
    }

    public void Stop()
    {
        _watcher?.Dispose();
        _watcher = null;
    }

    public void Dispose() => Stop();
}
