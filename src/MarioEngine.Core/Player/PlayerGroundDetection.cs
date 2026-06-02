using System.Numerics;
using MarioEngine.Core.Physics.Collision;

namespace MarioEngine.Core.Player;

internal sealed class PlayerGroundDetection
{
    public enum SurfaceType
    {
        Normal,
        Ice,
        Sand,
        Lava,
        Water
    }

    public bool IsGrounded { get; private set; }
    public bool WasGrounded { get; private set; }
    public float GroundY { get; private set; }
    public SurfaceType CurrentSurface { get; set; } = SurfaceType.Normal;

    public void Update(Vector2 position, Vector2 halfSize, Func<Vector2, bool> isSolid)
    {
        WasGrounded = IsGrounded;
        IsGrounded = false;

        if (isSolid == null) return;

        float footY = position.Y + halfSize.Y;
        float leftX = position.X - halfSize.X + 2;
        float rightX = position.X + halfSize.X - 2;
        float centerX = position.X;

        if (isSolid(new Vector2(centerX, footY + 1)) ||
            isSolid(new Vector2(leftX, footY + 1)) ||
            isSolid(new Vector2(rightX, footY + 1)))
        {
            IsGrounded = true;
            GroundY = footY;
        }
    }
}
