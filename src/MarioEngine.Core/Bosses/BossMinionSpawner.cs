using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossMinionSpawner
{
    public string MinionType { get; set; } = string.Empty;
    public float SpawnInterval { get; set; }
    public int MaxMinions { get; set; }
    public int ActiveMinions { get; set; }

    public void SpawnMinion(Vector2 position)
    {
        if (ActiveMinions >= MaxMinions) return;
        ActiveMinions++;
    }

    public void Update(float dt)
    {
    }
}
