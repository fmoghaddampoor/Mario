namespace MarioEngine.Core.Save;

/// <summary>Displays a single save slot in the save/load menu.</summary>
internal sealed class SaveSlotUI
{
    public int SlotIndex { get; set; }
    public SaveData? Data { get; set; }
    public bool IsEmpty => Data == null;

    public void Render()
    {
        if (IsEmpty)
        {
            Console.WriteLine($"Slot {SlotIndex + 1}: [Empty]");
            return;
        }

        var ts = TimeSpan.FromSeconds(Data!.PlayTime);
        Console.WriteLine($"Slot {SlotIndex + 1}: {Data.PlayerName} | " +
            $"World {Data.World}-{Data.Level} | Coins: {Data.Coins} | " +
            $"Time: {ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}");
    }
}
