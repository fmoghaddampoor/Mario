namespace MarioEngine.Core.Physics.Player;

using System.Numerics;

/// <summary>
/// Computes wall jump velocity based on the direction of the wall.
/// Provides horizontal push-off and vertical lift.
/// </summary>
internal sealed class WallJump
{
    /// <summary>Gets or sets the horizontal velocity imparted away from the wall.</summary>
    internal float WallJumpVelocityX { get; set; } = 200f;

    /// <summary>Gets or sets the vertical velocity imparted upward.</summary>
    internal float WallJumpVelocityY { get; set; } = -400f;

    /// <summary>Gets or sets the distance in units to check for wall adjacency.</summary>
    internal float WallCheckDistance { get; set; } = 8f;

    /// <summary>
    /// Computes the jump velocity vector based on the wall direction.
    /// </summary>
    /// <param name="wallDirection">-1 for wall on the left, +1 for wall on the right.</param>
    /// <returns>The velocity vector to apply for a wall jump.</returns>
    internal Vector2 GetJumpVelocity(int wallDirection)
    {
        return new Vector2(wallDirection * WallJumpVelocityX, WallJumpVelocityY);
    }
}
