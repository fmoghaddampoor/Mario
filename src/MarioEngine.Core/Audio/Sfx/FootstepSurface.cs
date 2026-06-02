namespace MarioEngine.Core.Audio.Sfx;

/// <summary>
/// Maps gameplay surfaces to their corresponding SFX names and properties.
/// Used by the footstep system to play surface-appropriate sounds.
/// </summary>
public readonly struct FootstepSurface
{
    /// <summary>Grass surface — soft footsteps.</summary>
    public static readonly FootstepSurface Grass = new FootstepSurface("sfx_footstep_grass", 0.6f, 1.0f);

    /// <summary>Stone surface — hard footsteps.</summary>
    public static readonly FootstepSurface Stone = new FootstepSurface("sfx_footstep_stone", 0.8f, 1.0f);

    /// <summary>Sand surface — muffled footsteps.</summary>
    public static readonly FootstepSurface Sand = new FootstepSurface("sfx_footstep_sand", 0.5f, 0.9f);

    /// <summary>Metal surface — loud, high-pitch footsteps.</summary>
    public static readonly FootstepSurface Metal = new FootstepSurface("sfx_footstep_metal", 1.0f, 1.2f);

    /// <summary>Snow surface — quiet, low-pitch footsteps.</summary>
    public static readonly FootstepSurface Snow = new FootstepSurface("sfx_footstep_snow", 0.4f, 0.8f);

    /// <summary>Water surface — splash sounds.</summary>
    public static readonly FootstepSurface Water = new FootstepSurface("sfx_footstep_water", 0.7f, 1.1f);

    /// <summary>Wood surface — hollow footsteps.</summary>
    public static readonly FootstepSurface Wood = new FootstepSurface("sfx_footstep_wood", 0.7f, 1.0f);

    /// <summary>Initializes a new instance of the <see cref="FootstepSurface"/> struct.</summary>
    /// <param name="sfxName">SFX name for the footstep sound.</param>
    /// <param name="volume">Base volume multiplier.</param>
    /// <param name="pitch">Base pitch multiplier.</param>
    public FootstepSurface(string sfxName, float volume, float pitch)
    {
        SfxName = sfxName;
        Volume = volume;
        Pitch = pitch;
    }

    /// <summary>Gets the SFX name for this surface.</summary>
    public string SfxName { get; }

    /// <summary>Gets the base volume for footstep sounds on this surface.</summary>
    public float Volume { get; }

    /// <summary>Gets the base pitch multiplier for this surface.</summary>
    public float Pitch { get; }
}
