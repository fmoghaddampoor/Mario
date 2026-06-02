namespace MarioEngine.Core.Audio;

using MarioEngine.Core.Resources;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains unload methods for the <see cref="SfxLibrary"/> class.
/// </summary>
public sealed partial class SfxLibrary
{
    /// <summary>
    /// Removes a sound buffer from the cache and releases it.
    /// </summary>
    /// <param name="name">The sound name to unload.</param>
    public void Unload(string name)
    {
        if (_cache.TryGetValue(name, out var buffer))
        {
            _cache.Remove(name);
            buffer.Release();
        }
    }

    /// <summary>
    /// Unloads all sound buffers from the cache.
    /// </summary>
    public void UnloadAll()
    {
        foreach (var buffer in _cache.Values)
        {
            buffer.Release();
        }

        _cache.Clear();
        _logger.LogInformation(Resources.Strings.Sfx_UnloadedAll);
    }
}
