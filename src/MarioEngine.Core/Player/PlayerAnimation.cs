namespace MarioEngine.Core.GamePlayer;

public sealed class PlayerAnimationController
{
    public enum AnimationState
    {
        Idle,
        Walk,
        Run,
        Jump,
        DoubleJump,
        Fall,
        Crouch,
        Slide,
        Skid,
        WallSlide,
        Swim,
        Climb,
        Dead,
        Victory
    }

    public AnimationState CurrentAnimation { get; set; } = AnimationState.Idle;
    public float AnimationSpeed { get; set; } = 1f;

    public void SyncWithState(PlayerStateMachine.PlayerState state)
    {
        CurrentAnimation = state switch
        {
            PlayerStateMachine.PlayerState.Idle => AnimationState.Idle,
            PlayerStateMachine.PlayerState.Walking => AnimationState.Walk,
            PlayerStateMachine.PlayerState.Running => AnimationState.Run,
            PlayerStateMachine.PlayerState.Jumping => AnimationState.Jump,
            PlayerStateMachine.PlayerState.DoubleJumping => AnimationState.DoubleJump,
            PlayerStateMachine.PlayerState.Falling => AnimationState.Fall,
            PlayerStateMachine.PlayerState.Crouching => AnimationState.Crouch,
            PlayerStateMachine.PlayerState.Sliding => AnimationState.Slide,
            PlayerStateMachine.PlayerState.Skidding => AnimationState.Skid,
            PlayerStateMachine.PlayerState.WallSliding => AnimationState.WallSlide,
            PlayerStateMachine.PlayerState.Climbing => AnimationState.Climb,
            PlayerStateMachine.PlayerState.Swimming => AnimationState.Swim,
            PlayerStateMachine.PlayerState.Dead => AnimationState.Dead,
            PlayerStateMachine.PlayerState.Victory => AnimationState.Victory,
            _ => AnimationState.Idle
        };
    }
}
