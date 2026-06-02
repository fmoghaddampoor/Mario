namespace MarioEngine.Core.Graphics.Buffer;

/// <summary>Generates and applies color grading LUTs.</summary>
internal static class LUTGenerator
{
    public static byte[] GenerateIdentityLUT(int size)
    {
        var lut = new byte[size * size * size * 4];
        for (int b = 0; b < size; b++)
        for (int g = 0; g < size; g++)
        for (int r = 0; r < size; r++)
        {
            int idx = (b * size * size + g * size + r) * 4;
            lut[idx] = (byte)(r * 255 / (size - 1));
            lut[idx + 1] = (byte)(g * 255 / (size - 1));
            lut[idx + 2] = (byte)(b * 255 / (size - 1));
            lut[idx + 3] = 255;
        }
        return lut;
    }

    public static byte[] ApplyGrade(byte[] lut, float temperature, float tint, float saturation)
    {
        // Placeholder: modify lut values based on grading parameters
        return lut;
    }
}
