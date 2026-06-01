namespace MarioEngine.Core.Graphics.Lighting;

using System;
using System.Numerics;

/// <summary>
/// A single dynamic point light with position, radius, color, intensity, and optional flicker.
/// Managed by <see cref="LightManager"/> with a limit of 16 active lights.
/// </summary>
public sealed class Light2D
{
    /// <summary>Elapsed time for flicker animation in seconds.</summary>
    private float _flickerElapsed;

    /// <summary>Initializes a new instance of the <see cref="Light2D"/> class.</summary>
    public Light2D()
    {
        Seed = Guid.NewGuid().GetHashCode() % 1000;
        if (Seed < 0)
        {
            Seed = -Seed;
        }
    }

    /// <summary>Gets or sets the light position in world pixels.</summary>
    public Vector2 Position { get; set; }

    /// <summary>Gets or sets the light radius in pixels.</summary>
    public float Radius { get; set; } = 100f;

    /// <summary>Gets or sets the light color as RGBA uint (0xRRGGBBAA).</summary>
    public uint Color { get; set; } = 0xFFFFFF80;

    /// <summary>Gets or sets the base intensity multiplier (0-1).</summary>
    public float Intensity { get; set; } = 1f;

    /// <summary>Gets or sets the flicker speed in Hz. 0 = no flicker.</summary>
    public float FlickerSpeed { get; set; } = 0f;

    /// <summary>Gets or sets the flicker amplitude (0-1).</summary>
    public float FlickerAmplitude { get; set; } = 0.1f;

    /// <summary>Gets a value indicating whether this light has flicker animation.</summary>
    public bool HasFlicker => FlickerSpeed > 0f;

    /// <summary>Gets or sets the shadow sprite size (0 = no shadow).</summary>
    public float ShadowSize { get; set; } = 0f;

    /// <summary>Gets or sets the shadow sprite offset.</summary>
    public Vector2 ShadowOffset { get; set; }

    /// <summary>Gets or sets the shadow opacity (0-1).</summary>
    public float ShadowOpacity { get; set; } = 0.3f;

    /// <summary>Gets the flicker animation seed for this light (0-999).</summary>
    internal int Seed { get; }

    /// <summary>
    /// Updates the flicker animation. Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!HasFlicker)
        {
            return;
        }

        _flickerElapsed += dt;
    }

    /// <summary>
    /// Gets the current effective intensity after flicker modulation.
    /// </summary>
    /// <returns>Intensity value (0-1) with flicker applied.</returns>
    public float GetEffectiveIntensity()
    {
        if (!HasFlicker)
        {
            return Intensity;
        }

        var phase = (_flickerElapsed * FlickerSpeed * MathF.PI * 2f) + (Seed * 0.001f);
        var flicker = MathF.Sin(phase) * FlickerAmplitude;
        return Math.Clamp(Intensity + flicker, 0f, 1f);
    }
}
