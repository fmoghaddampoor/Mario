namespace MarioEngine.Core.Editor;

/// <summary>Palette of selectable tiles, enemies, and items.</summary>
internal sealed class EditorPalette
{
    public List<PaletteItem> Items { get; } = new();
    public int SelectedIndex { get; set; }

    public EditorPalette()
    {
        Items.Add(new PaletteItem { Name = "Brick", TileId = 1, Type = "tile", Category = "Terrain" });
        Items.Add(new PaletteItem { Name = "Question", TileId = 2, Type = "tile", Category = "Terrain" });
        Items.Add(new PaletteItem { Name = "Goomba", TileId = -1, Type = "enemy", Category = "Enemies" });
        Items.Add(new PaletteItem { Name = "Koopa", TileId = -1, Type = "enemy", Category = "Enemies" });
        Items.Add(new PaletteItem { Name = "Mushroom", TileId = -1, Type = "item", Category = "Power-ups" });
    }

    public void Show()
    {
        for (int i = 0; i < Items.Count; i++)
            Console.WriteLine($"{i}: {Items[i].Name} [{Items[i].Category}]");
    }

    public PaletteItem? GetSelected()
    {
        if (SelectedIndex < 0 || SelectedIndex >= Items.Count) return null;
        return Items[SelectedIndex];
    }
}
