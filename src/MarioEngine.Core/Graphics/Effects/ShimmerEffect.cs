namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// Rainbow shimmer effect. Used for Starman invincibility.
/// Cycles through rainbow colors over the sprite.
/// </summary>
public sealed class ShimmerEffect : SpriteEffect
{
    /// <summary>Initializes a new instance of the <see cref="ShimmerEffect"/> class.</summary>
    /// <param name="duration">Effect duration in seconds. 0 = infinite.</param>
    /// <param name="speed">Rainbow cycling speed. Default 2.0.</param>
    public ShimmerEffect(float duration = 0f, float speed = 2.0f)
    {
        Duration = duration;
        Speed = speed;
    }

    /// <summary>Gets the rainbow cycling speed.</summary>
    public float Speed { get; }
}
