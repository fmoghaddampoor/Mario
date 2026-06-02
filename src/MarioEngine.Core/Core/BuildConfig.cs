namespace MarioEngine.Core.Core;

/// <summary>Build-time injected version and metadata.</summary>
internal static class BuildConfig
{
    public static string Version { get; } = "1.0.0";
    public static string BuildDate { get; } = "2026-06-02";
    public static string GitSHA { get; } = "0000000";
}
