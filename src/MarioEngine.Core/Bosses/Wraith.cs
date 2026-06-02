using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Wraith : BossBase
{
    public float TeleportCooldown { get; set; } = 2f;
    public int MaxMinions { get; set; } = 4;

    public Wraith()
    {
        BossName = "Wraith";
        MaxHealth = 750;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 40 });
    }
}
