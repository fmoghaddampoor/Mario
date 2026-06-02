using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles the bounce after a ground pound.</summary>
internal sealed class PlayerGroundPoundBounce
{
    /// <summary>Whether the player is currently bouncing.</summary>
    public bool IsBouncing { get; private set; }

    /// <summary>Upward velocity applied on bounce.</summary>
    public float BounceVelocity { get; set; } = -500f;

    /// <summary>Begins the bounce.</summary>
    public void StartBounce()
    {
        IsBouncing = true;
    }

    /// <summary>Returns the bounce velocity.</summary>
    public Vector2 GetBounceVelocity()
    {
        return new Vector2(0, BounceVelocity);
    }
}
