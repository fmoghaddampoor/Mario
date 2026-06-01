namespace MarioEngine.Core.Graphics.Animation;

using System;
using System.Collections.Generic;

/// <summary>
/// Controls playback of animations. Supports play, stop, pause, resume,
/// crossfade, speed multiplier, and frame-based events.
/// </summary>
public sealed partial class Animator
{
    /// <summary>Registered animation clips keyed by name.</summary>
    private readonly Dictionary<string, AnimationClip> _animations;

    /// <summary>The currently playing animation clip.</summary>
    private AnimationClip? _currentAnimation;

    /// <summary>The previous animation clip used for crossfade blending.</summary>
    private AnimationClip? _previousAnimation;

    /// <summary>Elapsed time since the current frame started in seconds.</summary>
    private float _elapsed;

    /// <summary>Time since crossfade started in seconds.</summary>
    private float _crossfadeTimer;

    /// <summary>Total crossfade duration in seconds.</summary>
    private float _crossfadeDuration;

    /// <summary>Index of the current frame being displayed.</summary>
    private int _currentFrameIndex;

    /// <summary>True while an animation is actively playing.</summary>
    private bool _playing;

    /// <summary>True if playback is paused.</summary>
    private bool _paused;

    /// <summary>Playback speed multiplier. 1.0 = normal speed.</summary>
    private float _speed = 1f;

    /// <summary>Frame event callbacks keyed by frame index.</summary>
    private Dictionary<int, Action> _events;

    /// <summary>Initializes a new instance of the <see cref="Animator"/> class.</summary>
    public Animator()
    {
        _animations = new Dictionary<string, AnimationClip>(StringComparer.OrdinalIgnoreCase);
        _events = new Dictionary<int, Action>();
    }

    /// <summary>Gets the current animation name, or null if none is playing.</summary>
    public string? CurrentAnimationName => _currentAnimation?.Name;

    /// <summary>Gets the current frame index.</summary>
    public int CurrentFrameIndex => _currentFrameIndex;

    /// <summary>Gets or sets the playback speed multiplier. 1.0 = normal.</summary>
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    /// <summary>Gets a value indicating whether an animation is currently playing.</summary>
    public bool IsPlaying => _playing && !_paused;

    /// <summary>Gets a value indicating whether the current animation has finished (non-looping only).</summary>
    public bool HasFinished { get; private set; }

    /// <summary>Gets the current crossfade blend factor (0 = previous, 1 = current).</summary>
    public float CrossfadeBlend => _crossfadeDuration > 0f
        ? Math.Min(1f, _crossfadeTimer / _crossfadeDuration)
        : 1f;

    /// <summary>Gets the UV coordinates for the current frame.</summary>
    public AnimationFrame CurrentFrame
    {
        get
        {
            if (_currentAnimation == null || _currentAnimation.Frames.Count == 0)
            {
                return new AnimationFrame(0, 0, 1, 1, 0);
            }

            return _currentAnimation.Frames[_currentFrameIndex];
        }
    }

    /// <summary>Gets the UV coordinates for the previous frame (during crossfade).</summary>
    public AnimationFrame? PreviousFrame
    {
        get
        {
            if (_previousAnimation == null || _previousAnimation.Frames.Count == 0)
            {
                return null;
            }

            return _previousAnimation.Frames[_currentFrameIndex];
        }
    }

    /// <summary>
    /// Registers an animation clip.
    /// </summary>
    /// <param name="animation">The animation clip to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if animation is null.</exception>
    public void AddAnimation(AnimationClip animation)
    {
        ArgumentNullException.ThrowIfNull(animation);
        _animations[animation.Name] = animation;
    }

    /// <summary>Gets the UV coordinates for the current animation frame.</summary>
    /// <param name="u1">Output left UV.</param>
    /// <param name="v1">Output top UV.</param>
    /// <param name="u2">Output right UV.</param>
    /// <param name="v2">Output bottom UV.</param>
    public void GetUV(out float u1, out float v1, out float u2, out float v2)
    {
        var f = CurrentFrame;
        u1 = f.U1;
        v1 = f.V1;
        u2 = f.U2;
        v2 = f.V2;
    }
}
