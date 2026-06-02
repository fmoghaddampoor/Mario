using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public enum AttackState
{
    WindUp,
    Active,
    Recovery,
    Cooldown
}

public sealed class BossAttack
{
    public string Name { get; set; } = string.Empty;
    public float Damage { get; set; }
    public float WindUpTime { get; set; }
    public float ActiveTime { get; set; }
    public float RecoveryTime { get; set; }
    public float Timer { get; set; }
    public AttackState State { get; set; } = AttackState.Cooldown;
}
