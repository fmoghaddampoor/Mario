namespace MarioEngine.Core.Graphics.Debug;

/// <summary>OpenGL timer query for measuring GPU frame time.</summary>
internal sealed class GpuTimerQuery
{
    public float LastFrameTimeMs { get; private set; }
    private long _startTicks;

    public void Begin()
    {
        _startTicks = DateTime.UtcNow.Ticks;
    }

    public void End()
    {
        var elapsed = DateTime.UtcNow.Ticks - _startTicks;
        LastFrameTimeMs = (float)(elapsed / 10000.0);
    }
}
