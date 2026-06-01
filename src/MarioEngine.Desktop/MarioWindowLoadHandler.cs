namespace MarioEngine.Desktop;

using Serilog;

/// <summary>
/// Handles the window Load event. Creates the splash screen.
/// </summary>
internal sealed class MarioWindowLoadHandler
{
    private readonly MarioWindow _window;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowLoadHandler"/> class.</summary>
    /// <param name="window">The MarioWindow instance providing the OpenGL context.</param>
    public MarioWindowLoadHandler(MarioWindow window)
    {
        _window = window;
    }

    /// <summary>Called when the window loads. Creates the splash screen.</summary>
    public void Handle()
    {
        _window.CreateSplash();
    }
}
