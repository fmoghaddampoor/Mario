namespace MarioEngine.Core.Save;

/// <summary>Handles migration between save format versions.</summary>
internal static class SaveConverter
{
    public static SaveData ConvertFromV1(SaveDataV1 oldData)
    {
        return new SaveData
        {
            PlayerName = oldData.Name,
            Lives = oldData.Lives,
            Coins = oldData.Coins,
            Score = oldData.Score,
            World = oldData.World,
            Level = oldData.Level,
            Position = new Vector2 { X = oldData.PosX, Y = oldData.PosY },
            PlayTime = oldData.Time,
            Timestamp = DateTime.UtcNow.ToString("o"),
            StarCoins = new bool[100],
            Flags = new Dictionary<string, bool>()
        };
    }

    public static SaveData ConvertFromV2(SaveDataV2 oldData)
    {
        return new SaveData
        {
            PlayerName = oldData.PlayerName,
            Lives = oldData.Lives,
            Coins = oldData.Coins,
            Score = oldData.Score,
            World = oldData.World,
            Level = oldData.Level,
            Position = oldData.Position,
            PlayTime = oldData.PlayTime,
            Timestamp = oldData.Timestamp,
            StarCoins = oldData.StarCoins ?? new bool[100],
            Flags = oldData.Flags ?? new Dictionary<string, bool>()
        };
    }
}

internal sealed class SaveDataV1
{
    public string Name { get; set; } = "";
    public int Lives { get; set; }
    public int Coins { get; set; }
    public int Score { get; set; }
    public int World { get; set; }
    public int Level { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float Time { get; set; }
}

internal sealed class SaveDataV2
{
    public string PlayerName { get; set; } = "";
    public int Lives { get; set; }
    public int Coins { get; set; }
    public int Score { get; set; }
    public int World { get; set; }
    public int Level { get; set; }
    public Vector2 Position { get; set; }
    public float PlayTime { get; set; }
    public string Timestamp { get; set; } = "";
    public bool[]? StarCoins { get; set; }
    public Dictionary<string, bool>? Flags { get; set; }
}
