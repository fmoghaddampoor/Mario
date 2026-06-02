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

    /// <summary>Gets the "Debug overlay toggled on" log message.</summary>
    public static string DebugOverlay_ToggledOn => "Debug overlay toggled on";

    /// <summary>Gets the "Debug overlay toggled off" log message.</summary>
    public static string DebugOverlay_ToggledOff => "Debug overlay toggled off";

    /// <summary>Gets the "Audio initialized" log message with vendor, renderer, version.</summary>
    public static string Audio_Initialized => "Audio initialized: {Vendor} / {Renderer} / {Version}";

    /// <summary>Gets the "Audio init failed" log message.</summary>
    public static string Audio_InitFailed => "Failed to initialize OpenAL audio";

    /// <summary>Gets the "No audio device found" log message.</summary>
    public static string Audio_NoDevice => "No audio device found";

    /// <summary>Gets the "Could not create audio context" log message.</summary>
    public static string Audio_NoContext => "Could not create OpenAL context";

    /// <summary>Gets the "Running in silent mode (no audio)" log message.</summary>
    public static string Audio_SilentMode => "Audio unavailable — running in silent mode";

    /// <summary>Gets the "Audio shutdown" log message.</summary>
    public static string Audio_Shutdown => "Audio shutdown complete";

    /// <summary>Gets the "Loaded SFX" log message with name, format, rate, duration.</summary>
    public static string Sfx_Loaded => "Loaded SFX: {Name} ({Format}, {Rate}Hz, {Duration:F2}s)";

    /// <summary>Gets the "All SFX buffers unloaded" log message.</summary>
    public static string Sfx_UnloadedAll => "All SFX buffers unloaded";
}
