using System;
using System.Collections.Generic;
using System.Linq;

namespace MarioEngine.Core.PowerUps;

public sealed class PowerUpManager
{
    public List<PowerUpBase> ActivePowerUps { get; } = new();
    public PowerUpBase? CurrentPowerUp { get; set; }

    public void Update(float dt, Player player)
    {
        for (int i = ActivePowerUps.Count - 1; i >= 0; i--)
        {
            var powerUp = ActivePowerUps[i];
            if (powerUp.Duration > 0f)
            {
                powerUp.Duration -= dt;
                if (powerUp.Duration <= 0f)
                {
                    powerUp.OnExpire();
                    ActivePowerUps.RemoveAt(i);
                }
            }
        }

        CurrentPowerUp = ActivePowerUps.LastOrDefault();
    }

    public void ClearPowerUps()
    {
        foreach (var powerUp in ActivePowerUps)
        {
            powerUp.OnExpire();
        }

        ActivePowerUps.Clear();
        CurrentPowerUp = null;
    }
}
