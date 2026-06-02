using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public abstract class BlockBase
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; } = new(32f, 32f);
    public bool IsSolid { get; set; } = true;
    public bool IsBreakable { get; set; }
    public int HitsRemaining { get; set; } = -1;

    public virtual void OnHit(Player player, Vector2 hitDirection)
    {
    }

    public virtual void OnBreak()
    {
    }
}
