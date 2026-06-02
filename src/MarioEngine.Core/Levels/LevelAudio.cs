namespace MarioEngine.Core.Levels;

/// <summary>Static manager for level audio.</summary>
internal static class LevelAudio
{
    /// <summary>Background music track name.</summary>
    public static string BackgroundMusic { get; set; } = string.Empty;

    /// <summary>Ambient sound name.</summary>
    public static string AmbientSound { get; set; } = string.Empty;

    /// <summary>Music volume (0-1).</summary>
    public static float MusicVolume { get; set; } = 1f;

    /// <summary>Plays the current background music.</summary>
    public static void PlayMusic()
    {
    }

    /// <summary>Stops the current background music.</summary>
    public static void StopMusic()
    {
    }

    /// <summary>Sets the ambient sound by name.</summary>
    public static void SetAmbient(string name)
    {
        AmbientSound = name;
    }
}
