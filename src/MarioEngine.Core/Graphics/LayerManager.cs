namespace MarioEngine.Core.Graphics;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

/// <summary>
/// Manages render layers with parallax factors, visibility toggles, and sprite counting.
/// Provides layer-based sorting for the SpriteBatcher and debug overlay data.
/// </summary>
public sealed class LayerManager
{
    private readonly ILogger<LayerManager> _logger;
    private readonly Dictionary<RenderLayer, LayerData> _layers;

    /// <summary>
    /// Initializes a new instance of the <see cref="LayerManager"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if logger is null.</exception>
    public LayerManager(ILogger<LayerManager> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _layers = new Dictionary<RenderLayer, LayerData>();

        foreach (var layer in Enum.GetValues<RenderLayer>())
        {
            _layers[layer] = new LayerData
            {
                ParallaxFactor = DefaultParallax(layer),
                Visible = true,
                SortY = layer == RenderLayer.Entities,
            };
        }
    }

    /// <summary>Gets the total sprite count across all visible layers this frame.</summary>
    public int TotalSpriteCount
    {
        get
        {
            var total = 0;
            foreach (var kvp in _layers)
            {
                total += kvp.Value.SpriteCount;
            }

            return total;
        }
    }

    /// <summary>
    /// Gets the parallax factor for a given layer.
    /// </summary>
    /// <param name="layer">The render layer to query.</param>
    /// <returns>Parallax multiplier (0 = static, 1 = full camera).</returns>
    public float GetParallax(RenderLayer layer)
    {
        return _layers.TryGetValue(layer, out var data) ? data.ParallaxFactor : 1f;
    }

    /// <summary>
    /// Sets the parallax factor for a layer.
    /// </summary>
    /// <param name="layer">The render layer to modify.</param>
    /// <param name="factor">Parallax multiplier (0 = static, 1 = full camera).</param>
    public void SetParallax(RenderLayer layer, float factor)
    {
        if (_layers.TryGetValue(layer, out var data))
        {
            data.ParallaxFactor = factor;
        }
    }

    /// <summary>
    /// Gets whether a layer is visible.
    /// </summary>
    /// <param name="layer">The render layer to query.</param>
    /// <returns>True if the layer should be rendered.</returns>
    public bool IsVisible(RenderLayer layer)
    {
        return _layers.TryGetValue(layer, out var data) && data.Visible;
    }

    /// <summary>
    /// Sets whether a layer is visible.
    /// </summary>
    /// <param name="layer">The render layer to modify.</param>
    /// <param name="visible">Whether to render this layer.</param>
    public void SetVisible(RenderLayer layer, bool visible)
    {
        if (_layers.TryGetValue(layer, out var data))
        {
            data.Visible = visible;
        }
    }

    /// <summary>
    /// Gets whether Y-sorting is enabled for a layer.
    /// </summary>
    /// <param name="layer">The render layer to query.</param>
    /// <returns>True if sprites in this layer should be sorted by Y position.</returns>
    public bool IsYSorted(RenderLayer layer)
    {
        return _layers.TryGetValue(layer, out var data) && data.SortY;
    }

    /// <summary>
    /// Increments the sprite count for a layer (called by renderer each frame).
    /// </summary>
    /// <param name="layer">The render layer to count.</param>
    public void CountSprite(RenderLayer layer)
    {
        if (_layers.TryGetValue(layer, out var data))
        {
            data.SpriteCount++;
        }
    }

    /// <summary>Resets all layer sprite counters (call at start of each frame).</summary>
    public void ResetCounts()
    {
        foreach (var kvp in _layers)
        {
            kvp.Value.SpriteCount = 0;
        }
    }

    /// <summary>
    /// Gets the sprite count for a specific layer.
    /// </summary>
    /// <param name="layer">The render layer to query.</param>
    /// <returns>Number of sprites rendered in this layer this frame.</returns>
    public int GetSpriteCount(RenderLayer layer)
    {
        return _layers.TryGetValue(layer, out var data) ? data.SpriteCount : 0;
    }

    /// <summary>
    /// Returns a debug string with sprite counts per layer.
    /// </summary>
    /// <returns>Multi-line string with visibility, count, and parallax per layer.</returns>
    public string GetDebugOverlay()
    {
        var result = string.Empty;
        foreach (var layer in Enum.GetValues<RenderLayer>())
        {
            if (_layers.TryGetValue(layer, out var data))
            {
                var vis = data.Visible ? "V" : "H";
                result += $"{vis} {layer}:{data.SpriteCount} px{data.ParallaxFactor:F1}\n";
            }
        }

        result += $"Total: {TotalSpriteCount}";
        return result;
    }

    private static float DefaultParallax(RenderLayer layer)
    {
        return layer switch
        {
            RenderLayer.Background => 0f,
            RenderLayer.FarParallax => 0.1f,
            RenderLayer.MidParallax => 0.3f,
            RenderLayer.NearParallax => 0.6f,
            RenderLayer.Ground => 1f,
            RenderLayer.Entities => 1f,
            RenderLayer.Foreground => 1f,
            RenderLayer.Particles => 1f,
            RenderLayer.UI => 0f,
            _ => 1f,
        };
    }

    private sealed class LayerData
    {
        public float ParallaxFactor { get; set; } = 1f;

        public bool Visible { get; set; } = true;

        public bool SortY { get; set; }

        public int SpriteCount { get; set; }
    }
}
