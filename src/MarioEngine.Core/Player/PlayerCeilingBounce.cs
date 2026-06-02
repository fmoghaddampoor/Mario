using System;
using System.Numerics;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles downward bounce off ceilings.</summary>
public sealed class PlayerCeilingBounce
{
    /// <summary>Whether the player is currently bouncing off a ceiling.</summary>
    public bool IsBouncing { get; private set; }

    /// <summary>Downward velocity applied on ceiling bounce.</summary>
    public float BounceVelocityY { get; set; } = 300f;

    /// <summary>Returns the bounce velocity.</summary>
    public Vector2 GetBounceVelocity()
    {
        return new Vector2(0, BounceVelocityY);
    }

    /// <summary>Begins the ceiling bounce.</summary>
    public void StartBounce()
    {
        IsBouncing = true;
    }
}
