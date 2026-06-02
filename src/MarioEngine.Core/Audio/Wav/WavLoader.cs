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

    /// <summary>Channels field not yet read from fmt chunk.</summary>
    private const ushort ChannelsUnset = 0;

    /// <summary>Sample rate field not yet read from fmt chunk.</summary>
    private const int SampleRateUnset = 0;

    /// <summary>Bits per sample field not yet read from fmt chunk.</summary>
    private const ushort BitsUnset = 0;

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

        ushort channels = ChannelsUnset;
        int sampleRate = SampleRateUnset;
        ushort bitsPerSample = BitsUnset;
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

        if (channels == ChannelsUnset || sampleRate == SampleRateUnset || bitsPerSample == BitsUnset)
        {
            throw new InvalidDataException("WAV file is missing format information");
        }

        return new WavData(samples, channels, sampleRate, bitsPerSample);
    }
}
