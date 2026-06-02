namespace MarioEngine.Core.Physics.Player;

/// <summary>
/// Manages the ledge grab state for the player.
/// Tracks grab duration and handles entering/leaving the grab state.
/// </summary>
internal sealed class LedgeGrab
{
    /// <summary>Gets or sets a value indicating whether the player is currently grabbing a ledge.</summary>
    internal bool IsGrabbing { get; set; }

    /// <summary>Gets or sets the duration in seconds the player has been grabbing.</summary>
    internal float GrabDuration { get; set; }

    /// <summary>
    /// Enters the ledge grab state. Resets the grab timer.
    /// </summary>
    internal void Grab()
    {
        IsGrabbing = true;
        GrabDuration = 0f;
    }

    /// <summary>
    /// Exits the ledge grab state.
    /// </summary>
    internal void Release()
    {
        IsGrabbing = false;
        GrabDuration = 0f;
    }

    /// <summary>
    /// Updates the grab timer while the player is holding onto a ledge.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    internal void Update(float dt)
    {
        if (IsGrabbing)
        {
            GrabDuration += dt;
        }
    }
}
