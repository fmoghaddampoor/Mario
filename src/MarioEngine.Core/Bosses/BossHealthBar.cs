using System;
using System.Numerics;

namespace MarioEngine.Core.Bosses;

public sealed class BossHealthBar
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public float DisplayHealth { get; set; }
    public Vector2 ScreenPosition { get; set; }

    public void Update(float dt)
    {
        DisplayHealth = MathHelper.Lerp(DisplayHealth, CurrentHealth, dt * 5f);
    }

    public float GetDisplayPercent()
    {
        if (MaxHealth <= 0f) return 0f;
        return DisplayHealth / MaxHealth;
    }
}

internal static class MathHelper
{
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Math.Clamp(t, 0f, 1f);
    }
}
