namespace MarioEngine.Core.Config;

using System;
using System.IO;
using System.Text.Json;

/// <summary>
/// Manages loading, saving, and merging of game configuration.
/// Reads from JSON file at platform-appropriate path.
/// Falls back to defaults if no config file exists.
/// Merges with command-line argument overrides.
/// </summary>
public sealed class ConfigManager
{
    /// <summary>Name of the configuration file.</summary>
    private const string ConfigFileName = "config.json";

    /// <summary>Directory for storing configuration files under AppData.</summary>
    private static readonly string ConfigDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "MarioGame");

    /// <summary>Full file path to the configuration JSON file.</summary>
    private static readonly string ConfigPath = Path.Combine(ConfigDirectory, ConfigFileName);

    /// <summary>Cached JSON serializer options for consistent formatting.</summary>
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        WriteIndented = true,
    };

    /// <summary>Initializes a new instance of the <see cref="ConfigManager"/> class. Use <see cref="Load"/> to create.</summary>
    private ConfigManager()
    {
    }

    /// <summary>Gets or sets the current game configuration.</summary>
    public GameConfig Config { get; set; } = new GameConfig();

    /// <summary>
    /// Loads configuration from disk, or creates defaults if no file exists.
    /// </summary>
    /// <returns>A loaded ConfigManager instance.</returns>
    public static ConfigManager Load()
    {
        var manager = new ConfigManager();

        if (File.Exists(ConfigPath))
        {
            try
            {
                var json = File.ReadAllText(ConfigPath);
                var loaded = JsonSerializer.Deserialize<GameConfig>(json);
                if (loaded != null)
                {
                    manager.Config = loaded;
                }
            }
            catch (JsonException)
            {
                // If deserialization fails, keep defaults
            }
            catch (IOException)
            {
                // If file is locked, keep defaults
            }
        }

        return manager;
    }

    /// <summary>
    /// Saves the current configuration to disk.
    /// </summary>
    /// <exception cref="IOException">Thrown if the file cannot be written.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown if access is denied.</exception>
    public void Save()
    {
        Directory.CreateDirectory(ConfigDirectory);
        var json = JsonSerializer.Serialize(Config, JsonOptions);
        File.WriteAllText(ConfigPath, json);
    }

    /// <summary>
    /// Merges command-line argument overrides into the current config.
    /// </summary>
    /// <param name="cliArgs">Parsed CLI arguments tuple.</param>
    public void MergeCliArgs((bool Fullscreen, int Width, int Height) cliArgs)
    {
        Config.Video.Fullscreen = cliArgs.Fullscreen;
        Config.Video.Width = cliArgs.Width;
        Config.Video.Height = cliArgs.Height;
    }

    /// <summary>
    /// Loads additional overrides from the legacy app.ini file.
    /// INI values override JSON defaults, but CLI args override INI.
    /// </summary>
    /// <param name="iniPath">Path to app.ini file.</param>
    public void MergeIniFile(string iniPath)
    {
        if (!File.Exists(iniPath))
        {
            return;
        }

        try
        {
            var ini = IniConfig.Load(iniPath);

            var splash = ini.GetFloat("Splash", "Duration", float.MinValue);
            if (splash > float.MinValue)
            {
                Config.Gameplay.SplashDuration = splash;
            }

            var seqUrl = ini.GetString("Logging", "SeqUrl");
            if (!string.IsNullOrEmpty(seqUrl))
            {
                Config.Audio.SeqUrl = seqUrl;
            }

            var lokiUrl = ini.GetString("Logging", "LokiUrl");
            if (!string.IsNullOrEmpty(lokiUrl))
            {
                Config.Audio.LokiUrl = lokiUrl;
            }
        }
        catch (IOException)
        {
            // Silently fail if ini file can't be read
        }
        catch (UnauthorizedAccessException)
        {
            // Silently fail if access denied
        }
    }
}
