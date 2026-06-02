namespace MarioEngine.Core.GamePlayer;

using System;
using System.Numerics;

/// <summary>Handles climbing behaviour on vines and vertical surfaces.</summary>
public sealed class PlayerClimbing
{
    /// <summary>Gets whether the player is currently climbing.</summary>
    public bool IsClimbing { get; private set; }

    /// <summary>Gets or sets the climb movement speed.</summary>
    public float ClimbSpeed { get; set; } = 100f;

    /// <summary>Gets the position of the vine being climbed.</summary>
    public Vector2 VinePosition { get; private set; }

    /// <summary>Starts climbing at the given vine position.</summary>
    /// <param name="vinePosition">The world position of the climbable vine.</param>
    public void StartClimb(Vector2 vinePosition)
    {
        IsClimbing = true;
        VinePosition = vinePosition;
    }

    /// <summary>Stops climbing, releasing the player from the vine.</summary>
    public void StopClimb()
    {
        IsClimbing = false;
    }

    /// <summary>Calculates the climb velocity based on vertical input.</summary>
    /// <param name="inputY">Vertical input direction (-1 to 1).</param>
    /// <returns>The velocity vector for climbing.</returns>
    public Vector2 GetClimbVelocity(float inputY)
    {
        return new Vector2(0f, inputY * ClimbSpeed);
    }
}
