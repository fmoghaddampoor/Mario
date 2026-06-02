namespace MarioEngine.Core.Save;

/// <summary>Save-related settings loaded from configuration.</summary>
internal sealed class SaveSettings
{
    public bool AutoSave { get; set; } = true;
    public bool CloudSave { get; set; }
    public int DefaultSlot { get; set; }
}
