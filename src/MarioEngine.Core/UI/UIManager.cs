namespace MarioEngine.Core.UI;

using System.Collections.Generic;

/// <summary>Manages UI state with stack-based navigation.</summary>
public sealed class UIManager
{
    /// <summary>UI states.</summary>
    public enum UIState
    {
        /// <summary>No UI visible.</summary>
        None,

        /// <summary>Main menu.</summary>
        MainMenu,

        /// <summary>Pause menu.</summary>
        PauseMenu,

        /// <summary>Settings menu.</summary>
        Settings,

        /// <summary>Heads-up display.</summary>
        HUD,

        /// <summary>Inventory screen.</summary>
        Inventory,

        /// <summary>World map.</summary>
        WorldMap,

        /// <summary>Level complete screen.</summary>
        LevelComplete,

        /// <summary>Game over screen.</summary>
        GameOver,

        /// <summary>Cutscene dialog.</summary>
        Cutscene,
    }

    /// <summary>Current UI state.</summary>
    public UIState CurrentState { get; private set; } = UIState.None;

    private readonly Stack<UIState> _stateStack = new();

    /// <summary>Shows a new UI state, pushing onto the stack.</summary>
    public void Show(UIState state)
    {
        _stateStack.Push(state);
        CurrentState = state;
    }

    /// <summary>Hides the current UI state, popping the stack.</summary>
    public void Hide()
    {
        if (_stateStack.Count > 0)
            _stateStack.Pop();

        CurrentState = _stateStack.Count > 0 ? _stateStack.Peek() : UIState.None;
    }
}
