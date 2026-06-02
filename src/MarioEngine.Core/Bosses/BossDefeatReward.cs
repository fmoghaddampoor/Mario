using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossDefeatReward
{
    public int CoinReward { get; set; }
    public string? PowerUpReward { get; set; }
    public string? KeyItemReward { get; set; }

    public void GrantReward()
    {
    }
}
