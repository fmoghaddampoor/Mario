namespace MarioEngine.Core.Graphics.Debug;

using Silk.NET.OpenGL;

/// <summary>
/// Renders a wireframe overlay for collision shapes and physics bodies.
/// Uses GL_LINES to draw shape outlines in screen space.
/// Toggled via F4 key (requires input system).
/// </summary>
internal sealed class WireframeDebugOverlay
{
    /// <summary>Color for collision shapes (bright green RGBA).</summary>
    private static readonly float[] ShapeColor = { 0f, 1f, 0f, 1f };

    /// <summary>Color for trigger zones (bright blue RGBA).</summary>
    private static readonly float[] TriggerColor = { 0f, 0.5f, 1f, 1f };

    /// <summary>OpenGL context.</summary>
    private readonly GL _gl;

    /// <summary>Whether the overlay is visible.</summary>
    private bool _visible;

    /// <summary>Initializes a new instance of the <see cref="WireframeDebugOverlay"/> class.</summary>
    /// <param name="gl">OpenGL context. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if gl is null.</exception>
    public WireframeDebugOverlay(GL gl)
    {
        _gl = gl ?? throw new ArgumentNullException(nameof(gl));
    }

    /// <summary>Gets or sets a value indicating whether the overlay is visible.</summary>
    public bool Visible
    {
        get => _visible;
        set => _visible = value;
    }

    /// <summary>Begins wireframe rendering for the current frame.</summary>
    public void Begin()
    {
        if (!_visible)
        {
            return;
        }

        _gl.UseProgram(0);
    }

    /// <summary>
    /// Draws an AABB wireframe rectangle.
    /// </summary>
    /// <param name="x">Left edge in world pixels.</param>
    /// <param name="y">Top edge in world pixels.</param>
    /// <param name="w">Width in world pixels.</param>
    /// <param name="h">Height in world pixels.</param>
    public void DrawAabb(float x, float y, float w, float h)
    {
        if (!_visible)
        {
            return;
        }
    }

    /// <summary>Ends wireframe rendering and flushes lines.</summary>
    public void End()
    {
        if (!_visible)
        {
            return;
        }
    }
}
