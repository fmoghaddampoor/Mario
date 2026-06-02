using System;

namespace MarioEngine.Core.PowerUps;

public sealed class StarEffect
{
    public float Timer { get; set; }
    public float RainbowCycleSpeed { get; set; } = 0.1f;

    public void Update(float dt)
    {
        Timer += dt;
    }

    public uint GetCurrentColor()
    {
        float t = Timer * RainbowCycleSpeed;
        t -= (float)Math.Floor(t);

        uint r = t < 0.5f ? (uint)(255f * (1f - t * 2f)) : (uint)(255f * (t * 2f - 1f));
        uint g = (uint)(255f * (float)Math.Sin(t * Math.PI * 2f));
        uint b = (uint)(255f * (float)Math.Cos(t * Math.PI * 2f));

        return 0xFF000000 | (r << 16) | (g << 8) | b;
    }
}
