namespace MarioEngine.Core.Editor;

/// <summary>Properties for the currently edited level.</summary>
internal sealed class EditorLevelProperties
{
    public string LevelName { get; set; } = "New Level";
    public Vector2 PlayerStart { get; set; }
    public Vector2 LevelBounds { get; set; }
    public string BackgroundMusic { get; set; } = "";
    public string Theme { get; set; } = "Grass";

    public void ApplyToLevel(Level level)
    {
        level.Name = LevelName;
    }
}
