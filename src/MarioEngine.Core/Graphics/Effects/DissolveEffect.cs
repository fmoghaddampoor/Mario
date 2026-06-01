namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// Noise-based dissolve effect. Used for death animations or transitions.
/// Fades the sprite from solid to fully transparent over the duration.
/// </summary>
public sealed class DissolveEffect : SpriteEffect
{
    /// <summary>Initializes a new instance of the <see cref="DissolveEffect"/> class.</summary>
    /// <param name="duration">Dissolve duration in seconds. Default 0.5.</param>
    public DissolveEffect(float duration = 0.5f)
    {
        Duration = duration;
    }
}
