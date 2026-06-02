namespace MarioEngine.Core.Audio;

/// <summary>
/// Tracks audio system performance statistics for debugging and profiling.
/// Exposed via <see cref="AudioManager"/> for the debug overlay.
/// </summary>
public sealed class AudioProfiler
{
    /// <summary>Peak number of active SFX sources this frame.</summary>
    private int _peakActiveSources;

    /// <summary>Total number of SFX played since startup.</summary>
    private int _totalSfxPlayed;

    /// <summary>Number of SFX instances that were stolen (priority override) since startup.</summary>
    private int _stolenSources;

    /// <summary>Total bytes of decoded PCM data streamed for music.</summary>
    private long _totalMusicBytesStreamed;

    /// <summary>Gets the current peak active source count.</summary>
    public int PeakActiveSources => _peakActiveSources;

    /// <summary>Gets the total number of SFX played since startup.</summary>
    public int TotalSfxPlayed => _totalSfxPlayed;

    /// <summary>Gets the number of source steals (priority overrides).</summary>
    public int StolenSources => _stolenSources;

    /// <summary>Gets the total bytes streamed for music playback.</summary>
    public long TotalMusicBytesStreamed => _totalMusicBytesStreamed;

    /// <summary>Records an SFX being played.</summary>
    public void RecordSfxPlayed() => _totalSfxPlayed++;

    /// <summary>Records a source being stolen by a higher-priority sound.</summary>
    public void RecordSourceStolen() => _stolenSources++;

    /// <summary>Records music bytes streamed.</summary>
    /// <param name="bytes">Number of bytes decoded this frame.</param>
    public void RecordMusicBytesStreamed(long bytes) => _totalMusicBytesStreamed += bytes;

    /// <summary>Updates the peak active source count.</summary>
    /// <param name="currentActive">Current number of active sources.</param>
    public void UpdatePeak(int currentActive)
    {
        if (currentActive > _peakActiveSources)
        {
            _peakActiveSources = currentActive;
        }
    }

    /// <summary>Resets all counters.</summary>
    public void Reset()
    {
        _peakActiveSources = 0;
        _totalSfxPlayed = 0;
        _stolenSources = 0;
        _totalMusicBytesStreamed = 0;
    }
}
