using System;

namespace MarioEngine.Core.Player;

/// <summary>Flags for which extended abilities are available to the player.</summary>
internal sealed class PlayerExtendedAbilities
{
    /// <summary>Whether the player can perform a spin jump.</summary>
    public bool CanSpinJump { get; set; }

    /// <summary>Whether the player can roll.</summary>
    public bool CanRoll { get; set; }

    /// <summary>Whether the player can backflip.</summary>
    public bool CanBackflip { get; set; }

    /// <summary>Whether the player can sideflip.</summary>
    public bool CanSideflip { get; set; }

    /// <summary>Whether the player can pole vault.</summary>
    public bool CanPoleVault { get; set; }

    /// <summary>Whether the player can hang glide.</summary>
    public bool CanHangGlide { get; set; }

    /// <summary>Whether the player can ground pound bounce.</summary>
    public bool CanGroundPoundBounce { get; set; }

    /// <summary>Whether the player can blow bubbles.</summary>
    public bool CanBubbleBlow { get; set; }

    /// <summary>Whether the player can shell surf.</summary>
    public bool CanShellSurf { get; set; }

    /// <summary>Whether the player can throw their cap.</summary>
    public bool CanCapThrow { get; set; }

    /// <summary>Whether the player can use a grappling hook.</summary>
    public bool CanGrapplingHook { get; set; }

    /// <summary>Whether the player can rewind time.</summary>
    public bool CanTimeRewind { get; set; }

    /// <summary>Unlocks every extended ability.</summary>
    public void UnlockAll()
    {
        CanSpinJump = true;
        CanRoll = true;
        CanBackflip = true;
        CanSideflip = true;
        CanPoleVault = true;
        CanHangGlide = true;
        CanGroundPoundBounce = true;
        CanBubbleBlow = true;
        CanShellSurf = true;
        CanCapThrow = true;
        CanGrapplingHook = true;
        CanTimeRewind = true;
    }

    /// <summary>Resets all abilities to locked.</summary>
    public void Reset()
    {
        CanSpinJump = false;
        CanRoll = false;
        CanBackflip = false;
        CanSideflip = false;
        CanPoleVault = false;
        CanHangGlide = false;
        CanGroundPoundBounce = false;
        CanBubbleBlow = false;
        CanShellSurf = false;
        CanCapThrow = false;
        CanGrapplingHook = false;
        CanTimeRewind = false;
    }
}
