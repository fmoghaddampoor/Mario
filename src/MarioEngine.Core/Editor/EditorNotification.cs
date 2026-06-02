namespace MarioEngine.Core.Editor;

/// <summary>Toast-style notification for editor feedback.</summary>
internal sealed class EditorNotification
{
    public string Message { get; set; } = "";
    public float Duration { get; set; } = 1.5f;
    public bool IsVisible { get; private set; }
    private float _timer;

    public void Show(string msg)
    {
        Message = msg;
        _timer = 0f;
        IsVisible = true;
    }

    public void Update(float dt)
    {
        if (!IsVisible) return;
        _timer += dt;
        if (_timer >= Duration)
            IsVisible = false;
    }
}
