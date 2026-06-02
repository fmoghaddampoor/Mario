namespace MarioEngine.Core.Levels;

using System.Collections.Generic;

/// <summary>Validates level integrity before loading.</summary>
internal static class LevelValidator
{
    /// <summary>Returns true if the level passes all validation checks.</summary>
    public static bool ValidateLevel(Level level)
    {
        return GetErrors(level).Count == 0;
    }

    /// <summary>Returns a list of validation error messages.</summary>
    public static List<string> GetErrors(Level level)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(level.Name))
            errors.Add("Level name is empty.");

        if (level.PlayerStart == default)
            errors.Add("PlayerStart is not set.");

        if (level.Entities is null)
            errors.Add("Entities list is null.");

        return errors;
    }
}
