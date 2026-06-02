namespace MarioEngine.Core.Save;

/// <summary>Placeholder for future cloud save synchronization.</summary>
internal sealed class CloudSaveManager
{
    public bool IsCloudAvailable { get; private set; }

    public void SyncToCloud()
    {
        if (!IsCloudAvailable) return;
    }

    public void SyncFromCloud()
    {
        if (!IsCloudAvailable) return;
    }

    public void Initialize(bool available)
    {
        IsCloudAvailable = available;
    }
}
