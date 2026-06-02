namespace MarioEngine.Core.Audio.Sfx;

using System;
using Silk.NET.OpenAL;

/// <summary>
/// Manages spatial audio properties for 3D positioning in a 2D world.
/// Each active <see cref="SfxInstance"/> can have a world position, pitch variation,
/// and occlusion factor applied to its OpenAL source.
/// </summary>
public sealed class SfxSpatial
{
    /// <summary>OpenAL API instance.</summary>
    private readonly AL _al;

    /// <summary>Width of the viewport in world units used for scale calculation.</summary>
    private float _worldWidth = 1920f;

    /// <summary>Height of the viewport in world units used for scale calculation.</summary>
    private float _worldHeight = 1080f;

    /// <summary>Listener world X position.</summary>
    private float _listenerX;

    /// <summary>Listener world Y position.</summary>
    private float _listenerY;

    /// <summary>Initializes a new instance of the <see cref="SfxSpatial"/> class.</summary>
    /// <param name="al">OpenAL API instance. Must not be null.</param>
    /// <exception cref="ArgumentNullException">Thrown if al is null.</exception>
    public SfxSpatial(AL al)
    {
        _al = al ?? throw new ArgumentNullException(nameof(al));
        _al.DistanceModel(DistanceModel.InverseDistanceClamped);
        _al.DopplerFactor(1f);
        _al.SpeedOfSound(343.3f);
    }

    /// <summary>
    /// Sets the world viewport dimensions for converting 2D coordinates to OpenAL space.
    /// </summary>
    /// <param name="worldWidth">Viewport width in world pixels.</param>
    /// <param name="worldHeight">Viewport height in world pixels.</param>
    public void SetWorldBounds(float worldWidth, float worldHeight)
    {
        _worldWidth = worldWidth;
        _worldHeight = worldHeight;
    }

    /// <summary>
    /// Updates the listener position in world coordinates.
    /// OpenAL uses a right-handed coordinate system with Y-up.
    /// We map world X to OpenAL X, world Y to OpenAL Z, and set OpenAL Y as up.
    /// </summary>
    /// <param name="x">Listener world X.</param>
    /// <param name="y">Listener world Y.</param>
    public void SetListenerPosition(float x, float y)
    {
        _listenerX = x;
        _listenerY = y;
        _al.SetListenerProperty(ListenerVector3.Position, x, 0f, y);

        unsafe
        {
            // Orientation: 6 floats = atX, atY, atZ, upX, upY, upZ
            var orientation = stackalloc float[6];
            orientation[0] = 0f; // forward X
            orientation[1] = 0f; // forward Y
            orientation[2] = -1f; // forward Z (looking into screen)
            orientation[3] = 0f; // up X
            orientation[4] = 1f; // up Y
            orientation[5] = 0f; // up Z
            _al.SetListenerProperty(ListenerFloatArray.Orientation, orientation);
        }
    }

    /// <summary>
    /// Applies spatial properties (position, pitch, occlusion) to a playing SFX instance.
    /// </summary>
    /// <param name="instance">The SFX instance to configure. Must not be null.</param>
    /// <param name="worldX">World X position of the sound source.</param>
    /// <param name="worldY">World Y position of the sound source.</param>
    /// <param name="pitchMin">Minimum pitch multiplier (0.5 to 2.0).</param>
    /// <param name="pitchMax">Maximum pitch multiplier (0.5 to 2.0).</param>
    /// <param name="occlusion">Occlusion factor (0.0 = none, 1.0 = fully occluded).</param>
    /// <exception cref="ArgumentNullException">Thrown if instance is null.</exception>
#pragma warning disable CA5394 // Random.Shared used for audio pitch variation, not security
    public void Apply(SfxInstance instance, float worldX, float worldY, float pitchMin = 1f, float pitchMax = 1f, float occlusion = 0f)
    {
        ArgumentNullException.ThrowIfNull(instance);
        var src = instance.Source;

        _al.SetSourceProperty(src, SourceVector3.Position, worldX, 0f, worldY);

        var pitch = pitchMin + ((float)Random.Shared.NextDouble() * (pitchMax - pitchMin));
        _al.SetSourceProperty(src, SourceFloat.Pitch, Math.Clamp(pitch, 0.5f, 2f));

        if (occlusion > 0f)
        {
            var occGain = Math.Clamp(1f - occlusion, 0f, 1f);
            float currentGain;
            unsafe
            {
                _al.GetSourceProperty(src, SourceFloat.Gain, &currentGain);
            }

            _al.SetSourceProperty(src, SourceFloat.Gain, occGain * currentGain);
        }

        _al.SetSourceProperty(src, SourceFloat.ReferenceDistance, 200f);
        _al.SetSourceProperty(src, SourceFloat.MaxDistance, 2000f);
        _al.SetSourceProperty(src, SourceFloat.RolloffFactor, 1f);
    }
#pragma warning restore CA5394

    /// <summary>
    /// Calculates the distance between the listener and a world position.
    /// </summary>
    /// <param name="worldX">World X of the sound source.</param>
    /// <param name="worldY">World Y of the sound source.</param>
    /// <returns>Distance in world units.</returns>
    public float DistanceToListener(float worldX, float worldY)
    {
        var dx = worldX - _listenerX;
        var dy = worldY - _listenerY;
        return MathF.Sqrt((dx * dx) + (dy * dy));
    }
}
