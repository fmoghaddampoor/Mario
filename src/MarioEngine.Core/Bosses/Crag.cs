using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Crag : BossBase
{
    public float DrillSpeed { get; set; } = 400f;
    public float EmergeDistance { get; set; } = 50f;

    public Crag()
    {
        BossName = "Crag";
        MaxHealth = 600;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 40 });
    }
}
