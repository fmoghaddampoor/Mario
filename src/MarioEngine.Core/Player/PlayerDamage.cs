namespace MarioEngine.Core.Player;

internal sealed class PlayerDamage
{
    public bool IsInvincible { get; private set; }
    public float InvincibilityDuration { get; set; } = 2f;
    public float InvincibilityTimer { get; private set; }
    public bool IsDead { get; private set; }
    public int Health { get; set; } = 3;

    public event Action? OnDamaged;
    public event Action? OnDied;

    public void TakeDamage()
    {
        if (IsInvincible || IsDead) return;

        Health--;
        IsInvincible = true;
        InvincibilityTimer = InvincibilityDuration;
        OnDamaged?.Invoke();

        if (Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (IsDead) return;
        IsDead = true;
        OnDied?.Invoke();
    }

    public void Update(float dt)
    {
        if (IsInvincible)
        {
            InvincibilityTimer -= dt;
            if (InvincibilityTimer <= 0)
            {
                IsInvincible = false;
                InvincibilityTimer = 0;
            }
        }
    }
}
