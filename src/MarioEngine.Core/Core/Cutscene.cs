namespace MarioEngine.Core.Core;

/// <summary>Represents a cutscene with ordered frames.</summary>
internal sealed class Cutscene
{
    public string Name { get; init; } = string.Empty;
    public List<CutsceneFrame> Frames { get; init; } = new();
    public int CurrentFrame { get; private set; }
    public bool IsComplete => CurrentFrame >= Frames.Count;

    public void Reset() => CurrentFrame = 0;

    public void Advance()
    {
        if (!IsComplete) CurrentFrame++;
    }
}
