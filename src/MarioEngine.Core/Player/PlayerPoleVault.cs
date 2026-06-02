using System;
using System.Numerics;

namespace MarioEngine.Core.Player;

/// <summary>Handles player pole vault movement.</summary>
internal sealed class PlayerPoleVault
{
    /// <summary>Whether the player is currently vaulting.</summary>
    public bool IsVaulting { get; private set; }

    /// <summary>Vertical offset applied during vault.</summary>
    public float VaultHeight { get; set; } = -300f;

    /// <summary>Horizontal distance covered during vault.</summary>
    public float VaultDistance { get; set; } = 200f;

    /// <summary>Vault progress from 0 to 1.</summary>
    public float Progress { get; private set; }

    private Vector2 _start;
    private Vector2 _end;

    /// <summary>Begins vaulting toward the pole position.</summary>
    public void StartVault(Vector2 polePosition)
    {
        IsVaulting = true;
        Progress = 0f;
        _start = polePosition;
        _end = polePosition + new Vector2(VaultDistance, VaultHeight);
    }

    /// <summary>Returns the interpolated vault position at progress t (0-1).</summary>
    public Vector2 GetVaultPosition(Vector2 currentPos, float t)
    {
        Progress = Math.Clamp(t, 0f, 1f);
        return Vector2.Lerp(_start, _end, Progress);
    }
}
