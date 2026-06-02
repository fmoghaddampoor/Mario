namespace MarioEngine.Core.Audio;

/// <summary>
/// Result of a WAV file parse operation containing raw PCM data and format metadata.
/// </summary>
internal readonly struct WavData
{
    /// <summary>Raw PCM sample data.</summary>
    internal readonly byte[] Samples;

    /// <summary>Number of audio channels (1 = mono, 2 = stereo).</summary>
    internal readonly ushort Channels;

    /// <summary>Sample rate in Hz (e.g. 44100, 48000).</summary>
    internal readonly int SampleRate;

    /// <summary>Bits per sample (8 or 16).</summary>
    internal readonly ushort BitsPerSample;

    /// <summary>Initializes a new instance of the <see cref="WavData"/> struct.</summary>
    /// <param name="samples">Raw PCM sample data.</param>
    /// <param name="channels">Number of audio channels.</param>
    /// <param name="sampleRate">Sample rate in Hz.</param>
    /// <param name="bitsPerSample">Bits per sample.</param>
    internal WavData(byte[] samples, ushort channels, int sampleRate, ushort bitsPerSample)
    {
        Samples = samples;
        Channels = channels;
        SampleRate = sampleRate;
        BitsPerSample = bitsPerSample;
    }
}
