namespace MarioEngine.Core.Player;

using System;

/// <summary>Manages the player's breath meter for underwater sections.</summary>
internal sealed class PlayerBreathMeter
{
    /// <summary>Gets or sets the maximum breath capacity in seconds.</summary>
    public float MaxBreath { get; set; } = 30f;

    /// <summary>Gets the current breath level.</summary>
    public float CurrentBreath { get; private set; }

    /// <summary>Gets or sets the rate at which breath depletes underwater (units per second).</summary>
    public float BreathDrainRate { get; set; } = 2f;

    /// <summary>Gets or sets the rate at which breath recharges in air (units per second).</summary>
    public float BreathRechargeRate { get; set; } = 5f;

    /// <summary>Gets whether the player is currently drowning (breath depleted).</summary>
    public bool IsDrowning { get; private set; }

    /// <summary>Updates the breath meter based on whether the player is underwater.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    /// <param name="isUnderwater"><c>true</c> if the player is submerged.</param>
    public void Update(float dt, bool isUnderwater)
    {
        if (isUnderwater)
        {
            CurrentBreath -= BreathDrainRate * dt;
            if (CurrentBreath <= 0f)
            {
                CurrentBreath = 0f;
                IsDrowning = true;
            }
        }
        else
        {
            CurrentBreath += BreathRechargeRate * dt;
            if (CurrentBreath >= MaxBreath)
            {
                CurrentBreath = MaxBreath;
                IsDrowning = false;
            }
        }
    }

    /// <summary>Fully refills the breath meter.</summary>
    public void Refill()
    {
        CurrentBreath = MaxBreath;
        IsDrowning = false;
    }
}
