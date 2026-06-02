using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles player sideflip movement.</summary>
internal sealed class PlayerSideflip
{
    /// <summary>Whether the player is currently sideflipping.</summary>
    public bool IsSideflipping { get; private set; }

    /// <summary>Horizontal velocity of the sideflip.</summary>
    public float SideflipVelocityX { get; set; } = 200f;

    /// <summary>Vertical velocity of the sideflip.</summary>
    public float SideflipVelocityY { get; set; } = -400f;

    /// <summary>Returns the sideflip velocity in the facing direction.</summary>
    public Vector2 GetSideflipVelocity(int facingDirection)
    {
        return new Vector2(SideflipVelocityX * facingDirection, SideflipVelocityY);
    }

    /// <summary>Begins the sideflip.</summary>
    public void StartSideflip()
    {
        IsSideflipping = true;
    }
}
