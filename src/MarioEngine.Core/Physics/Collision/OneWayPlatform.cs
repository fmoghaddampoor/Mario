namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Detects and resolves one-way platform collisions.
/// Platforms only collide when the player is falling from above.
/// Player can drop through by pressing down + jump.
/// </summary>
internal static class OneWayPlatform
{
    /// <summary>
    /// Checks if a one-way platform collision should occur.
    /// Only collides when the player's bottom is above the platform
    /// and the player is moving downward.
    /// </summary>
    /// <param name="playerBottom">Player AABB bottom Y.</param>
    /// <param name="playerPrevBottom">Player previous frame bottom Y.</param>
    /// <param name="platformTop">Platform surface top Y.</param>
    /// <param name="playerVelY">Player vertical velocity (positive = down).</param>
    /// <param name="dropThrough">True if the player is intentionally dropping through.</param>
    /// <returns>True if the player should land on the platform.</returns>
    internal static bool ShouldCollide(
        float playerBottom,
        float playerPrevBottom,
        float platformTop,
        float playerVelY,
        bool dropThrough)
    {
        if (dropThrough)
        {
            return false;
        }

        // Player must be above the platform and moving down
        return playerPrevBottom <= platformTop + 2f && playerVelY >= 0f;
    }
}
