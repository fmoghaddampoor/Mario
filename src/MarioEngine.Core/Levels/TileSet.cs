namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Defines a tileset with tile properties.</summary>
internal sealed class TileSet
{
    /// <summary>Tileset name.</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>First global tile ID.</summary>
    public int FirstGid { get; set; }

    /// <summary>Total number of tiles.</summary>
    public int TileCount { get; set; }

    /// <summary>Number of tile columns.</summary>
    public int Columns { get; set; }

    /// <summary>Per-tile properties keyed by tile ID.</summary>
    public Dictionary<int, string> TileProperties { get; set; } = new();

    /// <summary>Returns whether the given tile ID is solid.</summary>
    public bool IsTileSolid(int tileId)
    {
        return TileProperties.TryGetValue(tileId, out var prop) && prop == "solid";
    }
}
