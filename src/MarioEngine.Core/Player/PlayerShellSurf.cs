using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles shell surfing movement.</summary>
internal sealed class PlayerShellSurf
{
    /// <summary>Whether the player is currently surfing.</summary>
    public bool IsSurfing { get; private set; }

    /// <summary>Horizontal speed while surfing.</summary>
    public float SurfSpeed { get; set; } = 350f;

    /// <summary>Begins shell surfing.</summary>
    public void StartSurf()
    {
        IsSurfing = true;
    }

    /// <summary>Stops shell surfing.</summary>
    public void StopSurf()
    {
        IsSurfing = false;
    }

    /// <summary>Returns the surf velocity based on horizontal input.</summary>
    public Vector2 GetSurfVelocity(float inputX)
    {
        return new Vector2(inputX * SurfSpeed, 0);
    }
}
