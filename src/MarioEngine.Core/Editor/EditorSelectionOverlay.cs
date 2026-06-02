namespace MarioEngine.Core.Editor;

/// <summary>Highlights selected tiles and entities in the editor.</summary>
internal sealed class EditorSelectionOverlay
{
    public Rect SelectionBounds { get; set; }
    public uint HighlightColor { get; set; } = 0x44FFFFFF;

    public void Render()
    {
        // Render highlight overlay
    }
}
