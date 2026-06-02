namespace MarioEngine.Core.Input.Devices;

using System;
using Silk.NET.Input;

/// <summary>
/// Wraps a Silk.NET IGamepad and exposes button state per frame.
/// Uses IGamepad.Buttons collection to read state.
/// </summary>
internal sealed class GamepadDevice : InputDevice
{
    private IGamepad? _gamepad;

    public GamepadDevice() { Name = "Gamepad"; Type = DeviceType.Gamepad; }

    public void Bind(IGamepad gp) => _gamepad = gp;

    public override bool IsConnected => _gamepad?.IsConnected ?? false;

    public override void Update()
    {
        if (_gamepad is null) return;

        // Buttons are updated by Silk.NET's internal polling;
        // gamepad state is read directly via _gamepad.GetButton or buttons[index].IsPressed
    }
}
