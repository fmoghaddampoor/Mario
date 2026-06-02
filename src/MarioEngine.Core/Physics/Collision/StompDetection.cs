namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Detects stomp collisions where the player lands on top of an enemy
/// while falling downward.
/// </summary>
internal static class StompDetection
{
    /// <summary>
    /// Determines whether the player is stomping an enemy from above.
    /// True when the player is above the enemy and falling downward.
    /// </summary>
    /// <param name="playerAabb">The player's current AABB.</param>
    /// <param name="enemyAabb">The enemy's current AABB.</param>
    /// <param name="playerVelY">Player's vertical velocity (positive = downward).</param>
    /// <returns>True if the player can stomp the enemy.</returns>
    internal static bool IsStomp(Aabb playerAabb, Aabb enemyAabb, float playerVelY)
    {
        if (playerVelY <= 0f)
        {
            return false;
        }

        var playerBottom = playerAabb.Max.Y;
        var enemyTop = enemyAabb.Min.Y;

        if (playerBottom < enemyTop)
        {
            return false;
        }

        return playerAabb.Overlaps(enemyAabb);
    }
}
