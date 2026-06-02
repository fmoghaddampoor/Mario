namespace MarioEngine.Core.Editor;

/// <summary>An item in the editor palette (tile, enemy, or item).</summary>
internal sealed class PaletteItem
{
    public string Name { get; set; } = "";
    public int TileId { get; set; } = -1;
    public string Type { get; set; } = "tile";
    public string? Category { get; set; }
}
