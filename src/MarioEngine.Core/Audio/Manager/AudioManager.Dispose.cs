namespace MarioEngine.Core.Audio;

using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="Dispose"/> method for the <see cref="AudioManager"/> class.
/// Releases OpenAL device, context, and SFX library resources.
/// </summary>
public sealed partial class AudioManager
{
    /// <summary>Releases OpenAL device and context resources.</summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        _initialized = false;
        _music?.Dispose();
        _music = null;
        _sfx?.UnloadAll();
        _sfx = null;
        _al?.Dispose();
        _al = null;
        _context?.Dispose();
        _context = null;

        _logger.LogInformation(Resources.Strings.Audio_Shutdown);
    }
}
