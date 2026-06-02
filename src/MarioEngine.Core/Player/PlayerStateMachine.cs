namespace MarioEngine.Core.Player;

internal sealed class PlayerStateMachine
{
    public enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        DoubleJumping,
        Falling,
        Crouching,
        Sliding,
        Skidding,
        WallSliding,
        Climbing,
        Swimming,
        Dead,
        Victory
    }

    public PlayerState CurrentState { get; set; } = PlayerState.Idle;
    public PlayerState PreviousState { get; private set; } = PlayerState.Idle;

    public event Action<PlayerState, PlayerState>? OnStateChanged;

    public void TransitionTo(PlayerState newState)
    {
        if (newState == CurrentState) return;

        PreviousState = CurrentState;
        CurrentState = newState;
        OnStateChanged?.Invoke(PreviousState, CurrentState);
    }
}
