namespace MarioEngine.Core.Core;

/// <summary>Manages game achievements and unlock events.</summary>
internal sealed class AchievementManager
{
    public List<Achievement> Achievements { get; } = new();
    public event Action<Achievement>? OnAchievementUnlocked;

    public void Unlock(string id)
    {
        var ach = Achievements.Find(a => a.Id == id);
        if (ach == null || ach.Unlocked) return;
        ach.Unlocked = true;
        ach.UnlockedAt = DateTime.UtcNow;
        OnAchievementUnlocked?.Invoke(ach);
    }
}
