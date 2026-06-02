namespace MarioEngine.Core.Player;

internal sealed class PlayerJump
{
    public float JumpVelocity { get; set; } = -450f;
    public float DoubleJumpVelocity { get; set; } = -400f;
    public bool CanDoubleJump { get; set; }
    public int JumpCount { get; set; }
    public bool IsJumping { get; set; }
    public bool JumpHeld { get; set; }

    public CoyoteTimer Coyote { get; } = new();
    public JumpBuffer Buffer { get; } = new();

    public void PressJump()
    {
        JumpHeld = true;
        Buffer.Start();
    }

    public void ReleaseJump()
    {
        JumpHeld = false;
    }

    public float GetEffectiveJumpVelocity(bool coyoteActive, bool jumpBuffered)
    {
        if (JumpCount == 0 || coyoteActive && jumpBuffered)
        {
            JumpCount++;
            return JumpVelocity;
        }

        if (JumpCount == 1 && CanDoubleJump)
        {
            JumpCount++;
            return DoubleJumpVelocity;
        }

        return 0;
    }

    internal sealed class CoyoteTimer
    {
        public float Time { get; set; }
        public float Duration { get; set; } = 0.1f;
        public bool IsActive => Time > 0;

        public void Start() => Time = Duration;

        public void Update(float dt)
        {
            if (Time > 0) Time -= dt;
        }
    }

    internal sealed class JumpBuffer
    {
        public float Time { get; set; }
        public float Duration { get; set; } = 0.05f;
        public bool IsActive => Time > 0;

        public void Start() => Time = Duration;

        public void Update(float dt)
        {
            if (Time > 0) Time -= dt;
        }
    }
}
