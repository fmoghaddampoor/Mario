namespace MarioEngine.Core.Graphics.Buffer;

/// <summary>Applies gamma and sRGB correction to pixel data.</summary>
internal static class GammaCorrection
{
    public static byte[] ApplyGamma(byte[] pixels, float gamma)
    {
        var result = new byte[pixels.Length];
        var inv = 1.0f / gamma;
        for (int i = 0; i < pixels.Length; i++)
            result[i] = (byte)(MathF.Pow(pixels[i] / 255f, inv) * 255f);
        return result;
    }

    public static byte[] ApplySRGB(byte[] pixels)
    {
        var result = new byte[pixels.Length];
        for (int i = 0; i < pixels.Length; i++)
        {
            var v = pixels[i] / 255f;
            result[i] = (byte)((v <= 0.04045f
                ? v / 12.92f
                : MathF.Pow((v + 0.055f) / 1.055f, 2.4f)) * 255f);
        }
        return result;
    }
}
