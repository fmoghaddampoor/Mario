namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// White flash overlay effect. Used briefly when the player takes damage.
/// Lasts 0.1 seconds by default.
/// </summary>
public sealed class FlashEffect : SpriteEffect
{
    /// <summary>Initializes a new instance of the <see cref="FlashEffect"/> class.</summary>
    /// <param name="duration">Flash duration in seconds. Default 0.1.</param>
    public FlashEffect(float duration = 0.1f)
    {
        Duration = duration;
    }
}
