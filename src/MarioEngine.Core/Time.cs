namespace MarioEngine.Core;

/// <summary>
/// Centralized time management for the game loop.
/// Provides delta time, total elapsed time, frame count, and FPS tracking.
/// </summary>
public static class Time
{
    private static float _deltaTime;
    private static float _totalTime;
    private static float _timeScale = 1f;
    private static int _frameCount;

    /// <summary>Gets the delta time in seconds affected by <see cref="TimeScale"/>.</summary>
    public static float DeltaTime => _deltaTime * _timeScale;

    /// <summary>Gets the unscaled delta time in seconds.</summary>
    public static float DeltaTimeUnscaled => _deltaTime;

    /// <summary>Gets the total scaled elapsed time in seconds.</summary>
    public static float TotalTime => _totalTime;

    /// <summary>Gets the total unscaled elapsed time in seconds.</summary>
    public static float TotalTimeUnscaled { get; private set; }

    /// <summary>Gets the total number of frames rendered.</summary>
    public static int FrameCount => _frameCount;

    /// <summary>
    /// Gets or sets the time scale multiplier.
    /// 1.0 = normal speed, 0.5 = half speed (slow-mo), 0.0 = paused.
    /// </summary>
    public static float TimeScale
    {
        get => _timeScale;
        set => _timeScale = Math.Max(0f, value);
    }

    /// <summary>Gets the instantaneous frames-per-second.</summary>
    public static float Fps { get; private set; }

    /// <summary>Gets the smoothed FPS (exponential moving average).</summary>
    public static float SmoothFps { get; private set; }

    /// <summary>
    /// Updates time values for the current frame. Called once per frame by the application window.
    /// </summary>
    /// <param name="rawDeltaTime">Unscaled delta time from the previous frame in seconds.</param>
    public static void Update(float rawDeltaTime)
    {
        _deltaTime = Math.Clamp(rawDeltaTime, 0f, 0.1f);
        TotalTimeUnscaled += _deltaTime;
        _totalTime += _deltaTime * _timeScale;
        _frameCount++;

        Fps = 1f / Math.Max(_deltaTime, 0.0001f);
        SmoothFps = (SmoothFps * 0.9f) + (Fps * 0.1f);
    }

    /// <summary>Resets all time values to their default state.</summary>
    internal static void Reset()
    {
        _deltaTime = 0f;
        _totalTime = 0f;
        TotalTimeUnscaled = 0f;
        _timeScale = 1f;
        _frameCount = 0;
        Fps = 0f;
        SmoothFps = 0f;
    }
}
