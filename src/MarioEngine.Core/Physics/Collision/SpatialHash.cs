namespace MarioEngine.Core.Physics.Collision;

using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Spatial hash grid for broad-phase collision detection.
/// Partitions world space into cells to reduce collision pair checks.
/// </summary>
internal sealed class SpatialHash
{
    /// <summary>Size of each grid cell in world units.</summary>
    private readonly float _cellSize;

    /// <summary>Grid buckets keyed by cell hash.</summary>
    private readonly Dictionary<long, List<Aabb>> _cells;

    /// <summary>Initializes a new instance of the <see cref="SpatialHash"/> class.</summary>
    /// <param name="cellSize">Size of each grid cell. Default 100.</param>
    internal SpatialHash(float cellSize = 100f)
    {
        _cellSize = cellSize;
        _cells = new Dictionary<long, List<Aabb>>();
    }

    /// <summary>Clears all cells. Call at the start of each frame.</summary>
    internal void Clear()
    {
        _cells.Clear();
    }

    /// <summary>Inserts an AABB into the grid.</summary>
    /// <param name="aabb">The AABB to insert.</param>
    internal void Insert(Aabb aabb)
    {
        var min = aabb.Min;
        var max = aabb.Max;
        var startX = CellKey(min.X);
        var endX = CellKey(max.X);
        var startY = CellKey(min.Y);
        var endY = CellKey(max.Y);

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                var key = HashKey(x, y);
                if (!_cells.TryGetValue(key, out var bucket))
                {
                    bucket = new List<Aabb>();
                    _cells[key] = bucket;
                }

                bucket.Add(aabb);
            }
        }
    }

    /// <summary>
    /// Finds all potential collision pairs for a given AABB.
    /// </summary>
    /// <param name="aabb">The query AABB.</param>
    /// <returns>List of potentially overlapping AABBs.</returns>
    internal List<Aabb> Query(Aabb aabb)
    {
        var result = new List<Aabb>();
        var min = aabb.Min;
        var max = aabb.Max;
        var startX = CellKey(min.X);
        var endX = CellKey(max.X);
        var startY = CellKey(min.Y);
        var endY = CellKey(max.Y);

        for (var x = startX; x <= endX; x++)
        {
            for (var y = startY; y <= endY; y++)
            {
                var key = HashKey(x, y);
                if (_cells.TryGetValue(key, out var bucket))
                {
                    result.AddRange(bucket);
                }
            }
        }

        return result;
    }

    private static long HashKey(int x, int y) => (long)x << 32 | (uint)y;

    private int CellKey(float coord) => (int)MathF.Floor(coord / _cellSize);
}
