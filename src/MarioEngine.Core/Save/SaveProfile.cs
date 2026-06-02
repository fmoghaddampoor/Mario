namespace MarioEngine.Core.Save;

/// <summary>Represents a player profile with save history.</summary>
internal sealed class SaveProfile
{
    public string ProfileName { get; set; } = "";
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public TimeSpan TotalPlayTime { get; set; }
    public List<SaveData> RecentSaves { get; set; } = new();

    public void AddRecentSave(SaveData data)
    {
        RecentSaves.Insert(0, data);
        if (RecentSaves.Count > 5)
            RecentSaves.RemoveAt(RecentSaves.Count - 1);
    }
}
