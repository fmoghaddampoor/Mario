namespace MarioEngine.Core.Graphics.Animation;

using System;

/// <summary>
/// Contains event management methods for <see cref="Animator"/>.
/// </summary>
public sealed partial class Animator
{
    /// <summary>
    /// Registers an event callback that fires when a specific frame index is reached.
    /// </summary>
    /// <param name="frameIndex">Frame index (0-based) when the event fires.</param>
    /// <param name="callback">Action to invoke when reaching that frame.</param>
    public void AddEvent(int frameIndex, Action callback)
    {
        _events[frameIndex] = callback;
    }

    /// <summary>Removes all registered frame events.</summary>
    public void ClearEvents()
    {
        _events.Clear();
    }
}
