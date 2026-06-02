namespace MarioEngine.Core.Input;

using Silk.NET.Input;

internal sealed partial class InputManager : IDisposable
{
    public static InputManager Instance => _instance.Value;
    private static readonly Lazy<InputManager> _instance = new(() => new InputManager());

    private IInputContext? _inputContext;
    private readonly List<InputDevice> _devices = new();
    private readonly Dictionary<string, InputAction> _actions = new();

    private InputManager() { }

    public IReadOnlyList<InputDevice> Devices => _devices.AsReadOnly();

    public void Initialize(IInputContext context)
    {
        _inputContext = context;
        foreach (var kb in context.Keyboards)
            _devices.Add(new Devices.KeyboardDevice { /* handled by keyboard */ });
        foreach (var gp in context.Gamepads)
            _devices.Add(new Devices.GamepadDevice { /* handled by gamepad */ });
        foreach (var ms in context.Mice)
            _devices.Add(new Devices.MouseDevice { /* handled by mouse */ });
    }

    public void Update()
    {
        foreach (var device in _devices)
            device.Update();
    }

    public void Dispose()
    {
        if (_inputContext is not null)
        {
            _inputContext.ConnectionChanged -= OnConnectionChanged;
            _inputContext.Dispose();
            _inputContext = null;
        }
        _devices.Clear();
        _actions.Clear();
    }

    public InputAction? GetAction(string name)
    {
        return _actions.TryGetValue(name, out var action) ? action : null;
    }

    public void RegisterAction(string name, InputAction action)
    {
        _actions[name] = action;
    }

    private void OnConnectionChanged(IInputDevice device, bool connected)
    {
        if (connected)
            HandleDeviceConnected(device);
        else
            HandleDeviceDisconnected(device);
    }

    private void HandleDeviceConnected(IInputDevice device)
    {
        switch (device)
        {
            case IKeyboard keyboard:
                var kb = new Devices.KeyboardDevice();
                kb.Bind(keyboard);
                _devices.Add(kb);
                break;
            case IGamepad gamepad:
                var gp = new Devices.GamepadDevice();
                gp.Bind(gamepad);
                _devices.Add(gp);
                break;
            case IMouse mouse:
                var ms = new Devices.MouseDevice();
                ms.Bind(mouse);
                _devices.Add(ms);
                break;
        }
    }

    private void HandleDeviceDisconnected(IInputDevice device)
    {
        _devices.RemoveAll(d =>
            d is Devices.KeyboardDevice && device is IKeyboard ||
            d is Devices.GamepadDevice && device is IGamepad ||
            d is Devices.MouseDevice && device is IMouse);
    }
}
