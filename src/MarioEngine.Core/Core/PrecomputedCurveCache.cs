namespace MarioEngine.Core.Core;

/// <summary>Caches pre-computed bezier and easing curves.</summary>
internal sealed class PrecomputedCurveCache
{
    private readonly Dictionary<string, float[]> _curves = new();

    public float[] GetCurve(string name, int samples)
    {
        if (_curves.TryGetValue(name, out var curve)) return curve;
        var data = new float[samples];
        for (int i = 0; i < samples; i++)
        {
            float t = i / (float)(samples - 1);
            data[i] = t * t * (3f - 2f * t); // smoothstep default
        }
        _curves[name] = data;
        return data;
    }
}
