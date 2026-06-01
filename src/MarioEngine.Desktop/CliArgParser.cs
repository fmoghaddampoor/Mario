namespace MarioEngine.Desktop;

/// <summary>
/// Parses command-line arguments for game window configuration.
/// Supports --windowed, --width, and --height flags.
/// Defaults to fullscreen at 1920x1080.
/// </summary>
internal static class CliArgParser
{
    /// <summary>
    /// Parses command-line arguments for window configuration.
    /// </summary>
    /// <param name="args">Command-line arguments array.</param>
    /// <returns>Tuple of (fullscreen, width, height).</returns>
    public static (bool Fullscreen, int Width, int Height) Parse(string[] args)
    {
        var fullscreen = true;
        var width = 1920;
        var height = 1080;

        for (var i = 0; i < args.Length; i++)
        {
            switch (args[i].ToUpperInvariant())
            {
                case "--WINDOWED":
                case "-W":
                    fullscreen = false;
                    break;
                case "--WIDTH":
                    if (++i < args.Length && int.TryParse(args[i], out var w))
                    {
                        width = w;
                    }

                    break;
                case "--HEIGHT":
                case "-H":
                    if (++i < args.Length && int.TryParse(args[i], out var h))
                    {
                        height = h;
                    }

                    break;
            }
        }

        return (fullscreen, width, height);
    }
}
