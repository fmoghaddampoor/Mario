namespace MarioEngine.Core.Input.Mapping;

using Silk.NET.Input;

internal sealed class InputActionMap
{
    public string Name { get; }
    public Dictionary<string, InputAction> Actions { get; } = new();

    public InputActionMap(string name)
    {
        Name = name;
    }

    public void RegisterAction(InputAction action)
    {
        Actions[action.Name] = action;
    }

    public InputAction? GetAction(string name)
    {
        return Actions.TryGetValue(name, out var action) ? action : null;
    }

    public void Update(bool[,] keyStates)
    {
        foreach (var kvp in Actions)
        {
            bool isDown = false;
            foreach (var key in kvp.Value.Keys)
            {
                int keyIndex = (int)key;
                if (keyIndex < keyStates.GetLength(0) && keyStates[keyIndex, 0])
                {
                    isDown = true;
                    break;
                }
            }
            kvp.Value.Update(isDown);
        }
    }
}
