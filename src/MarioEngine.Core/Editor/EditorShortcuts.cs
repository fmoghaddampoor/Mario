namespace MarioEngine.Core.Editor;

/// <summary>Maps keyboard keys to editor actions.</summary>
internal static class EditorShortcuts
{
    public static Dictionary<Key, string> Shortcuts { get; } = new()
    {
        { Key.G, "ToggleGrid" },
        { Key.S, "SaveLevel" },
        { Key.L, "LoadLevel" },
        { Key.Z, "Undo" },
        { Key.Y, "Redo" },
        { Key.D1, "SelectTool0" },
        { Key.D2, "SelectTool1" },
        { Key.D3, "SelectTool2" },
        { Key.D4, "SelectTool3" },
        { Key.D5, "SelectTool4" },
        { Key.D6, "SelectTool5" },
        { Key.D7, "SelectTool6" },
        { Key.C, "Copy" },
        { Key.V, "Paste" },
        { Key.Delete, "Delete" },
    };

    public static string GetActionForKey(Key key) =>
        Shortcuts.TryGetValue(key, out var action) ? action : "";
}

internal enum Key
{
    G, S, L, Z, Y, D1, D2, D3, D4, D5, D6, D7, C, V, Delete
}
