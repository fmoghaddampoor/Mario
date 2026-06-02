using System;

namespace MarioEngine.Core.Enemies;

/// <summary>All possible enemy behaviour states.</summary>
internal enum EnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Hit,
    Dying,
    Dead
}

/// <summary>Finite state machine for enemy AI states. Handles transitions and state-change events.</summary>
internal class EnemyStateMachine
{
    /// <summary>Gets the current state of the machine.</summary>
    public EnemyState CurrentState { get; private set; } = EnemyState.Idle;

    /// <summary>Gets the previous state before the last transition.</summary>
    public EnemyState PreviousState { get; private set; } = EnemyState.Idle;

    /// <summary>Fired when a state transition occurs. Parameters: oldState, newState.</summary>
    public event Action<EnemyState, EnemyState>? OnStateChanged;

    /// <summary>Transitions to the specified new state. Does nothing if already in that state.</summary>
    /// <param name="newState">The target state to transition to.</param>
    public void TransitionTo(EnemyState newState)
    {
        if (newState == CurrentState) return;
        PreviousState = CurrentState;
        CurrentState = newState;
        OnStateChanged?.Invoke(PreviousState, CurrentState);
    }

    /// <summary>Per-frame state logic hook. Override in derived types for per-state updates.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt) { }
}
