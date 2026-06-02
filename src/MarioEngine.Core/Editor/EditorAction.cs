namespace MarioEngine.Core.Editor;

/// <summary>Represents a single undoable editor action.</summary>
internal sealed class EditorAction
{
    public ActionType Type { get; set; }
    public string Description { get; set; } = "";

    public void Execute()
    {
        // Apply the action
    }

    public void Undo()
    {
        // Revert the action
    }
}

internal enum ActionType
{
    PlaceTile,
    RemoveTile,
    PlaceEntity,
    RemoveEntity,
    ModifyProperty
}
