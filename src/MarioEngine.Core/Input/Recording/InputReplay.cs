namespace MarioEngine.Core.Input.Recording;

using Silk.NET.Input;

internal sealed class InputReplay
{
    private List<(float Time, Key Key, bool Pressed)>? _recordedData;
    private int _currentIndex;
    private float _accumulator;

    public bool IsPlaying { get; private set; }
    public float PlaybackTime { get; private set; }
    public float Speed { get; set; } = 1f;

    public void Start(List<(float Time, Key Key, bool Pressed)> recordedData)
    {
        _recordedData = recordedData;
        _currentIndex = 0;
        _accumulator = 0f;
        PlaybackTime = 0f;
        IsPlaying = true;
    }

    public void Stop()
    {
        IsPlaying = false;
        _recordedData = null;
        _currentIndex = 0;
        _accumulator = 0f;
        PlaybackTime = 0f;
    }

    public void Update(float dt, Devices.KeyboardDevice targetKeyboard)
    {
        if (!IsPlaying || _recordedData is null) return;

        _accumulator += dt * Speed;

        while (_currentIndex < _recordedData.Count && _recordedData[_currentIndex].Time <= _accumulator)
        {
            var (_, key, pressed) = _recordedData[_currentIndex];
            if (pressed)
                targetKeyboard.GetType().GetMethod("OnKeyDown")?.Invoke(targetKeyboard, new object[] { key });
            else
                targetKeyboard.GetType().GetMethod("OnKeyUp")?.Invoke(targetKeyboard, new object[] { key });
            _currentIndex++;
        }

        PlaybackTime = _accumulator;

        if (_currentIndex >= _recordedData.Count)
            Stop();
    }
}
