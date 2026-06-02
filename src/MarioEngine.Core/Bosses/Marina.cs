using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Marina : BossBase
{
    public float TentacleRange { get; set; } = 250f;
    public float InkDuration { get; set; } = 3f;

    public Marina()
    {
        BossName = "Marina";
        MaxHealth = 700;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 50 });
    }
}
