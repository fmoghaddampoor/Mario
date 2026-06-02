namespace MarioEngine.Core.Editor;

/// <summary>Paints tiles onto the level grid.</summary>
internal sealed class EditorPaintTool : EditorTool
{
    public int TileId { get; set; }
    public int BrushSize { get; set; } = 1;

    public EditorPaintTool()
    {
        Name = "Paint";
        Icon = "P";
    }

    public void SetTile(int tileId) => TileId = tileId;

    public override void OnMouseClick(Vector2 worldPos)
    {
        for (int x = 0; x < BrushSize; x++)
            for (int y = 0; y < BrushSize; y++)
            {
                // Place tile at snapped position
            }
    }
}
