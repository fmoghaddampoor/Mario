namespace MarioEngine.Core.Physics.Player;

using System.Numerics;

/// <summary>
/// Handles climbing behavior on vines and similar vertical surfaces.
/// </summary>
internal sealed class VineClimb
{
    /// <summary>Gets or sets the vertical climb speed in units/s.</summary>
    internal float ClimbSpeed { get; set; } = 100f;

    /// <summary>Gets or sets a value indicating whether the player is currently climbing a vine.</summary>
    internal bool IsClimbing { get; set; }

    /// <summary>
    /// Starts climbing. Sets the climbing flag.
    /// </summary>
    internal void StartClimb()
    {
        IsClimbing = true;
    }

    /// <summary>
    /// Computes the climb velocity based on vertical input.
    /// </summary>
    /// <param name="inputY">Vertical input (-1 = up, 0 = idle, 1 = down).</param>
    /// <returns>Velocity vector for climbing.</returns>
    internal Vector2 GetClimbVelocity(float inputY)
    {
        return new Vector2(0f, inputY * ClimbSpeed);
    }

    /// <summary>
    /// Stops climbing. Clears the climbing flag.
    /// </summary>
    internal void StopClimb()
    {
        IsClimbing = false;
    }
}
