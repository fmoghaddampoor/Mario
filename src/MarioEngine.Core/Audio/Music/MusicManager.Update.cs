namespace MarioEngine.Core.Audio.Music;

using System;

/// <summary>
/// Contains the update loop for the <see cref="MusicManager"/> class.
/// Called every frame to refill streaming buffers and manage transitions.
/// </summary>
public sealed partial class MusicManager
{
    /// <summary>
    /// Called every frame. Refills streaming buffers and updates crossfade/duck state.
    /// </summary>
    public void Update()
    {
        _current?.Update();

        if (_crossfadeDuration > 0f)
        {
            UpdateCrossfade();
        }

        if (_ducking)
        {
            UpdateDuck();
        }

        for (var i = 0; i < MaxStems; i++)
        {
            _stems[i]?.Update();
        }
    }

    /// <summary>Updates the crossfade transition each frame.</summary>
    private void UpdateCrossfade()
    {
        _crossfadeElapsed += 1f / 60f;

        var t = Math.Clamp(_crossfadeElapsed / _crossfadeDuration, 0f, 1f);

        if (_current != null)
        {
            _current.Volume = 1f - t;
        }

        if (_next != null)
        {
            _next.Volume = t;
        }

        if (t >= 1f)
        {
            _current?.Dispose();
            _current = _next;
            _next = null;
            _crossfadeDuration = 0f;
        }
    }

    /// <summary>Updates the duck effect each frame, restoring volume when done.</summary>
    private void UpdateDuck()
    {
        _duckElapsed += 1f / 60f;

        if (_duckElapsed >= _duckDuration)
        {
            if (_current != null)
            {
                _current.Volume = _duckOriginalVolume;
            }

            _ducking = false;
        }
    }
}
