namespace MarioEngine.Core.Scene;

/// <summary>
/// Manages a stack of <see cref="Scene"/> instances. Only the top-most scene receives updates.
/// </summary>
internal sealed class SceneManager
{
    private readonly Stack<Scene> _sceneStack = new();

    /// <summary>Returns the currently active scene, or <c>null</c> if the stack is empty.</summary>
    public Scene? CurrentScene => _sceneStack.Count > 0 ? _sceneStack.Peek() : null;

    /// <summary>Number of scenes currently on the stack.</summary>
    public int SceneCount => _sceneStack.Count;

    /// <summary>
    /// Pushes <paramref name="scene"/> onto the top of the stack.
    /// </summary>
    public void PushScene(Scene scene)
    {
        _sceneStack.Push(scene);
    }

    /// <summary>
    /// Removes and destroys the top-most scene. Does nothing if the stack is empty.
    /// </summary>
    public void PopScene()
    {
        if (_sceneStack.Count > 0)
        {
            var scene = _sceneStack.Pop();
            scene.Clear();
        }
    }

    /// <summary>
    /// Replaces the top-most scene with <paramref name="scene"/>.
    /// The previous top is cleared before removal.
    /// </summary>
    public void ReplaceTop(Scene scene)
    {
        if (_sceneStack.Count > 0)
        {
            var old = _sceneStack.Pop();
            old.Clear();
        }

        _sceneStack.Push(scene);
    }

    /// <summary>
    /// Updates the current scene (top of stack) with the given delta time.
    /// </summary>
    public void Update(float dt)
    {
        CurrentScene?.Update(dt);
    }
}
