namespace MarioEngine.Core.Graphics.Particles;

using System;

/// <summary>
/// Contains static utility methods for <see cref="ParticleSystem"/>.
/// </summary>
public sealed partial class ParticleSystem
{
    /// <summary>Linearly interpolates between two float values.</summary>
    /// <param name="a">Start value.</param>
    /// <param name="b">End value.</param>
    /// <param name="t">Interpolation factor (0-1).</param>
    /// <returns>Interpolated value clamped between a and b.</returns>
    private static float Lerp(float a, float b, float t)
    {
        return a + ((b - a) * Math.Clamp(t, 0f, 1f));
    }

    /// <summary>Linearly interpolates between two RGBA uint colors.</summary>
    /// <param name="from">Start color as 0xAARRGGBB.</param>
    /// <param name="to">End color as 0xAARRGGBB.</param>
    /// <param name="t">Interpolation factor (0-1).</param>
    /// <returns>Interpolated color as 0xAARRGGBB.</returns>
    private static uint ColorLerp(uint from, uint to, float t)
    {
        var fr = (byte)(from >> 16);
        var fg = (byte)(from >> 8);
        var fb = (byte)from;
        var fa = (byte)(from >> 24);
        var tr = (byte)(to >> 16);
        var tg = (byte)(to >> 8);
        var tb = (byte)to;
        var ta = (byte)(to >> 24);
        var r = (byte)(fr + ((tr - fr) * t));
        var g = (byte)(fg + ((tg - fg) * t));
        var b = (byte)(fb + ((tb - fb) * t));
        var a = (byte)(fa + ((ta - fa) * t));
        return (uint)((a << 24) | (r << 16) | (g << 8) | b);
    }
}
