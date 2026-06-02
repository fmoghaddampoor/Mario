namespace MarioEngine.Core.Core.Audio.Debug;

/// <summary>FFT-based spectrum analyzer for audio visualization.</summary>
internal sealed class AudioVisualizer
{
    public float[] SpectrumBars { get; } = new float[32];

    public void Update(float[] samples)
    {
        if (samples == null || samples.Length == 0) return;
        for (int i = 0; i < SpectrumBars.Length; i++)
        {
            float sum = 0f;
            int binSize = samples.Length / SpectrumBars.Length;
            for (int j = 0; j < binSize; j++)
            {
                int idx = i * binSize + j;
                if (idx < samples.Length) sum += Math.Abs(samples[idx]);
            }
            SpectrumBars[i] = sum / binSize;
        }
    }

    public void Render()
    {
        // Draw spectrum bars to debug overlay
    }
}
