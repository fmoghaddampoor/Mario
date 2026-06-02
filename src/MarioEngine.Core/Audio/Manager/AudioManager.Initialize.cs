namespace MarioEngine.Core.Audio;

using System;
using MarioEngine.Core.Audio.Music;
using MarioEngine.Core.Audio.Sfx;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;
using Silk.NET.OpenAL;

/// <summary>
/// Contains the <see cref="Initialize"/> method for the <see cref="AudioManager"/> class.
/// Opens the default OpenAL device, creates a context, and falls back to silent mode on failure.
/// </summary>
public sealed partial class AudioManager
{
    /// <summary>
    /// Initializes the OpenAL audio device and context using the default device.
    /// Falls back to silent mode gracefully if OpenAL is unavailable.
    /// </summary>
    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        try
        {
            _al = AL.GetApi();
            _context = new AudioContext();
            _context.MakeCurrent();

            var vendor = _al.GetStateProperty(StateString.Vendor);
            var renderer = _al.GetStateProperty(StateString.Renderer);
            var version = _al.GetStateProperty(StateString.Version);

            _busSystem = new AudioBusSystem();
            _busSystem.MasterVolume = _config.MasterVolume;

            _al.SetListenerProperty(ListenerFloat.Gain, _config.MasterVolume);
            _al.SetListenerProperty(ListenerVector3.Position, 0f, 0f, 0f);

            _sfx = new SfxLibrary(_al, _logger);
            _sfxPool = new SfxPool(_al, _busSystem, _logger);
            _spatial = new SfxSpatial(_al);
            _music = new MusicManager(_al, _logger);
            _initialized = true;

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation(
                    Resources.Strings.Audio_Initialized,
                    vendor,
                    renderer,
                    version);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            _logger.LogWarning(ex, Resources.Strings.Audio_InitFailed);
            EnterSilentMode();
        }
    }

    /// <summary>Enters silent mode, cleaning up any partial OpenAL resources.</summary>
    private void EnterSilentMode()
    {
        _initialized = false;
        _music?.Dispose();
        _music = null;
        _spatial = null;
        _sfxPool?.Dispose();
        _sfxPool = null;
        _sfx?.UnloadAll();
        _sfx = null;
        _busSystem = null;
        _al?.Dispose();
        _al = null;
        _context?.Dispose();
        _context = null;
        _logger.LogInformation(Resources.Strings.Audio_SilentMode);
    }
}
