using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class Bowser : BossBase
{
    public float BreathRange { get; set; } = 300f;
    public float AxeSpeed { get; set; } = 500f;

    public Bowser()
    {
        BossName = "Bowser";
        MaxHealth = 2000;
        CurrentHealth = MaxHealth;

        Phases.Add(new BossPhase { Name = "Shell", HealthThreshold = 100 });
        Phases.Add(new BossPhase { Name = "Enrage", HealthThreshold = 50 });
        Phases.Add(new BossPhase { Name = "Final Stand", HealthThreshold = 15 });
    }
}
