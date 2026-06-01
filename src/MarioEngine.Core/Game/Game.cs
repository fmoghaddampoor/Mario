namespace MarioEngine.Core;

using MarioEngine.Core.Audio;
using MarioEngine.Core.Config;
using MarioEngine.Core.Debug;
using MarioEngine.Core.Graphics;
using MarioEngine.Core.Graphics.Font;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;

#pragma warning disable CA1001 // AudioManager disposed in Shutdown()
/// <summary>
/// Core game class with lifecycle methods called by the application window.
/// Override to add custom initialization, updating, and rendering logic.
/// This class is split across multiple files by lifecycle method.
/// </summary>
public partial class Game
#pragma warning restore CA1001
{
    /// <summary>Layer value used for debug overlay rendering (rendered on top of everything).</summary>
    private const float DebugOverlayLayer = 1000f;

    /// <summary>Logger instance for game lifecycle events.</summary>
    private readonly ILogger<Game> _logger;

    /// <summary>Tracks per-frame performance metrics (FPS, frame time, etc.).</summary>
    private readonly PerformanceMetrics _metrics;

    /// <summary>In-game debug overlay for displaying performance data.</summary>
    private readonly DebugOverlay _overlay;

    /// <summary>Audio manager for OpenAL context and sound lifecycle.</summary>
    private readonly AudioManager _audio;

    /// <summary>2D renderer for drawing sprites, text, and primitives.</summary>
    private Renderer2D? _renderer;

    /// <summary>Bitmap font for text rendering (loaded during content load).</summary>
    private BitmapFont? _font;

    /// <summary>Initializes a new instance of the <see cref="Game"/> class.</summary>
    /// <param name="config">Game configuration with audio and other settings.</param>
    /// <param name="logger">Logger instance for this class.</param>
    public Game(GameConfig config, ILogger<Game> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        ArgumentNullException.ThrowIfNull(config);
        _metrics = new PerformanceMetrics();
        _overlay = new DebugOverlay(_metrics, _logger);
        _audio = new AudioManager(config.Audio, _logger);
    }

    /// <summary>Gets the performance metrics collector for this game session.</summary>
    public PerformanceMetrics Metrics => _metrics;

    /// <summary>Gets the debug overlay instance for this game session.</summary>
    public DebugOverlay Overlay => _overlay;

    /// <summary>Gets the audio manager for this game session.</summary>
    public AudioManager Audio => _audio;

    /// <summary>Gets or sets the 2D renderer used for drawing. Set after GL context is created.</summary>
    public Renderer2D? Renderer
    {
        get => _renderer;
        set => _renderer = value;
    }

    /// <summary>Gets or sets the bitmap font for text rendering. Set during content loading.</summary>
    public BitmapFont? Font
    {
        get => _font;
        set => _font = value;
    }

    /// <summary>
    /// Toggles the debug overlay visibility. Called when F3 is pressed.
    /// </summary>
    public void ToggleDebugOverlay()
    {
        _overlay.Toggle();
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation(
                _overlay.Visible ? Resources.Strings.DebugOverlay_ToggledOn : Resources.Strings.DebugOverlay_ToggledOff);
        }
    }
}
