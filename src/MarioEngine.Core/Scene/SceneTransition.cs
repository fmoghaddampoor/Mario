namespace MarioEngine.Core.Scene;

/// <summary>
/// Describes a visual transition between scenes.
/// </summary>
internal sealed class SceneTransition
{
    /// <summary>Available transition styles.</summary>
    public enum TransitionType
    {
        FadeIn,
        FadeOut,
        Wipe
    }

    /// <summary>The style of the transition.</summary>
    public TransitionType Type { get; }

    /// <summary>Total duration of the transition in seconds.</summary>
    public float Duration { get; }

    /// <summary>Time elapsed since the transition started.</summary>
    public float Elapsed { get; private set; }

    /// <summary>True when <see cref="Elapsed"/> reaches or exceeds <see cref="Duration"/>.</summary>
    public bool IsComplete => Elapsed >= Duration;

    /// <summary>Normalised progress from 0 to 1.</summary>
    public float Progress => Math.Clamp(Elapsed / Duration, 0f, 1f);

    /// <summary>Raised once when the transition finishes.</summary>
    public event Action? OnComplete;

    private bool _completed;

    /// <summary>
    /// Creates a new transition of the given <paramref name="type"/> lasting <paramref name="duration"/> seconds.
    /// </summary>
    public SceneTransition(TransitionType type, float duration)
    {
        Type = type;
        Duration = duration;
    }

    /// <summary>
    /// Advances the transition by <paramref name="dt"/> seconds.
    /// Triggers <see cref="OnComplete"/> when the duration is reached.
    /// </summary>
    public void Update(float dt)
    {
        if (_completed) return;

        Elapsed += dt;

        if (IsComplete && !_completed)
        {
            _completed = true;
            OnComplete?.Invoke();
        }
    }
}
