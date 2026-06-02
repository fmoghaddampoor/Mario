namespace MarioEngine.Core.Physics.Zones;

using System;
using System.Numerics;

/// <summary>
/// Handles pipe-based transport where the player enters a pipe
/// and travels to an exit position over a fixed duration.
/// </summary>
internal sealed class PipeTransport
{
    private Action? _onComplete;
    private float _elapsed;

    /// <summary>
    /// Initializes a new instance of the <see cref="PipeTransport"/> class.
    /// </summary>
    /// <param name="exitPosition">Exit world position.</param>
    /// <param name="travelDuration">Travel duration in seconds.</param>
    internal PipeTransport(Vector2 exitPosition, float travelDuration)
    {
        ExitPosition = exitPosition;
        TravelDuration = travelDuration;
        IsTraveling = false;
        _elapsed = 0f;
    }

    /// <summary>Gets or sets the world position the player exits at.</summary>
    internal Vector2 ExitPosition { get; set; }

    /// <summary>Gets or sets the duration in seconds the travel animation takes.</summary>
    internal float TravelDuration { get; set; }

    /// <summary>Gets or sets a value indicating whether the player is currently travelling through a pipe.</summary>
    internal bool IsTraveling { get; set; }

    /// <summary>
    /// Begins pipe travel. Calls <paramref name="onComplete"/> when travel finishes.
    /// </summary>
    /// <param name="onComplete">Callback invoked after travel duration elapses.</param>
    internal void StartTravel(Action onComplete)
    {
        IsTraveling = true;
        _elapsed = 0f;
        _onComplete = onComplete;
    }

    /// <summary>
    /// Advances the travel timer. Returns the exit position when travel completes.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    /// <returns>The current interpolated position, or exit position when done.</returns>
    internal Vector2 UpdateTravel(float dt)
    {
        if (!IsTraveling)
        {
            return ExitPosition;
        }

        _elapsed += dt;

        if (_elapsed >= TravelDuration)
        {
            IsTraveling = false;
            _onComplete?.Invoke();
        }

        return ExitPosition;
    }
}
