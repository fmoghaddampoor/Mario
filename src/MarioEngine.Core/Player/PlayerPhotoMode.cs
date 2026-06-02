using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles photo mode state.</summary>
public sealed class PlayerPhotoMode
{
    /// <summary>Whether photo mode is currently active.</summary>
    public bool IsActive { get; private set; }

    /// <summary>Whether the player has selected a pose.</summary>
    public bool IsPosed { get; private set; }

    /// <summary>Fired when photo mode is toggled on or off.</summary>
    public event Action<bool>? OnPhotoModeToggled;

    /// <summary>Toggles photo mode on and off.</summary>
    public void Toggle()
    {
        IsActive = !IsActive;

        if (!IsActive)
            IsPosed = false;

        OnPhotoModeToggled?.Invoke(IsActive);
    }

    /// <summary>Sets the current pose by index.</summary>
    public void SetPose(int poseIndex)
    {
        if (!IsActive) return;
        IsPosed = true;
    }
}
