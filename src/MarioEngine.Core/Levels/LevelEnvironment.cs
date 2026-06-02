namespace MarioEngine.Core.Levels;

/// <summary>Weather types for level environments.</summary>
internal enum Weather
{
    /// <summary>No weather effects.</summary>
    None,

    /// <summary>Rain.</summary>
    Rain,

    /// <summary>Snow.</summary>
    Snow,

    /// <summary>Sandstorm.</summary>
    Sandstorm,

    /// <summary>Lava rain.</summary>
    LavaRain,
}

/// <summary>Environmental settings for a level.</summary>
internal sealed class LevelEnvironment
{
    /// <summary>Current weather type.</summary>
    public Weather CurrentWeather { get; set; } = Weather.None;

    /// <summary>Particle density for weather effects.</summary>
    public float ParticleDensity { get; set; }

    /// <summary>Wind direction vector.</summary>
    public Vector2 WindDirection { get; set; }

    /// <summary>Wind strength.</summary>
    public float WindStrength { get; set; }
}
