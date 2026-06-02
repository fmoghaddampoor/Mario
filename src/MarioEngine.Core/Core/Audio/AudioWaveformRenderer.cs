namespace MarioEngine.Core.Core.Audio;

/// <summary>Renders audio waveform for the editor.</summary>
internal sealed class AudioWaveformRenderer
{
    public float[] WaveformData { get; private set; } = Array.Empty<float>();

    public void LoadFromBuffer(SoundBuffer buffer)
    {
        WaveformData = buffer.GetSamples() ?? Array.Empty<float>();
    }
}

/// <summary>Placeholder for sound buffer.</summary>
internal sealed class SoundBuffer
{
    public float[]? GetSamples() => null;
}
