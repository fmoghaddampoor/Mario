namespace MarioEngine.Core;

using System.Reflection;

/// <summary>
/// Provides version information for the game application.
/// Reads from assembly metadata and exposes structured version components.
/// </summary>
public static class VersionInfo
{
    /// <summary>Gets the full version string (e.g. "0.1.0-alpha").</summary>
    public static string Current { get; } = BuildVersion();

    /// <summary>Gets the major version number.</summary>
    public static int Major { get; } = 0;

    /// <summary>Gets the minor version number.</summary>
    public static int Minor { get; } = 1;

    /// <summary>Gets the patch version number.</summary>
    public static int Patch { get; } = 0;

    /// <summary>Gets the version suffix (e.g. "alpha", "beta", "rc").</summary>
    public static string Suffix { get; } = "alpha";

    private static string BuildVersion()
    {
        var version = Assembly.GetEntryAssembly()?.GetName().Version;
        if (version != null)
        {
            return $"{version.Major}.{version.Minor}.{version.Build}-{Suffix}";
        }

        return $"{Major}.{Minor}.{Patch}-{Suffix}";
    }
}
