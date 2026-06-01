namespace MarioEngine.Core.Config;

using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Parses an INI configuration file and exposes typed configuration values.
/// Supports [Section] groupings and Key=Value pairs.
///
/// INI Format:
///   [SectionName]
///   Key=Value
///   ; comment
///   # comment
///
/// All keys are case-insensitive. Access via GetString/GetInt/GetBool/GetFloat.
///
/// Available INI Parameters:
///
/// [Splash]
///   Duration=3        — How many seconds the startup splash screen is displayed.
///
/// [Window]
///   Width=1920        — Default window width in pixels.
///   Height=1080       — Default window height in pixels.
///   Fullscreen=true   — Whether to start in fullscreen mode (true/false).
///
/// [Logging]
///   Level=Debug       — Minimum log level (Verbose, Debug, Information, Warning, Error, Fatal).
///   SeqUrl=           — Seq server URL for structured log viewing (e.g. http://localhost:5341).
///                        Leave empty to disable Seq.
///   LokiUrl=          — Grafana Loki URL for log aggregation (e.g. http://localhost:3100).
///                        Leave empty to disable Loki.
/// </summary>
public sealed class IniConfig
{
    /// <summary>Parsed INI key-value pairs. Keys use Section_Key format, case-insensitive.</summary>
    private readonly Dictionary<string, string> _values;

    /// <summary>
    /// Initializes a new instance of the <see cref="IniConfig"/> class.
    /// </summary>
    /// <param name="values">Parsed key-value pairs with section-prefixed keys.</param>
    private IniConfig(Dictionary<string, string> values)
    {
        _values = values;
    }

    /// <summary>
    /// Loads and parses an INI file from the given path.
    /// </summary>
    /// <param name="path">Full path to the .ini file.</param>
    /// <returns>A populated IniConfig instance.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
    /// <exception cref="IOException">Thrown if the file cannot be read.</exception>
    public static IniConfig Load(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Configuration file not found: " + path);
        }

        var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var currentSection = string.Empty;

        foreach (var rawLine in File.ReadAllLines(path))
        {
            var line = rawLine.Trim();

            if (line.Length == 0 || line.StartsWith("#", StringComparison.Ordinal) || line.StartsWith(";", StringComparison.Ordinal))
            {
                continue;
            }

            if (line.StartsWith("[", StringComparison.Ordinal) && line.EndsWith("]", StringComparison.Ordinal))
            {
                currentSection = line.Substring(1, line.Length - 2).Trim();
                continue;
            }

            var separatorIndex = line.IndexOf('=', StringComparison.Ordinal);
            if (separatorIndex < 0)
            {
                continue;
            }

            var key = line.Substring(0, separatorIndex).Trim();
            var value = line.Substring(separatorIndex + 1).Trim();

            if (key.Length == 0)
            {
                continue;
            }

            var fullKey = currentSection.Length > 0 ? currentSection + "_" + key : key;
            values[fullKey] = value;
        }

        return new IniConfig(values);
    }

    /// <summary>
    /// Gets a string value for the specified section and key.
    /// </summary>
    /// <param name="section">INI section name (without brackets).</param>
    /// <param name="key">Key within the section.</param>
    /// <param name="defaultValue">Default value if the key is not found or is empty.</param>
    /// <returns>The configured value, or the default.</returns>
    public string GetString(string section, string key, string defaultValue = "")
    {
        var fullKey = section + "_" + key;
        return _values.TryGetValue(fullKey, out var value) && value.Length > 0 ? value : defaultValue;
    }

    /// <summary>
    /// Gets an integer value for the specified section and key.
    /// </summary>
    /// <param name="section">INI section name (without brackets).</param>
    /// <param name="key">Key within the section.</param>
    /// <param name="defaultValue">Default value if the key is not found or cannot be parsed.</param>
    /// <returns>The configured value, or the default.</returns>
    public int GetInt(string section, string key, int defaultValue = 0)
    {
        var value = GetString(section, key);
        return int.TryParse(value, out var result) ? result : defaultValue;
    }

    /// <summary>
    /// Gets a boolean value for the specified section and key.
    /// Accepts "true", "false", "1", "0", "yes", "no" (case-insensitive).
    /// </summary>
    /// <param name="section">INI section name (without brackets).</param>
    /// <param name="key">Key within the section.</param>
    /// <param name="defaultValue">Default value if the key is not found or cannot be parsed.</param>
    /// <returns>The configured value, or the default.</returns>
    public bool GetBool(string section, string key, bool defaultValue = false)
    {
        var value = GetString(section, key).ToUpperInvariant();
        return value switch
        {
            "TRUE" or "1" or "YES" => true,
            "FALSE" or "0" or "NO" => false,
            _ => defaultValue,
        };
    }

    /// <summary>
    /// Gets a float value for the specified section and key.
    /// </summary>
    /// <param name="section">INI section name (without brackets).</param>
    /// <param name="key">Key within the section.</param>
    /// <param name="defaultValue">Default value if the key is not found or cannot be parsed.</param>
    /// <returns>The configured value, or the default.</returns>
    public float GetFloat(string section, string key, float defaultValue = 0f)
    {
        var value = GetString(section, key);
        return float.TryParse(value, out var result) ? result : defaultValue;
    }
}
