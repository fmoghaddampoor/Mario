namespace MarioEngine.Desktop;

using MarioEngine.Core;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="MarioWindow.WireGameEvents"/> method for the <see cref="MarioWindow"/> class.
/// Creates handler instances for each window event and subscribes them.
/// </summary>
internal sealed partial class MarioWindow
{
    /// <summary>
    /// Subscribes game lifecycle methods to the corresponding window events.
    /// Displays a splash screen for 3 seconds before starting the game.
    /// </summary>
    /// <param name="game">The game instance to wire up.</param>
    public void WireGameEvents(Game game)
    {
        var state = new GameStartupState();
        _startupState = state;

        var loadHandler = new MarioWindowLoadHandler(this);
        var updateHandler = new MarioWindowUpdateHandler(game, state, _logger);
        var renderHandler = new MarioWindowRenderHandler(this, game, state);
        var closingHandler = new MarioWindowClosingHandler(game);

        _window.Load += loadHandler.Handle;
        _window.Update += (dt) => updateHandler.Handle((float)dt);
        _window.Render += (dt) => renderHandler.Handle((float)dt);
        _window.Closing += closingHandler.Handle;
    }
}
