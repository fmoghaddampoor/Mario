namespace MarioEngine.Desktop.Resources;

/// <summary>
/// String table containing all localizable strings for the Desktop project.
/// All UI text, log messages, and error messages should reference this class.
/// </summary>
internal static class Strings
{
    /// <summary>Gets the window title prefix.</summary>
    public static string Window_Title => "Super Mario \u2014 v";

    /// <summary>Gets the "Window starting" log message.</summary>
    public static string Window_Starting => "Window starting";

    /// <summary>Gets the window opened log message with format placeholders.</summary>
    public static string Window_Opened => "Window opened: {Width}x{Height}, GL {Major}.{Minor}";

    /// <summary>Gets the framebuffer resized log message with format placeholders.</summary>
    public static string Framebuffer_Resized => "Framebuffer resized: {Width}x{Height}";

    /// <summary>Gets the "Splash screen created" log message.</summary>
    public static string Splash_Created => "Splash screen created";

    /// <summary>Gets the "Splash finished, starting game" log message.</summary>
    public static string Splash_Finished => "Splash finished, starting game";

    /// <summary>Gets the OpenGL not available error message.</summary>
    public static string GL_NotAvailable => "OpenGL context not available until window is loaded";

    /// <summary>Gets the shader link failed error message with format placeholder.</summary>
    public static string Shader_LinkFailed => "Shader program link failed: {0}";

    /// <summary>Gets the shader compile failed error message with format placeholders.</summary>
    public static string Shader_CompileFailed => "Shader compile failed ({0}): {1}";

    /// <summary>Gets the splash not found error message with format placeholder.</summary>
    public static string Splash_NotFound => "Splash image not found: {0}";

    /// <summary>Gets the unhandled exception fatal log message.</summary>
    public static string Fatal_UnhandledException => "Unhandled exception - game crashed";

    /// <summary>Gets the fatal error console message with format placeholder.</summary>
    public static string Fatal_ErrorMessage => "Fatal error: {0}";

    /// <summary>Gets the fullscreen enabled log message.</summary>
    public static string Video_FullscreenOn => "Fullscreen mode enabled";

    /// <summary>Gets the fullscreen disabled log message.</summary>
    public static string Video_FullscreenOff => "Fullscreen mode disabled";

    /// <summary>Gets the VSync enabled log message.</summary>
    public static string Video_VSyncOn => "VSync enabled";

    /// <summary>Gets the VSync disabled log message.</summary>
    public static string Video_VSyncOff => "VSync disabled";
}
