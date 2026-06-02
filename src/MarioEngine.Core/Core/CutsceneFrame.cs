namespace MarioEngine.Core.Core;

/// <summary>A single frame in a cutscene.</summary>
internal sealed class CutsceneFrame
{
    public string Speaker { get; init; } = string.Empty;
    public string Text { get; init; } = string.Empty;
    public float DisplayDuration { get; init; }
    public string? AnimationTrigger { get; init; }
}
