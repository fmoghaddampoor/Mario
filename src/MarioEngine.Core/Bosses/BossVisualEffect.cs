using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public enum EffectType
{
    Flash,
    Scale,
    ColorShift,
    ParticleBurst
}

public sealed class BossVisualEffect
{
    public EffectType Type { get; set; }
    public float Duration { get; set; }
    public float Intensity { get; set; }

    public void Apply()
    {
    }

    public void Update(float dt)
    {
    }

    public void Remove()
    {
    }
}
