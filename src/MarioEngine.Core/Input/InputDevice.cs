namespace MarioEngine.Core.Input;

internal enum DeviceType
{
    Keyboard,
    Gamepad,
    Mouse,
    Touch
}

internal abstract class InputDevice
{
    public string Name { get; protected set; } = string.Empty;
    public DeviceType Type { get; protected set; }
    public abstract bool IsConnected { get; }
    public abstract void Update();
}
