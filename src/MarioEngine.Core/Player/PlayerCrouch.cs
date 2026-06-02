using System.Numerics;

namespace MarioEngine.Core.Player;

internal sealed class PlayerCrouch
{
    public bool IsCrouching { get; private set; }
    public bool IsSliding { get; private set; }
    public Vector2 CrouchSize { get; } = new(32, 32);
    public Vector2 SlideSize { get; } = new(32, 24);

    private Vector2 _slideVelocity;

    public void StartCrouch()
    {
        IsCrouching = true;
    }

    public void EndCrouch()
    {
        IsCrouching = false;
    }

    public void StartSlide(Vector2 velocity)
    {
        IsSliding = true;
        IsCrouching = true;
        _slideVelocity = velocity;
    }

    public Vector2 UpdateSlide(float dt)
    {
        if (!IsSliding) return Vector2.Zero;

        const float friction = 500f;
        float speed = _slideVelocity.Length();
        speed = Math.Max(0, speed - friction * dt);

        if (speed <= 0f)
        {
            IsSliding = false;
            _slideVelocity = Vector2.Zero;
            return Vector2.Zero;
        }

        _slideVelocity = Vector2.Normalize(_slideVelocity) * speed;
        return _slideVelocity;
    }
}
