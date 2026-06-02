namespace MarioEngine.Core.Levels;

/// <summary>Static utility for generating levels from files or defaults.</summary>
internal static class LevelGenerator
{
    /// <summary>Generates a level from a file path.</summary>
    public static Level GenerateFromFile(string filePath)
    {
        return new Level();
    }

    /// <summary>Generates a default level with the given name.</summary>
    public static Level GenerateDefault(string name)
    {
        return new Level { Name = name };
    }
}
