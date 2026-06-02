using System;
using System.Numerics;

namespace MarioEngine.Core.Blocks;

public sealed class VineBlock : BlockBase
{
    public VineBlock()
    {
        IsSolid = true;
        IsBreakable = false;
    }

    public void OnPlayerContact(Player player)
    {
        ArgumentNullException.ThrowIfNull(player);
        player.CanClimb = true;
    }
}
