using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Glacia : BossBase
{
    public float BeamWidth { get; set; } = 40f;
    public float PillarHeight { get; set; } = 150f;

    public Glacia()
    {
        BossName = "Glacia";
        MaxHealth = 900;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 35 });
    }
}
