using System.Numerics;

namespace MarioEngine.Core.Player;

internal sealed class PlayerMovement
{
    public float WalkSpeed { get; set; } = 200f;
    public float RunSpeed { get; set; } = 350f;
    public bool Running { get; set; }
    public bool FacingRight { get; set; } = true;

    public Vector2 GetMovementVelocity(float inputX, bool runHeld)
    {
        float speed = runHeld ? RunSpeed : WalkSpeed;
        Running = runHeld && Math.Abs(inputX) > 0.1f;
        return new Vector2(inputX * speed, 0);
    }

    public bool IsSkidding { get; private set; }

    public void UpdateSkid(float inputX, float velocityX, float dt)
    {
        if (Math.Abs(inputX) > 0.1f && velocityX != 0)
        {
            bool wasRight = velocityX > 0;
            bool wantsLeft = inputX < 0;
            bool wantsRight = inputX > 0;
            IsSkidding = wasRight && wantsLeft || !wasRight && wantsRight;
        }
        else
        {
            IsSkidding = false;
        }
    }
}
