using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class EndLevelBlock : BlockBase
{
    public string NextLevel { get; set; } = string.Empty;

    public override void OnHit(Player player, Vector2 hitDirection)
    {
    }
}
