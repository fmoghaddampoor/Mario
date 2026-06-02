namespace MarioEngine.Core.Physics.Player;

using System.Numerics;

/// <summary>
/// Handles the ground pound mechanic where the player slams downward.
/// </summary>
internal sealed class GroundPound
{
    /// <summary>Gets or sets a value indicating whether the player is currently executing a ground pound.</summary>
    internal bool IsPounding { get; set; }

    /// <summary>Gets or sets the downward velocity applied during the pound in units/s.</summary>
    internal float PoundVelocity { get; set; } = 1200f;

    /// <summary>
    /// Initiates the ground pound. Sets the pounding flag.
    /// </summary>
    internal void StartPound()
    {
        IsPounding = true;
    }

    /// <summary>
    /// Updates the ground pound state and returns the current velocity.
    /// Resets the flag each frame so it can be stopped externally.
    /// </summary>
    /// <param name="dt">Delta time in seconds (unused in velocity calc but reserved for future use).</param>
    /// <returns>The downward pound velocity vector.</returns>
    internal Vector2 UpdatePound(float dt)
    {
        _ = dt;
        return IsPounding ? new Vector2(0f, PoundVelocity) : Vector2.Zero;
    }
}
