namespace MarioEngine.Core.Save;

/// <summary>Manages the save/load menu flow with multiple slot UIs.</summary>
internal sealed class SaveUI
{
    public List<SaveSlotUI> Slots { get; } = new();
    private int _selectedIndex;

    public SaveUI()
    {
        for (int i = 0; i < 3; i++)
            Slots.Add(new SaveSlotUI { SlotIndex = i });
    }

    public void Show()
    {
        Console.WriteLine("=== Save / Load ===");
        foreach (var slot in Slots)
            slot.Render();
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= Slots.Count) return;
        _selectedIndex = index;
        Console.WriteLine($"Selected slot {index + 1}");
    }
}
