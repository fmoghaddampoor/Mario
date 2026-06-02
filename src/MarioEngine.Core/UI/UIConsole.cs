namespace MarioEngine.Core.UI;

using System.Collections.Generic;

/// <summary>Developer console overlay bound to ~/F1.</summary>
internal sealed class UIConsole
{
    /// <summary>Console output lines.</summary>
    public List<string> Lines { get; } = new();

    /// <summary>Current input text.</summary>
    public string CurrentInput { get; set; } = string.Empty;

    /// <summary>Toggles the console visibility.</summary>
    public void Toggle()
    {
    }

    /// <summary>Executes a console command.</summary>
    public void ExecuteCommand(string cmd)
    {
    }

    /// <summary>Adds a line to the console output.</summary>
    public void AddLine(string text)
    {
        Lines.Add(text);
    }
}
