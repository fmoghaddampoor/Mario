namespace MarioEngine.Core.UI;

/// <summary>
/// Simple 2D rectangle with position and size.
/// </summary>
internal struct Rect
{
    internal float X, Y, Width, Height;

    internal Rect(float x, float y, float w, float h)
    {
        X = x; Y = y; Width = w; Height = h;
    }
}
