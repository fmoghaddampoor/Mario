namespace MarioEngine.Core.UI;

using System;

/// <summary>Modal dialog with OK/Cancel buttons.</summary>
internal sealed class MessageBox
{
    /// <summary>Dialog title.</summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>Dialog message.</summary>
    public string Message { get; private set; } = string.Empty;

    /// <summary>Whether a cancel button is shown.</summary>
    public bool HasCancel { get; set; }

    /// <summary>Raised with true for OK, false for Cancel.</summary>
    public event Action<bool>? OnResult;

    /// <summary>Shows the message box.</summary>
    public void Show(string title, string msg)
    {
        Title = title;
        Message = msg;
    }
}
