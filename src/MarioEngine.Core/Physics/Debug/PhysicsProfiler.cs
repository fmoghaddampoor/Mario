namespace MarioEngine.Core.Physics.Debug;

/// <summary>
/// Performance profiler for the physics system.
/// Tracks broad-phase, narrow-phase, and active body counts each frame.
/// </summary>
internal sealed class PhysicsProfiler
{
    /// <summary>Gets or sets the number of broad-phase tests this frame.</summary>
    internal int BroadphaseTests { get; set; }

    /// <summary>Gets or sets the number of narrow-phase tests this frame.</summary>
    internal int NarrowphaseTests { get; set; }

    /// <summary>Gets or sets the number of active (non-static) bodies this frame.</summary>
    internal int ActiveBodies { get; set; }

    /// <summary>
    /// Resets all counters to zero at the start of a new frame.
    /// </summary>
    internal void BeginFrame()
    {
        BroadphaseTests = 0;
        NarrowphaseTests = 0;
        ActiveBodies = 0;
    }

    /// <summary>
    /// Finalizes profiling for the current frame.
    /// Currently a no-op but reserved for logging or aggregation.
    /// </summary>
    internal void EndFrame()
    {
    }
}
