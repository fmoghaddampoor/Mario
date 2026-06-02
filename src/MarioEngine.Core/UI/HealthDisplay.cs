namespace MarioEngine.Core.UI;

/// <summary>Health display as hearts or segments with animated damage flash.</summary>
internal sealed class HealthDisplay
{
    /// <summary>Maximum health value.</summary>
    public int MaxHealth { get; set; } = 3;

    /// <summary>Current health value.</summary>
    public int CurrentHealth { get; private set; }

    /// <summary>Updates the displayed health.</summary>
    public void Update(int health)
    {
        CurrentHealth = health;
    }
}
