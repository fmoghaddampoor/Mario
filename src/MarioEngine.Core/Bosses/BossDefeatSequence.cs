using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossDefeatSequence
{
    public float ExplosionDuration { get; set; } = 3f;
    public float VictoryFanfareDelay { get; set; } = 1f;
    public bool IsPlaying { get; set; }

    public void StartSequence()
    {
        IsPlaying = true;
    }

    public void Update(float dt)
    {
        if (!IsPlaying) return;
    }
}
