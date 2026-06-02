namespace MarioEngine.Core.Editor;

/// <summary>Manages the editor mode lifecycle.</summary>
internal sealed class EditorManager
{
    public bool IsActive { get; private set; }
    public Level? CurrentLevel { get; set; }

    public void Toggle()
    {
        IsActive = !IsActive;
    }

    public void Update(float dt)
    {
        if (!IsActive) return;
    }
}

internal sealed class Level
{
    public string Name { get; set; } = "";
    public int Width { get; set; } = 200;
    public int Height { get; set; } = 15;
}
