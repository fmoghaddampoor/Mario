namespace MarioEngine.Desktop;

using MarioEngine.Core;

/// <summary>
/// Handles the window Closing event. Shuts down the game.
/// </summary>
internal sealed class MarioWindowClosingHandler
{
    private readonly Game _game;

    /// <summary>Initializes a new instance of the <see cref="MarioWindowClosingHandler"/> class.</summary>
    /// <param name="game">The game instance to shut down.</param>
    public MarioWindowClosingHandler(Game game)
    {
        _game = game;
    }

    /// <summary>Called when the window is closing. Shuts down the game.</summary>
    public void Handle()
    {
        _game.Shutdown();
    }
}
