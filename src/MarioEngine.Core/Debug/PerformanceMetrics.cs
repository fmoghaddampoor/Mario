namespace MarioEngine.Core.Debug;

using System;
using System.Collections.Generic;

/// <summary>
/// Collects and tracks performance metrics each frame.
/// Provides FPS, frame time, memory, and custom counter tracking.
/// Maintains a rolling history of the last 60 frame times for graph rendering.
/// </summary>
public sealed class PerformanceMetrics
{
    private const int HistorySize = 60;

    private readonly float[] _frameTimeHistory;
    private int _historyIndex;
    private int _frameCount;
    private float _fpsTimer;
    private float _currentFps;
    private float _currentFrameTime;

    /// <summary>Initializes a new instance of the <see cref="PerformanceMetrics"/> class.</summary>
    public PerformanceMetrics()
    {
        _frameTimeHistory = new float[HistorySize];
    }

#pragma warning disable SA1204 // static members should come before non-static - intentional; SA1201 requires constructor before properties
    /// <summary>Gets the current managed memory in MB.</summary>
    public static float MemoryMb => (float)GC.GetTotalMemory(false) / (1024 * 1024);
#pragma warning restore SA1204

    /// <summary>Gets the current smoothed FPS.</summary>
    public float Fps => _currentFps;

    /// <summary>Gets the current frame time in milliseconds.</summary>
    public float FrameTimeMs => _currentFrameTime * 1000f;

    /// <summary>Gets the frame time history (last 60 frames) for graph rendering.</summary>
    public IReadOnlyList<float> FrameTimeHistory => _frameTimeHistory;

    /// <summary>Gets or sets the number of draw calls this frame.</summary>
    public int DrawCalls { get; set; }

    /// <summary>Gets or sets the number of active entities.</summary>
    public int EntityCount { get; set; }

    /// <summary>Gets or sets the number of active physics bodies.</summary>
    public int PhysicsBodyCount { get; set; }

    /// <summary>Gets or sets the number of active audio channels.</summary>
    public int AudioChannelCount { get; set; }

    /// <summary>Gets the minimum frame time in the current history.</summary>
    public float MinFrameTime { get; private set; }

    /// <summary>Gets the maximum frame time in the current history.</summary>
    public float MaxFrameTime { get; private set; }

    /// <summary>
    /// Updates the metrics. Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        _currentFrameTime = dt;
        _frameCount++;

        _frameTimeHistory[_historyIndex] = dt;
        _historyIndex = (_historyIndex + 1) % HistorySize;

        _fpsTimer += dt;
        if (_fpsTimer >= 1f)
        {
            _currentFps = _frameCount / _fpsTimer;
            _frameCount = 0;
            _fpsTimer = 0f;

            MinFrameTime = float.MaxValue;
            MaxFrameTime = float.MinValue;
            for (var i = 0; i < HistorySize; i++)
            {
                var t = _frameTimeHistory[i];
                if (t <= 0)
                {
                    continue;
                }

                if (t < MinFrameTime)
                {
                    MinFrameTime = t;
                }

                if (t > MaxFrameTime)
                {
                    MaxFrameTime = t;
                }
            }
        }
    }
}
