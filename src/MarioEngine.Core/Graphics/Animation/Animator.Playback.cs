namespace MarioEngine.Core.Graphics.Animation;

/// <summary>
/// Contains Play, Stop, Pause, Resume, and Update methods for <see cref="Animator"/>.
/// </summary>
public sealed partial class Animator
{
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
    /// Updates the animation state. Advances frame timer and fires frame events.
    /// Call once per frame.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    public void Update(float dt)
    {
        if (!_playing || _paused || _currentAnimation == null)
        {
            return;
        }

        var dtScaled = dt * _speed;

        if (_crossfadeTimer < _crossfadeDuration)
        {
            _crossfadeTimer += dt;
        }

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

            if (_events.TryGetValue(_currentFrameIndex, out var evt))
            {
                evt();
            }

            frame = _currentAnimation.Frames[_currentFrameIndex];
        }
    }
}
