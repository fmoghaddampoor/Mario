using System;

namespace MarioEngine.Core.Player;

/// <summary>Handles charge jump mechanic.</summary>
internal sealed class PlayerChargeJump
{
    /// <summary>Whether the player is currently charging.</summary>
    public bool IsCharging { get; private set; }

    /// <summary>Elapsed charge time in seconds.</summary>
    public float ChargeTime { get; private set; }

    /// <summary>Maximum charge time before full power.</summary>
    public float MaxChargeTime { get; set; } = 1.5f;

    /// <summary>Returns a multiplier between 1.0 and 3.0 based on charge time.</summary>
    public float GetChargeMultiplier()
    {
        var t = Math.Clamp(ChargeTime / MaxChargeTime, 0f, 1f);
        return 1f + t * 2f;
    }

    /// <summary>Begins charging the jump.</summary>
    public void StartCharge()
    {
        IsCharging = true;
        ChargeTime = 0f;
    }

    /// <summary>Releases the charge and returns the current multiplier. Resets state.</summary>
    public float ReleaseCharge()
    {
        var multiplier = GetChargeMultiplier();
        IsCharging = false;
        ChargeTime = 0f;
        return multiplier;
    }

    /// <summary>Accumulates charge time while charging.</summary>
    public void Update(float dt)
    {
        if (!IsCharging) return;
        ChargeTime = Math.Min(ChargeTime + dt, MaxChargeTime);
    }
}
