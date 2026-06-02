using System;

namespace MarioEngine.Core.PowerUps;

public sealed class PowerUpIcon
{
    public PowerUpType Type { get; set; }
    public string IconName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    public PowerUpIcon(PowerUpType type, string iconName, string displayName)
    {
        Type = type;
        IconName = iconName;
        DisplayName = displayName;
    }
}
