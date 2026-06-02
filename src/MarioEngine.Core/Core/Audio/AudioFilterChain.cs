namespace MarioEngine.Core.Core.Audio;

/// <summary>Placeholder EQ filter chain.</summary>
internal sealed class AudioFilterChain
{
    public float LowPass { get; set; } = 1.0f;
    public float HighPass { get; set; } = 0.0f;
    public List<AudioFilter> Filters { get; } = new();
}

internal sealed class AudioFilter
{
    public string Name { get; init; } = string.Empty;
    public float Value { get; set; }
}
