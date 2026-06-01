namespace MarioEngine.Core.Resources;

/// <summary>
/// String table containing all localizable strings for the Core project.
/// All UI text, log messages, and error messages should reference this class.
/// </summary>
internal static class Strings
{
    /// <summary>Gets the "Game initializing" log message.</summary>
    public static string Game_Initializing => "Game initializing";

    /// <summary>Gets the "Game initialized" log message.</summary>
    public static string Game_Initialized => "Game initialized";

    /// <summary>Gets the "Game.LoadContent started" log message.</summary>
    public static string Game_LoadContent_Started => "Game.LoadContent started";

    /// <summary>Gets the "Game.Shutdown started" log message.</summary>
    public static string Game_Shutdown_Started => "Game.Shutdown started";

    /// <summary>Gets the "Game shutting down" log message.</summary>
    public static string Game_ShuttingDown => "Game shutting down";

    /// <summary>Gets the logging initialized log message with format placeholders.</summary>
    public static string Logging_Initialized => "Logging initialized. Seq: {Seq}, Loki: {Loki}";

    /// <summary>Gets the "DI container initialized" log message.</summary>
    public static string DI_Container_Initialized => "DI container initialized";
}
