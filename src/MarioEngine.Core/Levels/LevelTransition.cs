namespace MarioEngine.Core.Levels;

using System;

/// <summary>Handles fade transitions between levels.</summary>
internal sealed class LevelTransition
{
    /// <summary>Duration of the fade in seconds.</summary>
    public float FadeDuration { get; set; } = 1f;

    /// <summary>Whether a transition is in progress.</summary>
    public bool IsTransitioning { get; private set; }

    /// <summary>Raised when the transition completes.</summary>
    public event Action? OnTransitionComplete;

    /// <summary>Begins a transition to the specified level.</summary>
    public void StartTransitionTo(string levelName)
    {
        IsTransitioning = true;
    }

    /// <summary>Updates the transition each frame.</summary>
    public void Update(float dt)
    {
    }
}
