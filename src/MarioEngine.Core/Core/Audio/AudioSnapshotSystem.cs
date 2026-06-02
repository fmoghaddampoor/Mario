namespace MarioEngine.Core.Core.Audio;

/// <summary>Saves and restores audio bus snapshots for scene transitions.</summary>
internal sealed class AudioSnapshotSystem
{
    private readonly Dictionary<string, AudioBusSystem> _snapshots = new();

    public void SaveSnapshot(string name)
    {
        _snapshots[name] = new AudioBusSystem();
    }

    public void RestoreSnapshot(string name)
    {
        if (_snapshots.TryGetValue(name, out var snapshot))
        {
            // Apply snapshot bus states
        }
    }
}
