namespace MarioEngine.Core.Core.Input;

/// <summary>One-button accessibility mode that cycles through actions.</summary>
internal sealed class AccessibilitySingleSwitch
{
    public bool IsSingleSwitchMode { get; set; }
    public Key AssignedKey { get; set; } = Key.Space;
    public event Action? OnPress;

    public void Press()
    {
        if (IsSingleSwitchMode) OnPress?.Invoke();
    }
}
