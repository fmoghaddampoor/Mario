namespace MarioEngine.Core.Core.Audio;

/// <summary>Crossfades between two audio volumes.</summary>
internal sealed class AudioCrossfader
{
    public float FromVolume { get; set; }
    public float ToVolume { get; set; }
    public float Duration { get; set; }

    public float GetVolume(float progress)
    {
        float clamped = Math.Clamp(progress, 0f, 1f);
        return FromVolume + (ToVolume - FromVolume) * clamped;
    }
}
