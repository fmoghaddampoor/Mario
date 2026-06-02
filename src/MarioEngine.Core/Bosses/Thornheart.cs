using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Thornheart : BossBase
{
    public float VineReach { get; set; } = 300f;
    public float SeedSpeed { get; set; } = 200f;

    public Thornheart()
    {
        BossName = "Thornheart";
        MaxHealth = 800;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 40 });
    }
}
