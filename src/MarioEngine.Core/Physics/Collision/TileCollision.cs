namespace MarioEngine.Core.Physics.Collision;

using System.Numerics;

/// <summary>
/// Swept AABB collision detection against a tile map.
/// Resolves collisions by pushing the entity out of overlapping tiles.
/// </summary>
internal static class TileCollision
{
    /// <summary>
    /// Resolves collision between a moving AABB and a grid of solid tiles.
    /// Uses sweep-based detection along each axis independently.
    /// </summary>
    /// <param name="position">Current entity position.</param>
    /// <param name="velocity">Current entity velocity.</param>
    /// <param name="halfSize">Half-width and half-height of the entity AABB.</param>
    /// <param name="tileWidth">Width of each tile in world units.</param>
    /// <param name="tileHeight">Height of each tile in world units.</param>
    /// <param name="isSolid">Function to check if a tile at (col, row) is solid.</param>
    /// <param name="dt">Delta time.</param>
    /// <returns>Adjusted position after tile collision resolution.</returns>
    internal static Vector2 Resolve(
        Vector2 position,
        Vector2 velocity,
        Vector2 halfSize,
        float tileWidth,
        float tileHeight,
        Func<int, int, bool> isSolid,
        float dt)
    {
        var result = position;
        var vel = velocity * dt;

        result.X += vel.X;
        var aabb = CreateAabb(result, halfSize);
        var tiles = GetOverlappingTiles(aabb, tileWidth, tileHeight);

        foreach (var (tx, ty) in tiles)
        {
            if (!isSolid(tx, ty))
            {
                continue;
            }

            var tileAabb = CreateTileAabb(tx, ty, tileWidth, tileHeight);
            if (aabb.Overlaps(tileAabb))
            {
                if (vel.X > 0)
                {
                    result.X = tileAabb.Min.X - halfSize.X;
                }
                else if (vel.X < 0)
                {
                    result.X = tileAabb.Max.X + halfSize.X;
                }
            }
        }

        result.Y += vel.Y;
        aabb = CreateAabb(result, halfSize);
        tiles = GetOverlappingTiles(aabb, tileWidth, tileHeight);

        foreach (var (tx, ty) in tiles)
        {
            if (!isSolid(tx, ty))
            {
                continue;
            }

            var tileAabb = CreateTileAabb(tx, ty, tileWidth, tileHeight);
            if (aabb.Overlaps(tileAabb))
            {
                if (vel.Y > 0)
                {
                    result.Y = tileAabb.Min.Y - halfSize.Y;
                }
                else if (vel.Y < 0)
                {
                    result.Y = tileAabb.Max.Y + halfSize.Y;
                }
            }
        }

        return result;
    }

    private static Aabb CreateAabb(Vector2 pos, Vector2 half)
    {
        return Aabb.FromMinMax(pos.X - half.X, pos.Y - half.Y, pos.X + half.X, pos.Y + half.Y);
    }

    private static Aabb CreateTileAabb(int tx, int ty, float tw, float th)
    {
        return Aabb.FromMinMax(tx * tw, ty * th, (tx + 1) * tw, (ty + 1) * th);
    }

    private static List<(int X, int Y)> GetOverlappingTiles(Aabb aabb, float tileW, float tileH)
    {
        var result = new List<(int, int)>();
        var min = aabb.Min;
        var max = aabb.Max;
        var startX = (int)MathF.Floor(min.X / tileW);
        var endX = (int)MathF.Floor(max.X / tileW);
        var startY = (int)MathF.Floor(min.Y / tileH);
        var endY = (int)MathF.Floor(max.Y / tileH);

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                result.Add((x, y));
            }
        }

        return result;
    }
}
