using System.Numerics;

namespace MarioEngine.Core.GamePlayer;

/// <summary>
/// Main player controller composing all player subsystems.
/// Exposes power-up state properties that other systems read/write.
/// </summary>
public sealed class PlayerController
{
    /// <summary>Gets or sets the player world position.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Gets or sets the player velocity.</summary>
    public Vector2 Velocity { get; set; }

    /// <summary>Gets or sets the player collision size.</summary>
    public Vector2 Size { get; set; } = new(32, 48);

    /// <summary>Gets or sets the base movement speed.</summary>
    public float Speed { get; set; } = 300f;

    /// <summary>Gets or sets whether the player faces right.</summary>
    public bool FacingRight { get; set; } = true;

    /// <summary>Movement subsystem.</summary>
    public PlayerMovement Movement { get; }

    /// <summary>Jump subsystem.</summary>
    public PlayerJump Jump { get; }

    /// <summary>State machine.</summary>
    public PlayerStateMachine StateMachine { get; }

    /// <summary>Crouch subsystem.</summary>
    public PlayerCrouch Crouch { get; }

    /// <summary>Ground detection subsystem.</summary>
    public PlayerGroundDetection GroundDetection { get; }

    /// <summary>Damage subsystem.</summary>
    public PlayerDamage Damage { get; }

    /// <summary>Animation controller.</summary>
    public PlayerAnimationController Animation { get; }

    /// <summary>Power-up manager.</summary>
    public PlayerPowerUpManager PowerUp { get; }

    // Power-up feature flags - set by power-up scripts

    /// <summary>Gets or sets the visual scale multiplier.</summary>
    public float Scale { get; set; } = 1f;

    /// <summary>Gets or sets extra hit points from power-ups.</summary>
    public int ExtraHitPoints { get; set; }

    /// <summary>Gets or sets whether player is big (mushroom).</summary>
    public bool IsBig { get; set; }

    /// <summary>Gets or sets whether player can throw fireballs.</summary>
    public bool CanThrowFireballs { get; set; }

    /// <summary>Gets or sets fireball speed.</summary>
    public float FireballSpeed { get; set; } = 400f;

    /// <summary>Gets or sets max simultaneous fireballs.</summary>
    public int MaxFireballs { get; set; } = 2;

    /// <summary>Gets or sets whether player is invincible (star).</summary>
    public bool IsInvincible { get; set; }

    /// <summary>Gets or sets the movement speed multiplier.</summary>
    public float MoveSpeed { get; set; } = 1f;

    /// <summary>Gets or sets the weight multiplier (metal).</summary>
    public float WeightMultiplier { get; set; } = 1f;

    /// <summary>Gets or sets whether player can crush blocks (metal).</summary>
    public bool CanCrushBlocks { get; set; }

    /// <summary>Gets or sets whether player sinks in water (metal).</summary>
    public bool SinksInWater { get; set; }

    /// <summary>Gets or sets whether player can fly (bee).</summary>
    public bool CanFly { get; set; }

    /// <summary>Gets or sets the fly speed (bee).</summary>
    public float FlySpeed { get; set; } = 200f;

    /// <summary>Gets or sets the hover duration (bee).</summary>
    public float HoverDuration { get; set; } = 5f;

    /// <summary>Gets or sets whether player is in ghost form (boo).</summary>
    public bool IsGhostForm { get; set; }

    /// <summary>Gets or sets whether player can phase through enemies (boo).</summary>
    public bool CanPhaseThroughEnemies { get; set; }

    /// <summary>Gets or sets whether player can place blocks (builder).</summary>
    public bool CanPlaceBlocks { get; set; }

    /// <summary>Gets or sets whether player can break blocks (builder).</summary>
    public bool CanBreakBlocks { get; set; }

    /// <summary>Gets or sets the block placement range (builder).</summary>
    public float PlaceRange { get; set; } = 100f;

    /// <summary>Gets or sets max placeable blocks (builder).</summary>
    public int MaxPlaceableBlocks { get; set; } = 3;

    /// <summary>Gets or sets whether player can climb walls (cat).</summary>
    public bool CanClimbWalls { get; set; }

    /// <summary>Gets or sets the wall climb speed (cat).</summary>
    public float ClimbSpeed { get; set; } = 200f;

    /// <summary>Gets or sets the scratch range (cat).</summary>
    public float ScratchRange { get; set; } = 40f;

    /// <summary>Gets or sets the current power-up type.</summary>
    public PowerUps.PowerUpType CurrentPowerUpType { get; set; }

    /// <summary>Gets or sets whether player can climb vines.</summary>
    public bool CanClimb { get; set; }

    /// <summary>Gets or sets whether player can ice slide (penguin).</summary>
    public bool CanIceSlide { get; set; }

    /// <summary>Gets or sets whether player can swim fast (penguin).</summary>
    public bool CanSwim { get; set; }

    /// <summary>Gets or sets the slide speed multiplier (penguin).</summary>
    public float SlideSpeedMultiplier { get; set; } = 2f;

    /// <summary>Gets or sets whether player can propeller spin (propeller).</summary>
    public bool CanPropellerSpin { get; set; }

    /// <summary>Gets or sets the propeller upward force (propeller).</summary>
    public float PropellerForce { get; set; } = -400f;

    /// <summary>Initializes a new instance of the <see cref="PlayerController"/> class.</summary>
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

    /// <summary>
    /// Called every frame. Updates all subsystems.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        GroundDetection.Update(Position, Size * 0.5f, _ => false);
        Damage.Update(dt);
        Animation.SyncWithState(StateMachine.CurrentState);
    }

    /// <summary>
    /// Checks whether the player has a specific power-up type.
    /// </summary>
    /// <param name="type">The power-up type to check.</param>
    /// <returns>True if the player currently has this power-up.</returns>
    public bool HasPowerUp(PowerUps.PowerUpType type)
    {
        return PowerUp?.CurrentPowerUp != null;
    }
}
