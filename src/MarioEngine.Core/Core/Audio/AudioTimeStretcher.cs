namespace MarioEngine.Core.Core.Audio;

/// <summary>Changes pitch without affecting speed (placeholder).</summary>
internal sealed class AudioTimeStretcher
{
    public float Pitch { get; set; } = 1.0f;
    public float Speed { get; set; } = 1.0f;
}
