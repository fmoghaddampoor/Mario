using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Zephyr : BossBase
{
    public float CycloneRadius { get; set; } = 100f;
    public float LightningDelay { get; set; } = 1f;

    public Zephyr()
    {
        BossName = "Zephyr";
        MaxHealth = 1000;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 50 });
    }
}
