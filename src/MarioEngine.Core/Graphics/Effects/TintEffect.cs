namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// Color overlay effect. Used during power-up transitions or status effects.
/// Applies a tint color with configurable opacity.
/// </summary>
public sealed class TintEffect : SpriteEffect
{
    /// <summary>Initializes a new instance of the <see cref="TintEffect"/> class.</summary>
    /// <param name="color">Tint color as RGBA uint. Alpha channel controls overlay strength.</param>
    /// <param name="duration">Effect duration in seconds. 0 = infinite.</param>
    public TintEffect(uint color, float duration = 0f)
    {
        Color = color;
        Duration = duration;
    }

    /// <summary>Gets the tint color as RGBA uint.</summary>
    public uint Color { get; }
}
