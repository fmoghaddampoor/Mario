using System;
using System.Numerics;

namespace MarioEngine.Core.PowerUps;

public sealed class PropellerPowerUp : PowerUpBase
{
    public float PropellerForce { get; set; } = -400f;

    public PropellerPowerUp()
    {
        Type = PowerUpType.Propeller;
    }

    public override void OnCollect(Player playerRef)
    {
        playerRef.CanPropellerSpin = true;
        playerRef.PropellerForce = PropellerForce;
    }
}
