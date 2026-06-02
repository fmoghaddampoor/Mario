namespace MarioEngine.Core.UI;

/// <summary>Level complete screen with score breakdown and star rating.</summary>
internal sealed class LevelCompleteScreen
{
    /// <summary>Next level name to transition to.</summary>
    public string NextLevelName { get; set; } = string.Empty;

    /// <summary>Shows the screen with completion data.</summary>
    public void Show(LevelCompleteData data)
    {
    }

    /// <summary>Handles the continue action.</summary>
    public void OnContinue()
    {
    }
}
