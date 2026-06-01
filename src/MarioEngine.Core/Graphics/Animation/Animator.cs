namespace MarioEngine.Core.Graphics.Animation;

using System;
using System.Collections.Generic;

/// <summary>
/// Controls playback of animations. Supports play, stop, pause, resume,
/// crossfade, speed multiplier, and frame-based events.
/// </summary>
public sealed class Animator
{
    private readonly Dictionary<string, AnimationClip> _animations;
    private AnimationClip? _currentAnimation;
    private AnimationClip? _previousAnimation;
    private float _elapsed;
    private float _crossfadeTimer;
    private float _crossfadeDuration;
    private int _currentFrameIndex;
    private bool _playing;
    private bool _paused;
    private float _speed = 1f;
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

    /// <summary>
    /// Plays an animation by name. Optionally crossfades from the current animation.
    /// </summary>
    /// <param name="name">Animation name.</param>
    /// <param name="crossfadeDuration">Crossfade duration in seconds. 0 for instant switch.</param>
    public void Play(string name, float crossfadeDuration = 0.1f)
    {
        if (!_animations.TryGetValue(name, out var anim))
        {
            return;
        }

        if (_currentAnimation != null && crossfadeDuration > 0f)
        {
            _previousAnimation = _currentAnimation;
            _crossfadeTimer = 0f;
            _crossfadeDuration = crossfadeDuration;
        }
        else
        {
            _previousAnimation = null;
            _crossfadeTimer = _crossfadeDuration = 0f;
        }

        _currentAnimation = anim;
        _currentFrameIndex = 0;
        _elapsed = 0f;
        _playing = true;
        _paused = false;
        HasFinished = false;
    }

    /// <summary>Stops the current animation and resets to the first frame.</summary>
    public void Stop()
    {
        _playing = false;
        _paused = false;
        _elapsed = 0f;
        _currentFrameIndex = 0;
        HasFinished = true;
    }

    /// <summary>Pauses the current animation.</summary>
    public void Pause()
    {
        _paused = true;
    }

    /// <summary>Resumes the current animation from pause.</summary>
    public void Resume()
    {
        _paused = false;
    }

    /// <summary>
    /// Registers an event callback for a specific frame index.
    /// </summary>
    /// <param name="frameIndex">Frame index (0-based) when the event fires.</param>
    /// <param name="callback">Action to invoke when reaching that frame.</param>
    public void AddEvent(int frameIndex, Action callback)
    {
        _events[frameIndex] = callback;
    }

    /// <summary>Clears all registered events.</summary>
    public void ClearEvents()
    {
        _events.Clear();
    }

    /// <summary>
    /// Updates the animation state. Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!_playing || _paused || _currentAnimation == null)
        {
            return;
        }

        var dtScaled = dt * _speed;

        // Update crossfade
        if (_crossfadeTimer < _crossfadeDuration)
        {
            _crossfadeTimer += dt;
        }

        // Advance frame timer
        _elapsed += dtScaled;

        var frame = _currentAnimation.Frames[_currentFrameIndex];
        while (_elapsed >= frame.Duration)
        {
            _elapsed -= frame.Duration;
            _currentFrameIndex++;

            if (_currentFrameIndex >= _currentAnimation.Frames.Count)
            {
                if (_currentAnimation.Loop)
                {
                    _currentFrameIndex = 0;
                }
                else
                {
                    _currentFrameIndex = _currentAnimation.Frames.Count - 1;
                    _playing = false;
                    HasFinished = true;
                    return;
                }
            }

            // Fire frame event
            if (_events.TryGetValue(_currentFrameIndex, out var evt))
            {
                evt();
            }

            frame = _currentAnimation.Frames[_currentFrameIndex];
        }
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
