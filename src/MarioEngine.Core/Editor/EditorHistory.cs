namespace MarioEngine.Core.Editor;

/// <summary>Manages undo/redo history for editor actions.</summary>
internal sealed class EditorHistory
{
    public Stack<EditorAction> UndoStack { get; } = new();
    public Stack<EditorAction> RedoStack { get; } = new();
    public int MaxHistory { get; set; } = 100;

    public void PushAction(EditorAction action)
    {
        UndoStack.Push(action);
        RedoStack.Clear();
        if (UndoStack.Count > MaxHistory)
        {
            var temp = UndoStack.ToArray();
            UndoStack.Clear();
            for (int i = temp.Length - 1; i > 0; i--)
                UndoStack.Push(temp[i]);
        }
    }

    public void Undo()
    {
        if (UndoStack.Count == 0) return;
        var action = UndoStack.Pop();
        action.Undo();
        RedoStack.Push(action);
    }

    public void Redo()
    {
        if (RedoStack.Count == 0) return;
        var action = RedoStack.Pop();
        action.Execute();
        UndoStack.Push(action);
    }
}
