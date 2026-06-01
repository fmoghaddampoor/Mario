namespace MarioEngine.Core;

using MarioEngine.Core.Graphics;

/// <summary>
/// Contains the <see cref="Render"/> method for the <see cref="Game"/> class.
/// Called every frame to draw the current game state.
/// Renders the debug overlay on top if visible.
/// </summary>
public partial class Game
{
    /// <summary>
    /// Called every frame after <see cref="Update"/>.
    /// Override to render the current game state.
    /// Call base.Render(interpolation) to render the debug overlay.
    /// Begin/End are called automatically around the overlay rendering.
    /// If overriding, handle Begin/End for your own content separately.
    /// </summary>
    /// <param name="interpolation">Interpolation factor (0-1) between fixed updates.</param>
    public virtual void Render(float interpolation)
    {
        if (!_overlay.Visible || _renderer == null)
        {
            return;
        }

        _renderer.Begin();
        RenderDebugOverlay();
        _renderer.End();
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
