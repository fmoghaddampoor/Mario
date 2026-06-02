namespace MarioEngine.Core.Player;

using System;
using System.Numerics;

/// <summary>Controls swimming movement, buoyancy, and breath tracking.</summary>
internal sealed class PlayerSwim
{
    /// <summary>Gets whether the player is currently swimming.</summary>
    public bool IsSwimming { get; set; }

    /// <summary>Gets or sets the swim movement speed.</summary>
    public float SwimSpeed { get; set; } = 150f;

    /// <summary>Gets or sets the buoyancy force applied when submerged.</summary>
    public float Buoyancy { get; set; } = -200f;

    /// <summary>Gets the current breath timer value.</summary>
    public float BreathTimer { get; private set; }

    /// <summary>Gets or sets the maximum breath duration in seconds.</summary>
    public float MaxBreath { get; set; } = 30f;

    /// <summary>Gets whether the player is drowning (breath depleted).</summary>
    public bool IsDrowning { get; private set; }

    /// <summary>Calculates the swim velocity based on input and buoyancy.</summary>
    /// <param name="inputX">Horizontal input direction.</param>
    /// <param name="inputY">Vertical input direction.</param>
    /// <param name="dt">Delta time in seconds.</param>
    /// <returns>The resulting velocity vector for swimming.</returns>
    public Vector2 GetSwimVelocity(float inputX, float inputY, float dt)
    {
        float x = inputX * SwimSpeed * dt;
        float y = (inputY * SwimSpeed + Buoyancy) * dt;
        return new Vector2(x, y);
    }

    /// <summary>Updates the breath timer while underwater.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void UpdateBreath(float dt)
    {
        BreathTimer += dt;

        if (BreathTimer >= MaxBreath)
        {
            IsDrowning = true;
        }
    }

    /// <summary>Resets the breath timer and drowning state.</summary>
    public void ResetBreath()
    {
        BreathTimer = 0f;
        IsDrowning = false;
    }
}
