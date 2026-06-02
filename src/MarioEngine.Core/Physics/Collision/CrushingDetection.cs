namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Detects whether a moving AABB would crush an entity by pinning it against a surface.
/// </summary>
internal static class CrushingDetection
{
    /// <summary>
    /// Determines if moving <paramref name="movingAabb"/> by <paramref name="moveDelta"/>
    /// would crush the entity occupying <paramref name="playerAabb"/>.
    /// A crush occurs when the moving body overlaps the player's space
    /// after the translation.
    /// </summary>
    /// <param name="playerAabb">The player's current AABB.</param>
    /// <param name="movingAabb">The moving obstacle's AABB.</param>
    /// <param name="moveDelta">The movement vector applied to the obstacle.</param>
    /// <returns>True if the player would be crushed.</returns>
    internal static bool WouldCrush(Aabb playerAabb, Aabb movingAabb, Vector2 moveDelta)
    {
        var moved = new Aabb(movingAabb.Center + moveDelta, movingAabb.HalfExtents);
        return playerAabb.Overlaps(moved);
    }
}
