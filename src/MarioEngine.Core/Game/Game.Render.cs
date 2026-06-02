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

        _renderer.Font = _font;
        _renderer.Clear(0.1f, 0.1f, 0.18f);
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

    /// <summary>Renders the main menu screen with rich premium aesthetics.</summary>
    private void RenderMainMenu()
    {
        var renderer = _renderer!;
        var viewW = renderer.Camera.ViewportWidth;
        var viewH = renderer.Camera.ViewportHeight;
        var time = Time.TotalTime;

        // 1. Base Background
        renderer.DrawScreenRect(0, 0, viewW, viewH, MenuBgColor, MenuLayer);

        // 2. Subtle Background Grid Pattern
        var gridSpacing = 40f;
        var gridColor = 0xFF22223Bu; // Deep purple/blue grid lines
        for (float x = 0; x < viewW; x += gridSpacing)
        {
            renderer.DrawScreenRect(x, 0, 1, viewH, gridColor, MenuLayer - 0.1f);
        }
        for (float y = 0; y < viewH; y += gridSpacing)
        {
            renderer.DrawScreenRect(0, y, viewW, 1, gridColor, MenuLayer - 0.1f);
        }

        // 3. Floating Retro Ambient Blocks on Sides
        float floatOffset = MathF.Sin(time * 2f) * 15f;
        // Left decorative block
        renderer.DrawScreenRect(150, (viewH * 0.4f) + floatOffset, 40, 40, 0xFFD4AF37u, MenuLayer - 0.5f); // Golden Question block look
        renderer.DrawScreenRect(155, (viewH * 0.4f) + floatOffset + 5, 30, 30, 0xFF8B6508u, MenuLayer - 0.6f);
        renderer.DrawStringScreen("?", 166, (viewH * 0.4f) + floatOffset + 12, 0xFFFFFFFFu, MenuLayer - 0.7f, 2f);

        // Right decorative block
        renderer.DrawScreenRect(viewW - 190, (viewH * 0.5f) - floatOffset, 40, 40, 0xFFB22222u, MenuLayer - 0.5f); // Red brick block look
        renderer.DrawScreenRect(viewW - 185, (viewH * 0.5f) - floatOffset + 5, 30, 30, 0xFF5C0606u, MenuLayer - 0.6f);

        // 4. Header Bar
        renderer.DrawScreenRect(0, 0, viewW, 90, 0xFF0F172Au, MenuLayer - 1); // Darker blue slate header
        renderer.DrawScreenRect(0, 88, viewW, 3, 0xFFFFD700u, MenuLayer - 1.5f); // Gold accent line at the bottom of header

        // 5. Render Title Text with double drop shadow
        var titleText = "SUPER MARIO GAME";
        var titleScale = 3f;
        var titleWidth = titleText.Length * 8f * titleScale;
        var titleX = (viewW - titleWidth) * 0.5f;
        var titleY = 25f;

        // Shadow 1 (Deep offset)
        renderer.DrawStringScreen(titleText, titleX + 4, titleY + 4, 0xFF000000u, MenuLayer - 2f, titleScale);
        // Shadow 2 (Accent outline)
        renderer.DrawStringScreen(titleText, titleX + 2, titleY + 2, 0xFF4F46E5u, MenuLayer - 2.5f, titleScale);
        // Main Text
        renderer.DrawStringScreen(titleText, titleX, titleY, MenuTitleColor, MenuLayer - 3f, titleScale);

        // 6. Menu items
        var menu = _ui.MainMenu.Items;
        var selectedIndex = _ui.MainMenu.SelectedIndex;
        var itemW = 320f;
        var itemH = 54f;
        var startY = viewH * 0.35f;
        var startX = (viewW - itemW) * 0.5f;
        var spacing = 14f;

        for (var i = 0; i < menu.Count; i++)
        {
            var y = startY + (i * (itemH + spacing));
            var isSelected = i == selectedIndex;

            // Slide animation on selection
            var slideX = isSelected ? 15f + MathF.Sin(time * 8f) * 2f : 0f;
            var currentX = startX + slideX;

            // Drop shadow for button
            renderer.DrawScreenRect(currentX + 6, y + 6, itemW, itemH, 0x66000000u, MenuLayer - 0.2f);

            // Button border (outer highlight)
            var borderColor = isSelected ? 0xFFFFD700u : 0xFF374151u; // Gold for active, dark grey for inactive
            renderer.DrawScreenRect(currentX - 2, y - 2, itemW + 4, itemH + 4, borderColor, MenuLayer - 0.4f);

            // Button inner background
            var bgColor = isSelected ? 0xFF1E293Bu : 0xFF0F172Au; // Slate-800 for active, slate-900 for inactive
            renderer.DrawScreenRect(currentX, y, itemW, itemH, bgColor, MenuLayer - 0.5f);

            // Left accent bar
            var barColor = isSelected ? 0xFFFFD700u : 0xFF4B5563u;
            renderer.DrawScreenRect(currentX, y, 6, itemH, barColor, MenuLayer - 0.6f);

            // Text coordinates
            var itemText = menu[i].Label;
            var itemTextW = itemText.Length * 8f * 2f;
            var tx = currentX + (itemW - itemTextW) * 0.5f;
            var ty = y + (itemH - 16f) * 0.5f;

            // Draw Selector Arrow for active item
            if (isSelected)
            {
                var arrowOffset = MathF.Sin(time * 10f) * 3f;
                renderer.DrawStringScreen(">", currentX - 25f + arrowOffset, ty, 0xFFFFD700u, MenuLayer - 0.8f, 2f);
                renderer.DrawStringScreen("<", currentX + itemW + 12f - arrowOffset, ty, 0xFFFFD700u, MenuLayer - 0.8f, 2f);
            }

            var textColor = isSelected ? MenuTextSelectedColor : MenuTextColor;
            renderer.DrawStringScreen(itemText, tx, ty, textColor, MenuLayer - 0.7f, 2f);
        }

        // 7. Screen Scanlines (CRT overlay effect)
        var scanlineHeight = 2f;
        for (float y = 0; y < viewH; y += 4f)
        {
            renderer.DrawScreenRect(0, y, viewW, scanlineHeight, 0x1A000000u, MenuLayer - 5f); // 10% opaque black lines
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
