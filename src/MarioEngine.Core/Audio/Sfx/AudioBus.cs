namespace MarioEngine.Core.Audio.Sfx;

/// <summary>
/// Identifies which audio bus a sound belongs to.
/// Each bus has its own volume multiplier in <see cref="AudioBusSystem"/>.
/// </summary>
public enum AudioBus
{
    /// <summary>Master bus — affects all sound output.</summary>
    Master = 0,

    /// <summary>Music bus — background tracks and stems.</summary>
    Music = 1,

    /// <summary>SFX bus — sound effects (player, enemies, environment).</summary>
    Sfx = 2,

    /// <summary>Voice bus — reserved for future use (dialogue, narration).</summary>
    Voice = 3,
}
