namespace MarioEngine.Core.Editor;

/// <summary>Camera controller for the level editor viewport.</summary>
internal sealed class EditorCamera
{
    public Vector2 Position { get; set; }
    public float Zoom { get; set; } = 1f;
    public float MoveSpeed { get; set; } = 500f;

    public void Move(Vector2 direction, float dt)
    {
        Position = new Vector2
        {
            X = Position.X + direction.X * MoveSpeed * dt / Zoom,
            Y = Position.Y + direction.Y * MoveSpeed * dt / Zoom
        };
    }

    public void ZoomIn()
    {
        Zoom = Math.Min(Zoom * 1.1f, 8f);
    }

    public void ZoomOut()
    {
        Zoom = Math.Max(Zoom / 1.1f, 1f);
    }
}
