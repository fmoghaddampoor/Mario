namespace MarioEngine.Core.Editor;

/// <summary>Renders a small overview of the entire level.</summary>
internal sealed class EditorMinimap
{
    public float MiniMapScale { get; set; } = 0.05f;

    public void Render(Camera2D camera, Level level)
    {
        // Render scaled-down level preview
    }
}
