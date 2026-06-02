namespace MarioEngine.Core.Levels;

/// <summary>Represents a secret area within a level.</summary>
internal sealed class SecretArea
{
    /// <summary>Entrance position.</summary>
    public Vector2 EntrancePosition { get; set; }

    /// <summary>Exit position.</summary>
    public Vector2 ExitPosition { get; set; }

    /// <summary>Power-up required to access this area.</summary>
    public string RequiredPowerUp { get; set; } = string.Empty;

    /// <summary>Returns true if the player can access this secret area.</summary>
    public bool IsAccessible(Player player)
    {
        return string.IsNullOrEmpty(RequiredPowerUp);
    }
}
