namespace MarioEngine.Core.Input.Devices;

using Silk.NET.Input;
using System.Numerics;

internal sealed class MouseDevice : InputDevice
{
    private IMouse? _mouse;
    private Vector2 _previousPosition;

    public MouseDevice()
    {
        Name = "Mouse";
        Type = DeviceType.Mouse;
    }

    public void Bind(IMouse mouse)
    {
        _mouse = mouse;
        _previousPosition = mouse.Position;
    }

    public override bool IsConnected => _mouse?.IsConnected ?? false;

    public Vector2 Position { get; private set; }
    public Vector2 Delta { get; private set; }
    public float ScrollDelta { get; private set; }

    public override void Update()
    {
        if (_mouse is null) return;

        Position = _mouse.Position;
        Delta = Position - _previousPosition;
        ScrollDelta = _mouse.ScrollWheels.Sum(w => w.Y);
        _previousPosition = Position;
    }

    public bool IsButtonDown(MouseButton button)
    {
        return _mouse?.IsButtonPressed(button) ?? false;
    }
}
