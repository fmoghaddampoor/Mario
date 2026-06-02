namespace MarioEngine.Core.Save;

/// <summary>Represents a complete game save state.</summary>
internal sealed class SaveData
{
    public string PlayerName { get; set; } = "Mario";
    public int Lives { get; set; } = 3;
    public int Coins { get; set; }
    public int Score { get; set; }
    public int World { get; set; } = 1;
    public int Level { get; set; } = 1;
    public Vector2 Position { get; set; }
    public float PlayTime { get; set; }
    public string Timestamp { get; set; } = "";
    public bool[] StarCoins { get; set; } = new bool[100];
    public Dictionary<string, bool> Flags { get; set; } = new();
}

internal struct Vector2
{
    public float X { get; set; }
    public float Y { get; set; }
}
