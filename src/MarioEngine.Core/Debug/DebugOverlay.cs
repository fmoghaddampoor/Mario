namespace MarioEngine.Core.Debug;

using System;
using Microsoft.Extensions.Logging;

/// <summary>
/// In-game debug overlay renderer. Displays FPS, frame time, draw calls,
/// memory, physics body count, audio channels, and a frame time history graph.
/// Toggle with F3 key. No performance impact when hidden.
/// </summary>
public sealed class DebugOverlay
{
    private readonly PerformanceMetrics _metrics;
    private readonly int _frameTimeGraphWidth = 200;
    private readonly int _frameTimeGraphHeight = 60;
    private float _targetFrameTime = 1f / 60f;
    private bool _visible;

    /// <summary>
    /// Initializes a new instance of the <see cref="DebugOverlay"/> class.
    /// </summary>
    /// <param name="metrics">Performance metrics instance.</param>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if metrics or logger is null.</exception>
    public DebugOverlay(PerformanceMetrics metrics, ILogger logger)
    {
        _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        ArgumentNullException.ThrowIfNull(logger);
    }

    /// <summary>Gets the performance metrics collector.</summary>
    public PerformanceMetrics Metrics => _metrics;

    /// <summary>Gets or sets a value indicating whether the overlay is visible.</summary>
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible != value)
            {
                _visible = value;
            }
        }
    }

    /// <summary>Gets or sets the target frame time in seconds for color coding.</summary>
    public float TargetFrameTime
    {
        get => _targetFrameTime;
        set => _targetFrameTime = value;
    }

    /// <summary>
    /// Toggles the overlay visibility.
    /// </summary>
    public void Toggle()
    {
        Visible = !Visible;
    }

    /// <summary>
    /// Returns the debug overlay text for rendering.
    /// </summary>
    /// <returns>Multi-line string with performance data, or null if hidden.</returns>
    public string? GetOverlayText()
    {
        if (!_visible)
        {
            return null;
        }

        var fps = _metrics.Fps;
        var frameMs = _metrics.FrameTimeMs;
        var drawCalls = _metrics.DrawCalls;
        var entities = _metrics.EntityCount;
        var physics = _metrics.PhysicsBodyCount;
        var memory = PerformanceMetrics.MemoryMb;
        var audio = _metrics.AudioChannelCount;

        var fpsColor = GetFpsColor(fps);
        var msColor = GetMsColor(frameMs);
        var graph = BuildFrameTimeGraph();

        return $"FPS:      {fps,6:F1} {fpsColor}\n" +
               $"Frame:    {frameMs,6:F1} ms {msColor}\n" +
               $"Draw:     {drawCalls,6}\n" +
               $"Entities: {entities,6}\n" +
               $"Physics:  {physics,6}\n" +
               $"Memory:   {memory,5:F1} MB\n" +
               $"Audio:    {audio,6}\n" +
               $"\nFrame Time (last 60):\n{graph}";
    }

    private static string GetFpsColor(float fps)
    {
        if (fps >= 55f)
        {
            return "(green)";
        }

        if (fps >= 30f)
        {
            return "(yellow)";
        }

        return "(red)";
    }

    private static string GetMsColor(float ms)
    {
        if (ms <= 16.67f)
        {
            return "(green)";
        }

        if (ms <= 33.33f)
        {
            return "(yellow)";
        }

        return "(red)";
    }

    private string BuildFrameTimeGraph()
    {
        if (!_visible)
        {
            return string.Empty;
        }

        var history = _metrics.FrameTimeHistory;
        var maxMs = _metrics.MaxFrameTime * 1000f;
        if (maxMs < 33.33f)
        {
            maxMs = 33.33f;
        }

        var graph = string.Empty;
        var count = Math.Min(history.Count, _frameTimeGraphWidth / 3);
        for (var i = 0; i < count; i++)
        {
            var pct = history[i] * 1000f / maxMs;
            var barHeight = (int)(pct * (_frameTimeGraphHeight / 100f));
            barHeight = Math.Clamp(barHeight, 1, _frameTimeGraphHeight);
            graph += new string('#', barHeight) + "\n";
        }

        return graph;
    }
}
