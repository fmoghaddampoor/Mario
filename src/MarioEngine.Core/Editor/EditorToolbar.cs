namespace MarioEngine.Core.Editor;

/// <summary>Toolbar containing selectable editor tools.</summary>
internal sealed class EditorToolbar
{
    public List<EditorTool> Tools { get; } = new();
    public EditorTool? SelectedTool { get; private set; }

    public EditorToolbar()
    {
        Tools.Add(new EditorSelectTool());
        Tools.Add(new EditorPaintTool());
        Tools.Add(new EditorEraseTool());
        Tools.Add(new EditorFillTool());
        Tools.Add(new EditorEnemyTool());
        Tools.Add(new EditorItemTool());
        Tools.Add(new EditorAreaTool());
    }

    public void SelectTool(int index)
    {
        if (index < 0 || index >= Tools.Count) return;
        SelectedTool?.OnDeactivate();
        SelectedTool = Tools[index];
        SelectedTool.OnActivate(new Level());
    }
}

internal sealed class EditorFillTool : EditorTool
{
    public EditorFillTool() { Name = "Fill"; Icon = "F"; }
}

internal sealed class EditorAreaTool : EditorTool
{
    public EditorAreaTool() { Name = "Area"; Icon = "A"; }
}
