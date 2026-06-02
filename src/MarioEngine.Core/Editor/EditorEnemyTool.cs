namespace MarioEngine.Core.Editor;

/// <summary>Places enemy entities in the level.</summary>
internal sealed class EditorEnemyTool : EditorTool
{
    public string EnemyType { get; set; } = "Goomba";

    public EditorEnemyTool()
    {
        Name = "Enemy";
        Icon = "En";
    }

    public override void OnMouseClick(Vector2 worldPos)
    {
        // Place enemy of EnemyType at worldPos
    }
}
