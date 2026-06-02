namespace MarioEngine.Core.Editor;

/// <summary>Base class for all editor tools.</summary>
internal abstract class EditorTool
{
    public string Name { get; set; } = "";
    public string Icon { get; set; } = "";

    public virtual void OnActivate(Level level) { }
    public virtual void OnDeactivate() { }
    public virtual void OnMouseClick(Vector2 worldPos) { }
}
