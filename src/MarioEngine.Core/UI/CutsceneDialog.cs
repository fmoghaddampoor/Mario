namespace MarioEngine.Core.UI;

/// <summary>Cutscene dialog box with speaker portrait and typewriter text.</summary>
internal sealed class CutsceneDialog
{
    /// <summary>Speaker name.</summary>
    public string SpeakerName { get; private set; } = string.Empty;

    /// <summary>Current dialog text.</summary>
    public string DialogText { get; private set; } = string.Empty;

    /// <summary>Character display speed in characters per second.</summary>
    public float CharDisplaySpeed { get; set; } = 30f;

    /// <summary>Shows dialog from a speaker.</summary>
    public void Show(string speaker, string text)
    {
        SpeakerName = speaker;
        DialogText = text;
    }

    /// <summary>Updates the typewriter effect each frame.</summary>
    public void Update(float dt)
    {
    }
}
