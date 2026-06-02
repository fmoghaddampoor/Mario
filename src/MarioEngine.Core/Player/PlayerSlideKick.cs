using System;

namespace MarioEngine.Core.GamePlayer;

/// <summary>Handles slide kick attack.</summary>
public sealed class PlayerSlideKick
{
    /// <summary>Whether the player is currently kicking.</summary>
    public bool IsKicking { get; private set; }

    /// <summary>Duration of the kick.</summary>
    public float KickDuration { get; set; } = 0.2f;

    /// <summary>Range of the kick in world units.</summary>
    public float KickRange { get; set; } = 40f;

    private float _timer;

    /// <summary>Begins the slide kick.</summary>
    public void StartKick()
    {
        IsKicking = true;
        _timer = 0f;
    }

    /// <summary>Updates the kick timer; ends kick when duration elapses.</summary>
    public void Update(float dt)
    {
        if (!IsKicking) return;

        _timer += dt;
        if (_timer >= KickDuration)
        {
            IsKicking = false;
            _timer = 0f;
        }
    }
}
