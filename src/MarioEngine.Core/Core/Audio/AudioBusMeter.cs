namespace MarioEngine.Core.Core.Audio;

/// <summary>Visual level meter per audio bus.</summary>
internal sealed class AudioBusMeter
{
    public float[] LevelPerBus { get; } = new float[4];

    public void Update(AudioBusSystem buses, SfxPool pool)
    {
        for (int i = 0; i < LevelPerBus.Length && i < buses.BusCount; i++)
        {
            LevelPerBus[i] = buses.GetBusLevel(i);
        }
    }
}

/// <summary>Placeholder for audio bus system.</summary>
internal sealed class AudioBusSystem
{
    public int BusCount => 4;
    public float GetBusLevel(int index) => 0f;
}

/// <summary>Placeholder for SFX pool.</summary>
internal sealed class SfxPool { }
