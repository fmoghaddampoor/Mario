namespace MarioEngine.Core.Config;

/// <summary>
/// Input configuration section.
/// </summary>
public sealed class InputConfig
{
    /// <summary>Gets or sets the horizontal look/aim sensitivity multiplier. Default 1.0.</summary>
    public float HorizontalSensitivity { get; set; } = 1.0f;

    /// <summary>Gets or sets the vertical look/aim sensitivity multiplier. Default 1.0.</summary>
    public float VerticalSensitivity { get; set; } = 1.0f;

    /// <summary>Gets or sets the controller stick dead zone (0.05 to 0.30). Default 0.15.</summary>
    public float DeadZone { get; set; } = 0.15f;

    /// <summary>Gets or sets a value indicating whether auto-run is enabled. Default false.</summary>
    public bool AutoRun { get; set; } = false;

    /// <summary>Gets or sets a value indicating whether controller vibration is enabled. Default true.</summary>
    public bool VibrationEnabled { get; set; } = true;
}
