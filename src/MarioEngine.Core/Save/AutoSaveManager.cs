namespace MarioEngine.Core.Save;

/// <summary>Automatically saves the game at a configured interval.</summary>
internal sealed class AutoSaveManager
{
    public float AutoSaveInterval { get; set; } = 120f;
    public bool Enabled { get; set; } = true;

    private float _timer;
    private readonly SaveManager _saveManager;

    public AutoSaveManager(SaveManager saveManager)
    {
        _saveManager = saveManager;
    }

    public void Update(float dt)
    {
        if (!Enabled) return;
        _timer += dt;
        if (_timer >= AutoSaveInterval)
        {
            _timer = 0f;
            Save();
        }
    }

    public void Save()
    {
        _saveManager.Save(_saveManager.CurrentSlot);
    }
}
