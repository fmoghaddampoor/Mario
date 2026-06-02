namespace MarioEngine.Core.Save;

/// <summary>Validates save data integrity using checksums.</summary>
internal static class SaveValidation
{
    public static bool ValidateSaveData(SaveData data)
    {
        if (data == null) return false;
        if (data.Lives < 0 || data.Lives > 99) return false;
        if (data.Coins < 0 || data.Coins > 999) return false;
        if (data.World < 1 || data.World > 16) return false;
        if (data.Level < 1 || data.Level > 4) return false;
        if (data.StarCoins == null || data.StarCoins.Length != 100) return false;
        if (data.PlayTime < 0) return false;
        return true;
    }

    public static List<string> GetCorruptFields(SaveData data)
    {
        var corrupt = new List<string>();
        if (data == null) { corrupt.Add("Null"); return corrupt; }
        if (data.Lives < 0 || data.Lives > 99) corrupt.Add(nameof(data.Lives));
        if (data.Coins < 0 || data.Coins > 999) corrupt.Add(nameof(data.Coins));
        if (data.World < 1 || data.World > 16) corrupt.Add(nameof(data.World));
        if (data.Level < 1 || data.Level > 4) corrupt.Add(nameof(data.Level));
        if (data.StarCoins == null || data.StarCoins.Length != 100) corrupt.Add(nameof(data.StarCoins));
        if (data.PlayTime < 0) corrupt.Add(nameof(data.PlayTime));
        return corrupt;
    }
}
