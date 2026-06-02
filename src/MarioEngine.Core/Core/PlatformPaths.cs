namespace MarioEngine.Core.Core;

/// <summary>Provides platform-appropriate file system paths.</summary>
internal static class PlatformPaths
{
    private static readonly string Base = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "MarioEngine");

    public static string GetSavePath() => Path.Combine(Base, "Saves");
    public static string GetConfigPath() => Path.Combine(Base, "Config");
    public static string GetLogPath() => Path.Combine(Base, "Logs");
    public static string GetScreenshotPath() => Path.Combine(Base, "Screenshots");
}
