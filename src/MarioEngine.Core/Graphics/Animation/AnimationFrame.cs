namespace MarioEngine.Core.Graphics.Animation;

/// <summary>
/// A single frame in an animation sequence.
/// Defines which texture region to show and for how long.
/// </summary>
public readonly struct AnimationFrame
{
    /// <summary>Initializes a new instance of the <see cref="AnimationFrame"/> struct.</summary>
    /// <param name="u1">Texture coordinate left (0-1).</param>
    /// <param name="v1">Texture coordinate top (0-1).</param>
    /// <param name="u2">Texture coordinate right (0-1).</param>
    /// <param name="v2">Texture coordinate bottom (0-1).</param>
    /// <param name="duration">Frame duration in seconds.</param>
    public AnimationFrame(float u1, float v1, float u2, float v2, float duration)
    {
        U1 = u1;
        V1 = v1;
        U2 = u2;
        V2 = v2;
        Duration = duration;
    }

    /// <summary>Gets the left UV coordinate.</summary>
    public float U1 { get; }

    /// <summary>Gets the top UV coordinate.</summary>
    public float V1 { get; }

    /// <summary>Gets the right UV coordinate.</summary>
    public float U2 { get; }

    /// <summary>Gets the bottom UV coordinate.</summary>
    public float V2 { get; }

    /// <summary>Gets the frame duration in seconds.</summary>
    public float Duration { get; }
}
