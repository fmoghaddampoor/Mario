namespace MarioEngine.Core;

using MarioEngine.Core.Graphics;
using MarioEngine.Core.UI;

/// <summary>
/// Contains the <see cref="Render"/> method for the <see cref="Game"/> class.
/// Called every frame to draw the current game state.
/// Renders the main menu when in menu state, and debug overlay on top if visible.
/// </summary>
public partial class Game
{
    /// <summary>Menu background color (dark navy).</summary>
    private const uint MenuBgColor = 0xFF1A1A2Eu;

    /// <summary>Menu title color (gold).</summary>
    private const uint MenuTitleColor = 0xFFFFD700u;

    /// <summary>Menu item normal color.</summary>
    private const uint MenuItemColor = 0xFF2D2D44u;

    /// <summary>Menu item selected color (accent).</summary>
    private const uint MenuItemSelectedColor = 0xFF4A4A6Au;

    /// <summary>Menu item text color.</summary>
    private const uint MenuTextColor = 0xFFCCCCCCu;

    /// <summary>Menu item text color when selected.</summary>
    private const uint MenuTextSelectedColor = 0xFFFFFFFFu;

    /// <summary>Render layer for menu background.</summary>
    private const float MenuLayer = 900f;

    /// <summary>
    /// Called every frame after <see cref="Update"/>.
    /// Override to render the current game state.
    /// Renders menus, then the debug overlay on top.
    /// </summary>
    /// <param name="interpolation">Interpolation factor (0-1) between fixed updates.</param>
    public virtual void Render(float interpolation)
    {
        if (_renderer == null)
        {
            return;
        }

        _renderer.Begin();

        // Render UI screens
        switch (_ui.CurrentState)
        {
            case UIManager.UIState.MainMenu:
                RenderMainMenu();
                break;
            case UIManager.UIState.None:
            default:
                // Game rendering goes here when play starts
                break;
        }

        // Render debug overlay on top of everything
        if (_overlay.Visible)
        {
            RenderDebugOverlay();
        }

        _renderer.End();
    }

    /// <summary>Renders the main menu screen.</summary>
    private void RenderMainMenu()
    {
        var renderer = _renderer!;
        var viewW = renderer.Camera.ViewportWidth;
        var viewH = renderer.Camera.ViewportHeight;

        // Background
        renderer.DrawScreenRect(0, 0, viewW, viewH, MenuBgColor, MenuLayer);

        // Title bar at top
        renderer.DrawScreenRect(0, 0, viewW, 80, 0xFF16213Eu, MenuLayer - 1);

        // Title text area (colored bar)
        renderer.DrawScreenRect(viewW * 0.25f, 20, viewW * 0.5f, 40, 0xFF0F3460u, MenuLayer - 0.5f);

        // Menu items
        var menu = new[] { "New Game", "Settings", "Credits", "Quit" };
        var selectedIndex = 0; // Will be wired to MainMenu.SelectedIndex
        var itemW = 300f;
        var itemH = 50f;
        var startY = viewH * 0.35f;
        var startX = (viewW - itemW) * 0.5f;
        var spacing = 10f;

        for (var i = 0; i < menu.Length; i++)
        {
            var y = startY + (i * (itemH + spacing));
            var isSelected = i == selectedIndex;
            var color = isSelected ? MenuItemSelectedColor : MenuItemColor;
            renderer.DrawScreenRect(startX, y, itemW, itemH, color, MenuLayer - 1);

            // Draw a colored indicator on the left edge of selected item
            if (isSelected)
            {
                renderer.DrawScreenRect(startX, y, 5, itemH, MenuTitleColor, MenuLayer - 0.5f);
            }
        }
    }

    /// <summary>
    /// Parses a color tag suffix from a debug text line.
    /// Expected format: "label (color)" where color is green/yellow/red.
    /// </summary>
    /// <param name="line">The raw debug line with optional color tag.</param>
    /// <returns>Tuple of clean text and packed RGBA color.</returns>
    private static (string Text, uint Color) ParseColorTag(string line)
    {
        if (line.EndsWith("(green)", StringComparison.Ordinal))
        {
            return (line[..^7].TrimEnd(), 0xFF00FF00u);
        }

        if (line.EndsWith("(yellow)", StringComparison.Ordinal))
        {
            return (line[..^8].TrimEnd(), 0xFFFFAA00u);
        }

        if (line.EndsWith("(red)", StringComparison.Ordinal))
        {
            return (line[..^5].TrimEnd(), 0xFFFF0000u);
        }

        return (line, 0xFFFFFFFFu);
    }

    /// <summary>Renders the debug overlay text and frame time graph in screen space.</summary>
    private void RenderDebugOverlay()
    {
        var text = _overlay.GetOverlayText();
        if (text == null)
        {
            return;
        }

        _renderer!.Font = _font;

        var lines = text.Split('\n');
        var lineY = 10f;
        foreach (var line in lines)
        {
            if (line.StartsWith("Frame Time", StringComparison.Ordinal))
            {
                RenderFrameTimeGraph(lineY);
                lineY += 10f;
                continue;
            }

            var (cleanText, color) = ParseColorTag(line);
            _renderer.DrawStringScreen(cleanText, 10f, lineY, color, DebugOverlayLayer);
            lineY += _font?.LineHeight ?? 14;
        }
    }

    /// <summary>Renders the frame time history bars.</summary>
    /// <param name="startY">Screen Y position to start rendering the graph.</param>
    private void RenderFrameTimeGraph(float startY)
    {
        var history = _metrics.FrameTimeHistory;
        var count = history.Count;
        var barWidth = 3f;
        var barSpacing = 1f;
        var maxHeight = 60f;
        var maxMs = _metrics.MaxFrameTime * 1000f;
        if (maxMs < 33.33f)
        {
            maxMs = 33.33f;
        }

        for (var i = 0; i < count; i++)
        {
            var ms = history[i] * 1000f;
            var pct = ms / maxMs;
            var barHeight = Math.Clamp(pct * maxHeight, 1f, maxHeight);
            var color = ms <= 16.67f ? 0xFF00FF00u : ms <= 33.33f ? 0xFFFFAA00u : 0xFFFF0000u;
            var bx = 10f + (i * (barWidth + barSpacing));
            var by = startY + maxHeight - barHeight;
            _renderer!.DrawScreenRect(bx, by, barWidth, barHeight, color, DebugOverlayLayer);
        }
    }
}
