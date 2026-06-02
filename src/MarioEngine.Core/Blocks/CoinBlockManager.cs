using System;
using System.Collections.Generic;

namespace MarioEngine.Core.Blocks;

public sealed class CoinBlockManager
{
    private readonly List<CoinBlock> _coinBlocks = new();
    public int TotalCoinsCollected { get; private set; }

    public void RegisterCoinBlock(CoinBlock block)
    {
        _coinBlocks.Add(block);
    }

    public void NotifyCoinCollected()
    {
        TotalCoinsCollected++;
    }
}
