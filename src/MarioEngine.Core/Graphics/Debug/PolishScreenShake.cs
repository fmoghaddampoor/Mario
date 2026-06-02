namespace MarioEngine.Core.Graphics.Debug;

/// <summary>Screen shake effect for polish feedback.</summary>
internal sealed class PolishScreenShake
{
    public float Intensity { get; private set; }
    public float Duration { get; private set; }

    private float _elapsed;
    private Random _random = new();

    public void Trigger(float intensity, float duration)
    {
        Intensity = intensity;
        Duration = duration;
        _elapsed = 0f;
    }

    public void Update(float dt)
    {
        if (_elapsed >= Duration) { Intensity = 0f; return; }
        _elapsed += dt;
        float progress = _elapsed / Duration;
        float decay = 1f - progress;
        float offsetX = ((float)_random.NextDouble() * 2f - 1f) * Intensity * decay;
        float offsetY = ((float)_random.NextDouble() * 2f - 1f) * Intensity * decay;
        // Apply camera offset via external callback or transform
    }
}
