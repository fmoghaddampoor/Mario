namespace MarioEngine.Core.Core;

/// <summary>Manages time attack mode with per-level best times.</summary>
internal sealed class TimeAttackManager
{
    public bool IsActive { get; private set; }
    public float Elapsed { get; private set; }
    public Dictionary<string, float> BestTime { get; } = new();

    public void Start()
    {
        IsActive = true;
        Elapsed = 0f;
    }

    public void Stop()
    {
        IsActive = false;
    }
}
