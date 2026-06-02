namespace MarioEngine.Core.Graphics.Buffer;

/// <summary>Converts HDR pixel data to LDR using various tone mapping operators.</summary>
internal static class ToneMapping
{
    public static byte[] ApplyReinhard(byte[] hdrData)
    {
        var result = new byte[hdrData.Length];
        for (int i = 0; i < hdrData.Length; i++)
        {
            var v = hdrData[i] / 255f;
            v = v / (1f + v);
            result[i] = (byte)(v * 255f);
        }
        return result;
    }

    public static byte[] ApplyACES(byte[] hdrData)
    {
        var result = new byte[hdrData.Length];
        for (int i = 0; i < hdrData.Length; i++)
        {
            var v = hdrData[i] / 255f;
            var a = 2.51f * v + 0.03f;
            var b = 4.59f * v + 0.45f;
            v = MathF.Min(1f, a / b);
            result[i] = (byte)(v * 255f);
        }
        return result;
    }
}
