namespace MarioEngine.Core.UI;

/// <summary>Full-screen white flash effect for damage or star collection.</summary>
internal sealed class ScreenFlash
{
    /// <summary>Flash duration in seconds.</summary>
    public float FlashDuration { get; set; } = 0.1f;

    /// <summary>Current flash timer.</summary>
    public float Timer { get; private set; }

    /// <summary>Flash color as uint RGBA.</summary>
    public uint Color { get; set; } = 0xFFFFFFFF;

    /// <summary>Whether the flash is active.</summary>
    public bool IsFlashing => Timer > 0f;

    /// <summary>Triggers the flash effect.</summary>
    public void Trigger()
    {
        Timer = FlashDuration;
    }

    /// <summary>Updates the flash timer each frame.</summary>
    public void Update(float dt)
    {
        if (Timer > 0f)
            Timer -= dt;
    }
}
