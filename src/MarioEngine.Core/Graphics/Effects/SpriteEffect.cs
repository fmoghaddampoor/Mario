namespace MarioEngine.Core.Graphics.Effects;

/// <summary>
/// Base class for all sprite effects (flash, tint, dissolve, shimmer, pulse).
/// Each effect has a duration, tracks elapsed time, and can be stacked.
/// </summary>
public abstract class SpriteEffect
{
    /// <summary>Gets or sets the effect duration in seconds. 0 = infinite (manual stop).</summary>
    public float Duration { get; set; }

    /// <summary>Gets the elapsed time since the effect started.</summary>
    public float Elapsed { get; private set; }

    /// <summary>Gets a value indicating whether the effect has finished.</summary>
    public bool IsFinished => Duration > 0f && Elapsed >= Duration;

    /// <summary>Gets the normalized progress (0-1) based on elapsed/duration.</summary>
    public float Progress => Duration > 0f ? System.Math.Clamp(Elapsed / Duration, 0f, 1f) : 0f;

    /// <summary>
    /// Updates the effect timer. Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public virtual void Update(float dt)
    {
        Elapsed += dt;
    }

    /// <summary>
    /// Resets the effect to its initial state.
    /// </summary>
    public virtual void Reset()
    {
        Elapsed = 0f;
    }
}
