namespace MarioEngine.Core.UI;

/// <summary>Temporary notification bar for messages.</summary>
internal sealed class NotificationBar
{
    /// <summary>Current message text.</summary>
    public string Message { get; private set; } = string.Empty;

    /// <summary>Duration to display in seconds.</summary>
    public float DisplayDuration { get; set; } = 2f;

    /// <summary>Current display timer.</summary>
    public float Timer { get; private set; }

    /// <summary>Shows a notification message.</summary>
    public void Show(string msg)
    {
        Message = msg;
        Timer = DisplayDuration;
    }

    /// <summary>Updates the notification timer.</summary>
    public void Update(float dt)
    {
        if (Timer > 0f)
            Timer -= dt;
    }
}
