namespace MarioEngine.Core.UI;

/// <summary>Loading screen with progress bar and random tips.</summary>
internal sealed class LoadingScreen
{
    /// <summary>Progress from 0 to 1.</summary>
    public float Progress { get; set; }

    /// <summary>Current tip text.</summary>
    public string TipText { get; set; } = string.Empty;

    /// <summary>Shows the loading screen.</summary>
    public void Show()
    {
    }

    /// <summary>Hides the loading screen.</summary>
    public void Hide()
    {
    }

    /// <summary>Updates the loading screen each frame.</summary>
    public void Update(float dt)
    {
    }
}
