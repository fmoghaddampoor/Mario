using System;
using System.Numerics;

namespace MarioEngine.Core.Enemies;

/// <summary>Burrowing mole that pops up and throws dirt at the player.</summary>
internal sealed class MontyMole : EnemyBase
{
    /// <summary>Whether the mole is currently underground.</summary>
    public bool IsBurrowed { get; private set; } = true;

    /// <summary>Duration the mole stays above ground before burrowing again.</summary>
    public float PopUpDuration { get; set; } = 1.5f;

    private float _popUpTimer;

    public MontyMole()
    {
        Health = 2;
        Speed = 0;
        StateMachine.TransitionTo(EnemyState.Idle);
    }

    /// <summary>Burrows underground, becoming hidden and invulnerable.</summary>
    public void Burrow()
    {
        IsBurrowed = true;
        _popUpTimer = 0;
        Velocity = Vector2.Zero;
        StateMachine.TransitionTo(EnemyState.Idle);
    }

    /// <summary>Emerges from underground to attack.</summary>
    public void PopUp()
    {
        IsBurrowed = false;
        _popUpTimer = 0;
        StateMachine.TransitionTo(EnemyState.Attack);
    }

    /// <summary>Throws a dirt projectile toward the player.</summary>
    public void ThrowDirt()
    {
        if (IsBurrowed) return;
        // Dirt projectile creation handled externally.
    }

    /// <summary>Handles pop-up timer and auto-burrow.</summary>
    /// <param name="dt">Delta time.</param>
    public override void Update(float dt)
    {
        if (!IsAlive) return;
        if (IsBurrowed) return;
        _popUpTimer += dt;
        if (_popUpTimer >= PopUpDuration) Burrow();
    }
}
