namespace MarioEngine.Core.Physics.Player;

using System.Numerics;

/// <summary>
/// Controls slope sliding behavior for the player.
/// When on a steep slope, the player slides down automatically.
/// </summary>
internal sealed class SlopeSlide
{
    /// <summary>Gets or sets the downward velocity applied while sliding on a slope.</summary>
    internal float SlideVelocity { get; set; } = 200f;

    /// <summary>Gets or sets a value indicating whether the player is currently sliding on a slope.</summary>
    internal bool IsSliding { get; set; }

    /// <summary>
    /// Starts the slope slide.
    /// </summary>
    internal void StartSlide()
    {
        IsSliding = true;
    }

    /// <summary>
    /// Returns the slide velocity applied each frame while on a slope.
    /// </summary>
    /// <returns>Downward velocity vector.</returns>
    internal Vector2 GetSlideVelocity()
    {
        return new Vector2(0f, SlideVelocity);
    }

    /// <summary>
    /// Stops the slope slide.
    /// </summary>
    internal void StopSlide()
    {
        IsSliding = false;
    }
}
