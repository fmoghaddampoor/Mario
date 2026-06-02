using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class SwitchBlock : BlockBase
{
    public string SwitchGroup { get; set; } = string.Empty;
    public bool IsPressed { get; private set; }
    public event Action? OnToggled;

    public SwitchBlock()
    {
        HitsRemaining = 1;
    }

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (IsPressed) return;
        IsPressed = true;
        OnToggled?.Invoke();
    }
}
