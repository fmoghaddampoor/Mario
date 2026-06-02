namespace MarioEngine.Core.Editor;

/// <summary>Selects, copies, pastes, and deletes tiles/entities.</summary>
internal sealed class EditorSelectTool : EditorTool
{
    public Rect SelectionRect { get; set; }
    public bool IsSelecting { get; set; }

    public EditorSelectTool()
    {
        Name = "Select";
        Icon = "S";
    }

    public override void OnMouseClick(Vector2 worldPos)
    {
        IsSelecting = true;
    }

    public void Copy() { }
    public void Paste() { }
    public void Delete() { }
}

internal struct Rect
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}
