using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public enum HitDirection
{
    Any,
    Top,
    Bottom,
    Left,
    Right
}

public sealed class DirectionalBlock : BlockBase
{
    public HitDirection AllowedDirection { get; set; } = HitDirection.Any;

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        if (AllowedDirection == HitDirection.Any) return;

        HitDirection incomingDirection = GetHitDirection(hitDirection);
        if (incomingDirection != AllowedDirection) return;
    }

    private static HitDirection GetHitDirection(Vector2 dir)
    {
        float ax = Math.Abs(dir.X);
        float ay = Math.Abs(dir.Y);

        if (ay > ax)
            return dir.Y < 0 ? HitDirection.Top : HitDirection.Bottom;

        return dir.X < 0 ? HitDirection.Left : HitDirection.Right;
    }
}
