namespace MarioEngine.Core.UI;

using System.Collections.Generic;

/// <summary>Main menu screen.</summary>
public sealed class MainMenu
{
    /// <summary>Menu items.</summary>
    public List<MenuItem> Items { get; set; } = new()
    {
        new MenuItem { Label = "New Game" },
        new MenuItem { Label = "Continue" },
        new MenuItem { Label = "Settings" },
        new MenuItem { Label = "Credits" },
        new MenuItem { Label = "Quit" },
    };

    /// <summary>Currently selected index.</summary>
    public int SelectedIndex { get; set; }

    /// <summary>Moves selection up.</summary>
    public void NavigateUp()
    {
        if (SelectedIndex > 0)
            SelectedIndex--;
    }

    /// <summary>Moves selection down.</summary>
    public void NavigateDown()
    {
        if (SelectedIndex < Items.Count - 1)
            SelectedIndex++;
    }

    /// <summary>Confirms the current selection.</summary>
    public void Confirm()
    {
    }
}
