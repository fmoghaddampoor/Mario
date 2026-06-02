namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Axis-Aligned Bounding Box for collision detection.
/// Defined by a center position and half-extents.
/// </summary>
internal readonly struct Aabb
{
    /// <summary>Center of the AABB in world units.</summary>
    internal readonly Vector2 Center;

    /// <summary>Half-width and half-height of the AABB.</summary>
    internal readonly Vector2 HalfExtents;

    /// <summary>Initializes a new instance of the <see cref="Aabb"/> struct.</summary>
    /// <param name="center">Center position.</param>
    /// <param name="halfExtents">Half-width and half-height.</param>
    internal Aabb(Vector2 center, Vector2 halfExtents)
    {
        Center = center;
        HalfExtents = halfExtents;
    }

    /// <summary>Gets the minimum corner (left, top).</summary>
    internal Vector2 Min => Center - HalfExtents;

    /// <summary>Gets the maximum corner (right, bottom).</summary>
    internal Vector2 Max => Center + HalfExtents;

    /// <summary>
    /// Creates an AABB from minimum and maximum corners.
    /// </summary>
    /// <param name="minX">Minimum X coordinate.</param>
    /// <param name="minY">Minimum Y coordinate.</param>
    /// <param name="maxX">Maximum X coordinate.</param>
    /// <param name="maxY">Maximum Y coordinate.</param>
    /// <returns>A new AABB covering the specified range.</returns>
    internal static Aabb FromMinMax(float minX, float minY, float maxX, float maxY)
    {
        var center = new Vector2((minX + maxX) * 0.5f, (minY + maxY) * 0.5f);
        var half = new Vector2((maxX - minX) * 0.5f, (maxY - minY) * 0.5f);
        return new Aabb(center, half);
    }

    /// <summary>
    /// Tests whether this AABB overlaps another AABB.
    /// </summary>
    /// <param name="other">The other AABB to test against.</param>
    /// <returns>True if the AABBs overlap.</returns>
    internal bool Overlaps(Aabb other)
    {
        var d = other.Center - Center;
        var overlapX = MathF.Abs(d.X) <= HalfExtents.X + other.HalfExtents.X;
        var overlapY = MathF.Abs(d.Y) <= HalfExtents.Y + other.HalfExtents.Y;
        return overlapX && overlapY;
    }

    /// <summary>
    /// Tests whether this AABB contains a point.
    /// </summary>
    /// <param name="point">The point to test.</param>
    /// <returns>True if the point is inside the AABB.</returns>
    internal bool Contains(Vector2 point)
    {
        var d = point - Center;
        return MathF.Abs(d.X) <= HalfExtents.X && MathF.Abs(d.Y) <= HalfExtents.Y;
    }
}
