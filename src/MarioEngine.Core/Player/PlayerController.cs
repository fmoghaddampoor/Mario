using System.Numerics;

namespace MarioEngine.Core.Player;

internal sealed class PlayerController
{
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Vector2 Size { get; set; } = new(32, 48);
    public float Speed { get; set; } = 300f;
    public bool FacingRight { get; set; } = true;

    public PlayerMovement Movement { get; }
    public PlayerJump Jump { get; }
    public PlayerStateMachine StateMachine { get; }
    public PlayerCrouch Crouch { get; }
    public PlayerGroundDetection GroundDetection { get; }
    public PlayerDamage Damage { get; }
    public PlayerAnimationController Animation { get; }
    public PlayerPowerUpManager PowerUp { get; }

    public PlayerController()
    {
        Movement = new PlayerMovement();
        Jump = new PlayerJump();
        StateMachine = new PlayerStateMachine();
        Crouch = new PlayerCrouch();
        GroundDetection = new PlayerGroundDetection();
        Damage = new PlayerDamage();
        Animation = new PlayerAnimationController();
        PowerUp = new PlayerPowerUpManager();
    }

    public void Update(float dt)
    {
        GroundDetection.Update(Position, Size * 0.5f, _ => false);
        Damage.Update(dt);
        Animation.SyncWithState(StateMachine.CurrentState);
    }
}
