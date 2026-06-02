using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class NoteBlock : BlockBase
{
    public float BounceForce { get; set; } = -400f;

    public override void OnHit(Player player, Vector2 hitDirection)
    {
        player.Velocity = new Vector2(player.Velocity.X, BounceForce);
    }
}
