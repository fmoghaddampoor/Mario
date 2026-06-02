namespace MarioEngine.Core.Core;

/// <summary>Periodically triggers an auto-save callback.</summary>
internal sealed class AutoSaver
{
    public float Interval { get; set; } = 300f;
    public bool Enabled { get; set; }
    public Action? OnAutoSave { get; set; }

    private float _elapsed;

    public void Update(float dt)
    {
        if (!Enabled) return;
        _elapsed += dt;
        if (_elapsed >= Interval)
        {
            _elapsed = 0f;
            OnAutoSave?.Invoke();
        }
    }
}
