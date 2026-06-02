namespace MarioEngine.Core.Core.Input.Debug;

/// <summary>Measures input-to-screen latency.</summary>
internal sealed class InputLatencyMeasurer
{
    private readonly Queue<float> _samples = new();
    public int MaxSamples { get; set; } = 30;

    public void RecordLatency(float ms)
    {
        _samples.Enqueue(ms);
        while (_samples.Count > MaxSamples) _samples.Dequeue();
    }
}
