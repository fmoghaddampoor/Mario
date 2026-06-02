using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Brokk : BossBase
{
    public float ShockwaveRange { get; set; } = 200f;
    public float ShellTossSpeed { get; set; } = 350f;

    public Brokk()
    {
        BossName = "Brokk";
        MaxHealth = 500;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Phase 1", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Phase 2", HealthThreshold = 50 });
    }
}
