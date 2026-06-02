namespace MarioEngine.Core.UI;

using System.Collections.Generic;

/// <summary>Pause menu overlay.</summary>
internal sealed class PauseMenu
{
    /// <summary>Pause menu items.</summary>
    public List<MenuItem> Items { get; set; } = new()
    {
        new MenuItem { Label = "Resume" },
        new MenuItem { Label = "Settings" },
        new MenuItem { Label = "Restart Level" },
        new MenuItem { Label = "Quit to Menu" },
    };

    /// <summary>Whether the game is paused.</summary>
    public bool IsPaused { get; private set; }

    /// <summary>Toggles the pause state.</summary>
    public void Toggle()
    {
        IsPaused = !IsPaused;
    }
}
