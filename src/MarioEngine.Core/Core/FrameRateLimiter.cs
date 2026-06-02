namespace MarioEngine.Core.Core;

/// <summary>Limits frame rate to a target FPS using precise Sleep timing.</summary>
internal sealed class FrameRateLimiter
{
    public int TargetFPS { get; set; } = 60;
    public bool Unlimited { get; set; }

    private long _targetTicks = TimeSpan.TicksPerSecond / 60;

    public void CapFrameRate(float dt)
    {
        if (Unlimited) return;
        _targetTicks = TimeSpan.TicksPerSecond / TargetFPS;
        long elapsedTicks = (long)(dt * TimeSpan.TicksPerSecond);
        long sleepTicks = _targetTicks - elapsedTicks;
        if (sleepTicks > 0)
        {
            int sleepMs = (int)(sleepTicks / TimeSpan.TicksPerMillisecond);
            if (sleepMs > 1) Thread.Sleep(sleepMs - 1);
            while (DateTime.UtcNow.Ticks % _targetTicks != 0) { /* spin-wait */ }
        }
    }
}
