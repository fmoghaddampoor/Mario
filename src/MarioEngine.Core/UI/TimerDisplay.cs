namespace MarioEngine.Core.UI;

/// <summary>Countdown timer display with color changes when time is low.</summary>
internal sealed class TimerDisplay
{
    /// <summary>Time value to display.</summary>
    public float DisplayTime { get; private set; }

    /// <summary>Updates the displayed time.</summary>
    public void Update(float time)
    {
        DisplayTime = time;
    }
}
