using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Ignis : BossBase
{
    public float FistRange { get; set; } = 200f;
    public float MeteorCount { get; set; } = 5f;

    public Ignis()
    {
        BossName = "Ignis";
        MaxHealth = 1100;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 30 });
    }
}
