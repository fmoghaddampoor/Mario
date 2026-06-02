namespace MarioEngine.Core.Core.Audio.Wav;

/// <summary>Trims leading and trailing silence from audio clips.</summary>
internal static class AudioClipTrimmer
{
    public static WavData TrimSilence(WavData wav, float threshold)
    {
        if (wav.Samples == null || wav.Samples.Length == 0) return wav;
        int start = 0, end = wav.Samples.Length - 1;
        while (start < end && Math.Abs(wav.Samples[start]) < threshold) start++;
        while (end > start && Math.Abs(wav.Samples[end]) < threshold) end--;
        var trimmed = new float[end - start + 1];
        Array.Copy(wav.Samples, start, trimmed, 0, trimmed.Length);
        return new WavData { Samples = trimmed, SampleRate = wav.SampleRate, Channels = wav.Channels };
    }
}

/// <summary>Raw WAV audio data.</summary>
internal sealed class WavData
{
    public float[]? Samples { get; set; }
    public int SampleRate { get; set; }
    public int Channels { get; set; }
}
