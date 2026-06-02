using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles spin jump movement and state.</summary>
internal sealed class PlayerSpinJump
{
    /// <summary>Whether the player is currently spinning.</summary>
    public bool IsSpinning { get; private set; }

    /// <summary>Vertical velocity applied during a spin jump.</summary>
    public float SpinVelocity { get; set; } = -350f;

    /// <summary>Whether the spin jump is active.</summary>
    public bool IsActive => IsSpinning;

    /// <summary>Begins the spin jump.</summary>
    public void StartSpin()
    {
        IsSpinning = true;
    }

    /// <summary>Returns the spin jump velocity combined with current vertical velocity.</summary>
    public Vector2 GetSpinVelocity(float currentVelY)
    {
        return new Vector2(0, currentVelY + SpinVelocity);
    }
}
