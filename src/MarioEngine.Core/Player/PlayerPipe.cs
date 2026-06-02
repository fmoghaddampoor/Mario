namespace MarioEngine.Core.Player;

using System;
using System.Numerics;

/// <summary>Handles pipe travel transitions including positioning and timing.</summary>
internal sealed class PlayerPipeTravel
{
    /// <summary>Gets whether the player is currently traveling through a pipe.</summary>
    public bool IsTraveling { get; private set; }

    /// <summary>Gets the target exit position of the pipe.</summary>
    public Vector2 ExitPosition { get; private set; }

    /// <summary>Gets or sets the total travel duration in seconds.</summary>
    public float TravelDuration { get; set; }

    /// <summary>Gets the elapsed travel time.</summary>
    public float TravelTimer { get; private set; }

    /// <summary>Raised when pipe travel completes.</summary>
    public event Action? OnTravelComplete;

    private Vector2 _entryPosition;

    /// <summary>Begins pipe travel toward the given exit position over the specified duration.</summary>
    /// <param name="exitPos">The world position to exit at.</param>
    /// <param name="duration">Travel duration in seconds.</param>
    public void StartTravel(Vector2 exitPos, float duration)
    {
        IsTraveling = true;
        ExitPosition = exitPos;
        TravelDuration = duration;
        TravelTimer = 0f;
    }

    /// <summary>Advances the travel timer and fires completion when finished.</summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!IsTraveling)
            return;

        TravelTimer += dt;

        if (TravelTimer >= TravelDuration)
        {
            IsTraveling = false;
            OnTravelComplete?.Invoke();
        }
    }

    /// <summary>Gets the interpolated position along the pipe path.</summary>
    /// <param name="startPos">The entry world position.</param>
    /// <returns>A linearly interpolated position between entry and exit.</returns>
    public Vector2 GetTravelPosition(Vector2 startPos)
    {
        _entryPosition = startPos;
        float t = Math.Clamp(TravelTimer / TravelDuration, 0f, 1f);
        return Vector2.Lerp(_entryPosition, ExitPosition, t);
    }
}
