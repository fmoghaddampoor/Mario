namespace MarioEngine.Core.Graphics.Debug;

/// <summary>Screen transition effects between scenes.</summary>
internal enum TransitionStyle { Fade, Wipe, Circle, Slide }

internal sealed class ScreenTransition
{
    public TransitionStyle Style { get; set; }
    public float Duration { get; set; } = 0.5f;
    public bool IsActive { get; private set; }
    private float _elapsed;

    public void Start()
    {
        IsActive = true;
        _elapsed = 0f;
    }

    public void Update(float dt)
    {
        if (!IsActive) return;
        _elapsed += dt;
        if (_elapsed >= Duration) IsActive = false;
    }

    public uint GetTransitionColor(float progress)
    {
        byte alpha = (byte)(progress * 255);
        return (uint)(alpha << 24 | 0xFFFFFF);
    }
}
