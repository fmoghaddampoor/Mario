namespace MarioEngine.Core.Core;

/// <summary>Represents a single achievement.</summary>
internal sealed class Achievement
{
    public string Id { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public bool Unlocked { get; set; }
    public DateTime? UnlockedAt { get; set; }
    public string IconName { get; init; } = string.Empty;
}
