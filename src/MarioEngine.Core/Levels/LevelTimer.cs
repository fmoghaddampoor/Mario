namespace MarioEngine.Core.Levels;

/// <summary>Countdown timer for levels.</summary>
internal sealed class LevelTimer
{
    /// <summary>Remaining time in seconds.</summary>
    public float TimeRemaining { get; private set; }

    /// <summary>Total starting time in seconds.</summary>
    public float TotalTime { get; set; }

    /// <summary>Whether the timer is running.</summary>
    public bool IsRunning { get; private set; }

    /// <summary>Whether the timer has expired.</summary>
    public bool IsExpired => TimeRemaining <= 0f;

    /// <summary>Starts the timer.</summary>
    public void Start()
    {
        IsRunning = true;
    }

    /// <summary>Stops the timer.</summary>
    public void Stop()
    {
        IsRunning = false;
    }

    /// <summary>Updates the timer each frame.</summary>
    public void Update(float dt)
    {
        if (!IsRunning || IsExpired)
            return;

        TimeRemaining -= dt;
        if (TimeRemaining < 0f)
            TimeRemaining = 0f;
    }

    /// <summary>Adds extra time in seconds.</summary>
    public void AddTime(float seconds)
    {
        TimeRemaining += seconds;
    }
}
