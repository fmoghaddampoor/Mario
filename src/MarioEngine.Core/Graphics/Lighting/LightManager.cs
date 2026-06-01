namespace MarioEngine.Core.Graphics.Lighting;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

/// <summary>
/// Manages up to 16 dynamic point lights, renders them with additive blending,
/// and handles shadow sprites. Provides ambient light per level.
/// </summary>
public sealed class LightManager
{
    /// <summary>Maximum number of dynamic lights allowed at once.</summary>
    private const int MaxLights = 16;

    /// <summary>Logger for light limit warnings.</summary>
    private readonly ILogger<LightManager> _logger;

    /// <summary>Active dynamic lights list.</summary>
    private readonly List<Light2D> _lights;

    /// <summary>Ambient light intensity (0-1).</summary>
    private float _ambientIntensity = 0.3f;

    /// <summary>Ambient light color as RGBA uint.</summary>
    private uint _ambientColor = 0x202040FF;

    /// <summary>
    /// Initializes a new instance of the <see cref="LightManager"/> class.
    /// </summary>
    /// <param name="logger">Logger instance.</param>
    /// <exception cref="ArgumentNullException">Thrown if logger is null.</exception>
    public LightManager(ILogger<LightManager> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _lights = new List<Light2D>(MaxLights);
    }

    /// <summary>Gets or sets the ambient light intensity (0-1).</summary>
    public float AmbientIntensity
    {
        get => _ambientIntensity;
        set => _ambientIntensity = Math.Clamp(value, 0f, 1f);
    }

    /// <summary>Gets or sets the ambient light color as RGBA uint.</summary>
    public uint AmbientColor
    {
        get => _ambientColor;
        set => _ambientColor = value;
    }

    /// <summary>Gets the number of currently active lights.</summary>
    public int ActiveLightCount => _lights.Count;

    /// <summary>Gets the read-only list of active lights.</summary>
    public IReadOnlyList<Light2D> Lights => _lights;

    /// <summary>Gets the number of lights rendered this frame.</summary>
    public int FrameLightCount => _lights.Count;

    /// <summary>Gets a debug string with lighting statistics.</summary>
    public string DebugOverlay => $"Lights: {ActiveLightCount}/{MaxLights} | Ambient: {_ambientIntensity:F2}";

    /// <summary>
    /// Adds a dynamic light. Returns false if the limit of 16 is reached.
    /// </summary>
    /// <param name="light">The light to add.</param>
    /// <returns>True if the light was added.</returns>
    /// <exception cref="ArgumentNullException">Thrown if light is null.</exception>
    public bool AddLight(Light2D light)
    {
        ArgumentNullException.ThrowIfNull(light);
        if (_lights.Count >= MaxLights)
        {
            _logger.LogWarning("Light limit reached ({Max}), light not added", MaxLights);
            return false;
        }

        _lights.Add(light);
        return true;
    }

    /// <summary>
    /// Removes a light from the manager.
    /// </summary>
    /// <param name="light">The light to remove.</param>
    public void RemoveLight(Light2D light)
    {
        _lights.Remove(light);
    }

    /// <summary>Removes all lights.</summary>
    public void Clear()
    {
        _lights.Clear();
    }

    /// <summary>
    /// Updates all lights (flicker animation). Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        for (var i = 0; i < _lights.Count; i++)
        {
            _lights[i].Update(dt);
        }
    }

#pragma warning disable SA1204 // Static members before non-static — utility method kept at end for readability
    /// <summary>
    /// Creates a shadow sprite rectangle for an entity.
    /// </summary>
    /// <param name="entityX">Entity X position in pixels.</param>
    /// <param name="entityY">Entity Y position in pixels.</param>
    /// <param name="entityWidth">Entity width in pixels.</param>
    /// <returns>Tuple of (x, y, width, height) for the shadow rectangle.</returns>
    public static (float X, float Y, float Width, float Height) GetShadowRect(float entityX, float entityY, float entityWidth)
    {
        var shadowW = entityWidth * 0.8f;
        var shadowH = entityWidth * 0.15f;
        var shadowX = entityX - (shadowW * 0.5f);
        var shadowY = entityY - (shadowH * 0.5f);
        return (shadowX, shadowY, shadowW, shadowH);
    }
#pragma warning restore SA1204
}
