namespace MarioEngine.Core.UI;

using System;

/// <summary>Interactive slider control.</summary>
internal sealed class UISlider
{
    /// <summary>Slider rectangle bounds.</summary>
    public Rect Bounds { get; set; }

    /// <summary>Minimum value.</summary>
    public float MinValue { get; set; } = 0f;

    /// <summary>Maximum value.</summary>
    public float MaxValue { get; set; } = 1f;

    /// <summary>Current value.</summary>
    public float CurrentValue { get; set; }

    /// <summary>Whether the slider is being dragged.</summary>
    public bool IsDragging { get; private set; }

    /// <summary>Raised when the value changes.</summary>
    public event Action<float>? OnValueChanged;
}
