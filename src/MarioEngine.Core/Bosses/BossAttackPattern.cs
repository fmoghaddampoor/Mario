using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossAttackPattern
{
    public List<BossAttack> Attacks { get; set; } = new();
    public int CurrentAttackIndex { get; set; }

    public BossAttack GetNextAttack()
    {
        if (Attacks.Count == 0) return null!;

        var attack = Attacks[CurrentAttackIndex];
        CurrentAttackIndex = (CurrentAttackIndex + 1) % Attacks.Count;
        return attack;
    }

    public void ResetPattern()
    {
        CurrentAttackIndex = 0;
    }
}
