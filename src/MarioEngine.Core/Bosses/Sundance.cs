using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Sundance : BossBase
{
    public float FloatSpeed { get; set; } = 100f;

    public Sundance()
    {
        BossName = "Sundance";
        MaxHealth = 400;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 30 });
    }
}
