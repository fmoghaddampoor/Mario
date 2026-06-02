using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class SpringBlock : BlockBase
{
    public float LaunchForce { get; set; } = -600f;

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        player.Velocity = new Vector2(player.Velocity.X, LaunchForce);
    }
}
