namespace MarioEngine.Core.Input.Mapping;

using Silk.NET.Input;

internal static class KeyConflictDetector
{
    public static bool HasConflict(InputAction action, IEnumerable<InputAction> allActions)
    {
        return FindConflict(action, allActions).HasValue;
    }

    public static Key? FindConflict(InputAction action, IEnumerable<InputAction> allActions)
    {
        foreach (var other in allActions)
        {
            if (other == action) continue;
            foreach (var key in action.Keys)
            {
                if (other.Keys.Contains(key))
                    return key;
            }
        }
        return null;
    }
}
