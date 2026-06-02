using System.Text.Json;

namespace MarioEngine.Core.Save;

/// <summary>Manages save file operations with JSON serialization.</summary>
internal sealed class SaveManager
{
    public string SaveDirectory { get; set; } = "Saves";
    public int CurrentSlot { get; set; }
    public SaveData[] Slots { get; } = new SaveData[3];

    public void Save(int slot)
    {
        var path = Path.Combine(SaveDirectory, $"save_{slot}.json");
        Directory.CreateDirectory(SaveDirectory);
        var json = JsonSerializer.Serialize(Slots[slot]);
        File.WriteAllText(path, SaveEncryption.Encrypt(json));
        SaveBackup.CreateBackup(path);
    }

    public SaveData? Load(int slot)
    {
        var path = Path.Combine(SaveDirectory, $"save_{slot}.json");
        if (!File.Exists(path)) return null;
        var encrypted = File.ReadAllText(path);
        var json = SaveEncryption.Decrypt(encrypted);
        var data = JsonSerializer.Deserialize<SaveData>(json);
        if (data == null || !SaveValidation.ValidateSaveData(data)) return null;
        Slots[slot] = data;
        return data;
    }

    public void Delete(int slot)
    {
        var path = Path.Combine(SaveDirectory, $"save_{slot}.json");
        if (File.Exists(path)) File.Delete(path);
    }

    public bool Exists(int slot) =>
        File.Exists(Path.Combine(SaveDirectory, $"save_{slot}.json"));
}
