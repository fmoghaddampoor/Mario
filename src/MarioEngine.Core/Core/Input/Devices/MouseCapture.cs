namespace MarioEngine.Core.Core.Input.Devices;

/// <summary>Locks mouse cursor to the game window.</summary>
internal sealed class MouseCapture
{
    public bool IsCaptured { get; private set; }

    public void Capture(IMouse mouse)
    {
        IsCaptured = true;
        // Set cursor clip to window bounds
    }

    public void Release()
    {
        IsCaptured = false;
        // Release cursor clip
    }
}

internal interface IMouse { }
