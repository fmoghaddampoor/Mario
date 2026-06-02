namespace MarioEngine.Core.GamePlayer;

public sealed class PlayerPowerUpManager
{
    public enum PowerUp
    {
        None,
        Mushroom,
        Fire,
        Star,
        Penguin,
        Bee,
        Boo,
        Propeller,
        Metal,
        Cat,
        Builder
    }

    public PowerUp CurrentPowerUp { get; set; } = PowerUp.None;
    public float StarTimer { get; set; }

    public event Action<PowerUp, PowerUp>? OnPowerUpChanged;

    public bool HasFirePower => CurrentPowerUp == PowerUp.Fire;
    public bool IsStarInvincible => CurrentPowerUp == PowerUp.Star;

    public void ApplyPowerUp(PowerUp powerUp)
    {
        var previous = CurrentPowerUp;
        CurrentPowerUp = powerUp;
        OnPowerUpChanged?.Invoke(previous, powerUp);
    }

    public void RemovePowerUp()
    {
        ApplyPowerUp(PowerUp.None);
    }
}
