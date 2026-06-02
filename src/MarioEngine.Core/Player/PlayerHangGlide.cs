using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles player hang gliding movement.</summary>
internal sealed class PlayerHangGlide
{
    /// <summary>Whether the player is currently gliding.</summary>
    public bool IsGliding { get; private set; }

    /// <summary>Descent speed while gliding.</summary>
    public float GlideDescentSpeed { get; set; } = 50f;

    /// <summary>Horizontal speed while gliding.</summary>
    public float GlideHorizontalSpeed { get; set; } = 150f;

    /// <summary>Begins gliding.</summary>
    public void StartGlide()
    {
        IsGliding = true;
    }

    /// <summary>Stops gliding.</summary>
    public void StopGlide()
    {
        IsGliding = false;
    }

    /// <summary>Returns the glide velocity based on horizontal input.</summary>
    public Vector2 GetGlideVelocity(float inputX)
    {
        return new Vector2(inputX * GlideHorizontalSpeed, GlideDescentSpeed);
    }
}
