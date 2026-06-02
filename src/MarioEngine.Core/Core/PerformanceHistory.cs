namespace MarioEngine.Core.Core;

/// <summary>Tracks frame time history for performance metrics.</summary>
internal sealed class PerformanceHistory
{
    private const int MaxFrames = 60;
    private readonly Queue<float> _frameTimes = new(MaxFrames);

    public float AverageFPS { get; private set; }
    public float MinFPS { get; private set; } = float.MaxValue;
    public float MaxFPS { get; private set; }

    public void RecordFrame(float dt)
    {
        _frameTimes.Enqueue(dt);
        if (_frameTimes.Count > MaxFrames) _frameTimes.Dequeue();

        float sum = 0f, min = float.MaxValue, max = 0f;
        foreach (var t in _frameTimes)
        {
            sum += t;
            if (t < min) min = t;
            if (t > max) max = t;
        }
        float avg = sum / _frameTimes.Count;
        AverageFPS = avg > 0f ? 1f / avg : 0f;
        MinFPS = min > 0f ? 1f / min : 0f;
        MaxFPS = max > 0f ? 1f / max : 0f;
    }
}
