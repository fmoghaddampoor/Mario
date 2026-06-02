namespace MarioEngine.Core.Editor;

/// <summary>Renders and manages the editor grid overlay.</summary>
internal sealed class EditorGrid
{
    public float GridSize { get; set; } = 32f;
    public bool ShowGrid { get; set; } = true;

    public void Render(Camera2D camera)
    {
        // Render grid lines based on camera
    }

    public Vector2 SnapToGrid(Vector2 position)
    {
        return new Vector2
        {
            X = MathF.Floor(position.X / GridSize) * GridSize,
            Y = MathF.Floor(position.Y / GridSize) * GridSize
        };
    }
}

internal sealed class Camera2D { }
