using System.Numerics;
using MarioEngine.Core.Physics.Collision;

namespace MarioEngine.Core.GamePlayer;

public sealed class PlayerHitbox
{
    public Aabb GetHitbox(Vector2 position, Vector2 size)
    {
        Vector2 half = size * 0.5f;
        return new Aabb(position - half, position + half);
    }

    public Aabb GetHurtbox(Vector2 position, Vector2 size)
    {
        Vector2 half = size * 0.5f;
        float shrink = 2f;
        return new Aabb(
            new Vector2(position.X - half.X + shrink, position.Y - half.Y + shrink),
            new Vector2(position.X + half.X - shrink, position.Y + half.Y - shrink)
        );
    }

    public Aabb GetHeadbox(Vector2 position, Vector2 size)
    {
        Vector2 half = size * 0.5f;
        float headHeight = 12f;
        return new Aabb(
            new Vector2(position.X - half.X + 4, position.Y - half.Y),
            new Vector2(position.X + half.X - 4, position.Y - half.Y + headHeight)
        );
    }
}
