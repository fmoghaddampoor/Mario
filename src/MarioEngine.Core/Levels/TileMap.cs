namespace MarioEngine.Core.Levels;

/// <summary>Represents a tile-based map.</summary>
internal sealed class TileMap
{
    /// <summary>Width in tiles.</summary>
    public int Width { get; }

    /// <summary>Height in tiles.</summary>
    public int Height { get; }

    /// <summary>Flat tile data array.</summary>
    public int[] Tiles { get; }

    /// <summary>Size of each tile in pixels.</summary>
    public float TileSize { get; } = 32f;

    /// <summary>Creates a new tile map.</summary>
    public TileMap(int width, int height)
    {
        Width = width;
        Height = height;
        Tiles = new int[width * height];
    }

    /// <summary>Gets the tile ID at the given coordinates.</summary>
    public int GetTile(int x, int y)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return -1;
        return Tiles[y * Width + x];
    }

    /// <summary>Sets the tile ID at the given coordinates.</summary>
    public void SetTile(int x, int y, int tileId)
    {
        if (x < 0 || x >= Width || y < 0 || y >= Height)
            return;
        Tiles[y * Width + x] = tileId;
    }

    /// <summary>Returns true if the tile at (x, y) is solid.</summary>
    public bool IsSolid(int x, int y)
    {
        return GetTile(x, y) >= 0;
    }
}
