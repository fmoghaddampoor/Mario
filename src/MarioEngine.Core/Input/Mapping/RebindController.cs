namespace MarioEngine.Core.Input.Mapping;

using Silk.NET.Input;

internal sealed class RebindController
{
    public bool IsRebinding { get; private set; }
    public InputAction? TargetAction { get; private set; }

    public void StartRebind(InputAction action)
    {
        IsRebinding = true;
        TargetAction = action;
    }

    public void CancelRebind()
    {
        IsRebinding = false;
        TargetAction = null;
    }

    public bool TryCompleteRebind(Key pressedKey)
    {
        if (!IsRebinding || TargetAction is null) return false;

        TargetAction.Keys.Clear();
        TargetAction.Keys.Add(pressedKey);
        IsRebinding = false;
        TargetAction = null;
        return true;
    }
}
