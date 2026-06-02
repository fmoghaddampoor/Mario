namespace MarioEngine.Core.Editor;

/// <summary>Erases tiles from the level grid.</summary>
internal sealed class EditorEraseTool : EditorTool
{
    public int BrushSize { get; set; } = 1;

    public EditorEraseTool()
    {
        Name = "Erase";
        Icon = "E";
    }

    public override void OnMouseClick(Vector2 worldPos)
    {
        for (int x = 0; x < BrushSize; x++)
            for (int y = 0; y < BrushSize; y++)
            {
                // Remove tile at position
            }
    }
}
