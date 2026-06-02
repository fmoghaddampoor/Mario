namespace MarioEngine.Core.Core.Scene;

/// <summary>Manages scene-level state transitions.</summary>
internal enum SceneState { Menu, Gameplay, Cutscene, Paused, Loading }

internal sealed class SceneStateMachine
{
    public SceneState CurrentState { get; private set; } = SceneState.Menu;
    public event Action<SceneState, SceneState>? OnStateChanged;

    public void TransitionTo(SceneState state)
    {
        var previous = CurrentState;
        CurrentState = state;
        OnStateChanged?.Invoke(previous, state);
    }
}
