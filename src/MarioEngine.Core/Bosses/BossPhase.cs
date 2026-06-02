using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossPhase
{
    public event Action? OnStart;
    public event Action? OnEnd;

    public int HealthThreshold { get; set; }
    public string Name { get; set; } = string.Empty;
    public float Duration { get; set; }

    public void OnPhaseStart()
    {
        OnStart?.Invoke();
    }

    public void OnPhaseEnd()
    {
        OnEnd?.Invoke();
    }
}
