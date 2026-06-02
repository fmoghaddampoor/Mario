namespace MarioEngine.Core.Audio;

using MarioEngine.Core.Audio.Sfx;
using Silk.NET.OpenAL;

/// <summary>
/// Contains public API methods and internal processing for the <see cref="AudioManager"/> class.
/// </summary>
public sealed partial class AudioManager
{
    /// <summary>
    /// Called every frame. Refills streaming music buffers, reclaims SFX sources,
    /// processes delayed SFX, and updates profiler metrics.
    /// </summary>
    public void Update()
    {
        _music?.Update();
        _sfxPool?.Update();
        ProcessDelayedSfx();
        _profiler.UpdatePeak(_sfxPool?.ActiveCount ?? 0);
    }

    /// <summary>
    /// Sets the master volume on both the bus system and OpenAL listener.
    /// </summary>
    /// <param name="volume">Volume level from 0.0 (silent) to 1.0 (full).</param>
    public void SetMasterVolume(float volume)
    {
        if (_busSystem != null)
        {
            _busSystem.MasterVolume = volume;
        }

        if (_initialized && _al != null)
        {
            _al.SetListenerProperty(ListenerFloat.Gain, volume);
        }
    }

    /// <summary>Pauses music playback. SFX are individually managed and unaffected.</summary>
    public void PauseAll()
    {
        _music?.Pause();
    }

    /// <summary>Resumes music playback.</summary>
    public void ResumeAll()
    {
        _music?.Resume();
    }

    /// <summary>
    /// Registers an audio cue for playback by name.
    /// </summary>
    /// <param name="cue">The audio cue to register. Must not be null.</param>
    /// <exception cref="System.ArgumentNullException">Thrown if cue is null.</exception>
    public void RegisterCue(AudioCue cue)
    {
        ArgumentNullException.ThrowIfNull(cue);
        _cueLibrary[cue.Name] = cue;
    }

    /// <summary>
    /// Plays a sound effect by cue name.
    /// </summary>
    /// <param name="cueName">Name of the registered cue.</param>
    public void PlayCue(string cueName)
    {
        if (!_cueLibrary.TryGetValue(cueName, out var cue))
        {
            return;
        }

        if (_sfx == null || _sfxPool == null)
        {
            return;
        }

        var buffer = _sfx.Get(cue.SfxName);
        if (buffer == null)
        {
            return;
        }

        var instance = _sfxPool.Play(buffer, cue.Priority, cue.Bus);
        if (instance == null)
        {
            _profiler.RecordSourceStolen();
        }
        else
        {
            _profiler.RecordSfxPlayed();
        }
    }

    /// <summary>
    /// Plays a sound effect after a delay.
    /// </summary>
    /// <param name="buffer">Sound buffer to play.</param>
    /// <param name="delaySeconds">Delay before playback in seconds.</param>
    /// <param name="priority">Priority level.</param>
    /// <param name="bus">Audio bus.</param>
    public void PlayDelayed(SoundBuffer buffer, float delaySeconds, int priority = 0, AudioBus bus = AudioBus.Sfx)
    {
        _delayedSfx.Add((buffer, priority, bus, delaySeconds, 0f));
    }

    /// <summary>Processes delayed SFX, playing those whose delay has elapsed.</summary>
    private void ProcessDelayedSfx()
    {
        for (var i = _delayedSfx.Count - 1; i >= 0; i--)
        {
            var entry = _delayedSfx[i];
            var elapsed = entry.Elapsed + 0.016f;
            if (elapsed >= entry.Delay)
            {
                var instance = _sfxPool?.Play(entry.Buffer, entry.Priority, entry.Bus);
                if (instance != null)
                {
                    _profiler.RecordSfxPlayed();
                }

                _delayedSfx.RemoveAt(i);
            }
            else
            {
                _delayedSfx[i] = (entry.Buffer, entry.Priority, entry.Bus, entry.Delay, elapsed);
            }
        }
    }
}
