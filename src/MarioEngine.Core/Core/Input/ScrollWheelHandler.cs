namespace MarioEngine.Core.Core.Input;

/// <summary>Accumulates scroll wheel input into discrete steps.</summary>
internal sealed class ScrollWheelHandler
{
    public float AccumulatedScroll { get; private set; }
    public float Threshold { get; set; } = 120f;

    public int GetScrollSteps()
    {
        int steps = (int)(AccumulatedScroll / Threshold);
        AccumulatedScroll -= steps * Threshold;
        return steps;
    }
}
