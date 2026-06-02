namespace MarioEngine.Core.Audio;

using System;
using System.IO;

/// <summary>
/// Parses uncompressed PCM WAV file data into raw PCM bytes and format info.
/// Supports 8-bit and 16-bit mono/stereo WAV files at any sample rate.
/// </summary>
internal static class WavLoader
{
    /// <summary>RIFF chunk ID expected at the start of every WAV file.</summary>
    private const uint RiffId = 0x46464952u;

    /// <summary>WAVE format ID expected after the RIFF header.</summary>
    private const uint WaveId = 0x45564157u;

    /// <summary>fmt sub-chunk ID containing format information.</summary>
    private const uint FmtId = 0x20746D66u;

    /// <summary>data sub-chunk ID containing audio samples.</summary>
    private const uint DataId = 0x61746164u;

    /// <summary>PCM audio format indicator in the fmt chunk.</summary>
    private const ushort PcmFormat = 1;

    /// <summary>
    /// Loads and parses a WAV file from disk.
    /// </summary>
    /// <param name="filePath">Absolute path to the .wav file.</param>
    /// <returns>Parsed WAV data containing PCM samples and format info.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    /// <exception cref="InvalidDataException">Thrown if the file is not a valid PCM WAV.</exception>
    internal static WavData Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"WAV file not found: {filePath}");
        }

        using var stream = File.OpenRead(filePath);
        using var reader = new BinaryReader(stream);

        var riffId = reader.ReadUInt32();
        if (riffId != RiffId)
        {
            throw new InvalidDataException("Not a valid RIFF file");
        }

        reader.ReadUInt32();
        var waveId = reader.ReadUInt32();
        if (waveId != WaveId)
        {
            throw new InvalidDataException("Not a valid WAVE file");
        }

        ushort channels = 0;
        int sampleRate = 0;
        ushort bitsPerSample = 0;
        byte[]? samples = null;

        while (stream.Position < stream.Length)
        {
            var chunkId = reader.ReadUInt32();
            var chunkSize = reader.ReadInt32();

            if (chunkId == FmtId)
            {
                var audioFormat = reader.ReadUInt16();
                if (audioFormat != PcmFormat)
                {
                    throw new InvalidDataException("Only uncompressed PCM WAV is supported");
                }

                channels = reader.ReadUInt16();
                sampleRate = reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadUInt16();
                bitsPerSample = reader.ReadUInt16();
            }
            else if (chunkId == DataId)
            {
                samples = reader.ReadBytes(chunkSize);
            }
            else
            {
                reader.ReadBytes(chunkSize);
            }

            if (chunkSize % 2 != 0)
            {
                reader.ReadByte();
            }
        }

        if (samples == null || samples.Length == 0)
        {
            throw new InvalidDataException("WAV file contains no audio data");
        }

        if (channels == 0 || sampleRate == 0 || bitsPerSample == 0)
        {
            throw new InvalidDataException("WAV file is missing format information");
        }

        return new WavData(samples, channels, sampleRate, bitsPerSample);
    }

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
}
