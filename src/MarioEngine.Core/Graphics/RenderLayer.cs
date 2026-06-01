namespace MarioEngine.Core.Graphics;

/// <summary>
/// Defines the render order for all visual elements.
/// Lower values render first (behind), higher values render later (on top).
/// </summary>
public enum RenderLayer
{
    /// <summary>Skybox, far background gradients.</summary>
    Background = 0,

    /// <summary>Slow-moving distant parallax layer (0.1x scroll speed).</summary>
    FarParallax = 1,

    /// <summary>Mid-distance parallax elements (0.3x scroll speed).</summary>
    MidParallax = 2,

    /// <summary>Near parallax elements close to gameplay (0.6x scroll speed).</summary>
    NearParallax = 3,

    /// <summary>Solid ground tiles, platforms, terrain.</summary>
    Ground = 4,

    /// <summary>Interactive game entities: player, enemies, items. Y-sorted within layer.</summary>
    Entities = 5,

    /// <summary>Foreground elements like grass, vines, overlays.</summary>
    Foreground = 6,

    /// <summary>Particle effects (dust, fire, smoke, sparks).</summary>
    Particles = 7,

    /// <summary>UI elements, HUD, menus (screen space, not affected by camera).</summary>
    UI = 8,
}
