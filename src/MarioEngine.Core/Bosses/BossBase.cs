using System;
using System.Collections.Generic;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public abstract class BossBase
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public int MaxHealth { get; protected set; }
    public int CurrentHealth { get; protected set; }
    public bool IsAlive { get; protected set; } = true;
    public bool IsInvincible { get; set; }
    public string BossName { get; set; } = string.Empty;
    public List<BossPhase> Phases { get; set; } = new();
    public int CurrentPhaseIndex { get; set; }

    public virtual void Update(float dt)
    {
        Position += Velocity * dt;
    }

    public virtual void TakeDamage(int amount)
    {
        if (IsInvincible || !IsAlive) return;

        CurrentHealth = Math.Max(0, CurrentHealth - amount);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        IsAlive = false;
    }
}
