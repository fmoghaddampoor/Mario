namespace MarioEngine.Core.Core;

/// <summary>Manages cutscene playback.</summary>
internal sealed class CutsceneManager
{
    public List<Cutscene> Cutscenes { get; } = new();
    public Cutscene? ActiveCutscene { get; private set; }

    public void Play(string name)
    {
        ActiveCutscene = Cutscenes.Find(c => c.Name == name);
        ActiveCutscene?.Reset();
    }

    public void Stop() => ActiveCutscene = null;

    public void Update(float dt)
    {
        if (ActiveCutscene == null || ActiveCutscene.IsComplete)
        {
            if (ActiveCutscene?.IsComplete == true) ActiveCutscene = null;
            return;
        }
        ActiveCutscene.Advance();
    }
}
