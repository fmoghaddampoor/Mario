using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles player backflip movement.</summary>
internal sealed class PlayerBackflip
{
    /// <summary>Whether the player is currently backflipping.</summary>
    public bool IsBackflipping { get; private set; }

    /// <summary>Horizontal velocity of the backflip.</summary>
    public float BackflipVelocityX { get; set; } = 150f;

    /// <summary>Vertical velocity of the backflip.</summary>
    public float BackflipVelocityY { get; set; } = -450f;

    /// <summary>Returns the backflip velocity in the facing direction.</summary>
    public Vector2 GetBackflipVelocity(int facingDirection)
    {
        return new Vector2(BackflipVelocityX * facingDirection, BackflipVelocityY);
    }

    /// <summary>Begins the backflip.</summary>
    public void StartBackflip()
    {
        IsBackflipping = true;
    }
}
