namespace MarioEngine.Core.Editor;

/// <summary>Places items and power-ups in the level.</summary>
internal sealed class EditorItemTool : EditorTool
{
    public string ItemType { get; set; } = "Mushroom";

    public EditorItemTool()
    {
        Name = "Item";
        Icon = "I";
    }

    public override void OnMouseClick(Vector2 worldPos)
    {
        // Place item of ItemType at worldPos
    }
}
