using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Skeletal Koopa that crumbles into bones when stomped and revives after a delay.</summary>
internal sealed class DryBones : EnemyBase
{
    /// <summary>Whether the Dry Bones is currently a pile of bones.</summary>
    public bool IsCrumpled { get; private set; }

    /// <summary>Time in seconds before the Dry Bones reassembles.</summary>
    public float ReviveTime { get; set; } = 3f;

    private float _reviveTimer;

    public DryBones()
    {
        Health = 1;
        Speed = 50f;
        StateMachine.TransitionTo(EnemyState.Patrol);
    }

    /// <summary>Crumbles into bones. Revives after ReviveTime seconds.</summary>
    /// <param name="amount">Damage amount.</param>
    public override void TakeDamage(int amount)
    {
        if (IsCrumpled) return;
        IsCrumpled = true;
        IsAlive = false;
        Velocity = Vector2.Zero;
        StateMachine.TransitionTo(EnemyState.Dying);
    }

    /// <summary>Handles revive timer when crumpled.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsCrumpled) { base.Update(dt); return; }
        _reviveTimer += dt;
        if (_reviveTimer >= ReviveTime)
        {
            IsCrumpled = false;
            IsAlive = true;
            Health = 1;
            _reviveTimer = 0;
            StateMachine.TransitionTo(EnemyState.Patrol);
        }
    }
}
