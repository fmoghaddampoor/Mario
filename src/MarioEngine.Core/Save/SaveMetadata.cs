namespace MarioEngine.Core.Save;

/// <summary>Stores metadata associated with a save slot.</summary>
internal sealed class SaveMetadata
{
    public int Slot { get; set; }
    public int TotalSaves { get; set; }
    public DateTime LastSaveTime { get; set; }
    public TimeSpan TotalPlayTime { get; set; }
    public string Version { get; set; } = "1.0";

    public void Update(int slot, float playTime)
    {
        Slot = slot;
        TotalSaves++;
        LastSaveTime = DateTime.UtcNow;
        TotalPlayTime = TimeSpan.FromSeconds(playTime);
    }
}
