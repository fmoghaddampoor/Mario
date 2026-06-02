namespace MarioEngine.Core.Physics;

/// <summary>
/// Coyote time timer: grants a short grace period after leaving a ledge
/// where the player can still jump. This prevents the &quot;ran off the edge
/// and can't jump&quot; frustration.
/// </summary>
internal sealed class CoyoteTimer
{
    /// <summary>Maximum coyote time duration in seconds (100 ms).</summary>
    private const float MaxCoyoteTime = 0.1f;

    /// <summary>Elapsed time since leaving the ground.</summary>
    private float _elapsed;

    /// <summary>True while the player is grounded.</summary>
    private bool _grounded;

    /// <summary>Gets a value indicating whether the player can still jump (coyote time active).</summary>
    internal bool CanJump => !_grounded && _elapsed < MaxCoyoteTime;

    /// <summary>
    /// Updates the coyote timer state.
    /// </summary>
    /// <param name="dt">Delta time in seconds.</param>
    /// <param name="isGrounded">Whether the player is currently grounded.</param>
    internal void Update(float dt, bool isGrounded)
    {
        if (isGrounded)
        {
            _grounded = true;
            _elapsed = 0f;
        }
        else
        {
            if (_grounded)
            {
                _grounded = false;
                _elapsed = 0f;
            }

            _elapsed += dt;
        }
    }

    /// <summary>Resets the timer (called after a successful coyote jump).</summary>
    internal void Consume()
    {
        _elapsed = MaxCoyoteTime;
    }
}
