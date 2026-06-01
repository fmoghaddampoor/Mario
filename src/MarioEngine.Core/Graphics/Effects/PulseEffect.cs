namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// Pulse effect that oscillates sprite opacity. Used for block idle animations
/// or making objects "breath" to attract attention.
/// </summary>
public sealed class PulseEffect : SpriteEffect
{
    /// <summary>Initializes a new instance of the <see cref="PulseEffect"/> class.</summary>
    /// <param name="duration">Effect duration in seconds. 0 = infinite.</param>
    /// <param name="speed">Pulse speed multiplier. Default 1.0.</param>
    public PulseEffect(float duration = 0f, float speed = 1.0f)
    {
        Duration = duration;
        Speed = speed;
    }

    /// <summary>Gets the pulse speed multiplier.</summary>
    public float Speed { get; }
}
