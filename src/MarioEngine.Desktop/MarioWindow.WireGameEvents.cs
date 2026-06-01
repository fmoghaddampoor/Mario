namespace MarioEngine.Desktop;

using MarioEngine.Core;
using Microsoft.Extensions.Logging;

/// <summary>
/// Contains the <see cref="MarioWindow.WireGameEvents"/> method for the <see cref="MarioWindow"/> class.
/// Wires the game lifecycle to window events with a 3-second splash screen transition.
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
        _window.Load += () =>
        {
            _splash = SplashScreen.Create(this.GL);
        };

        _window.Update += (dt) =>
        {
            if (_gameStarted)
            {
                game.ProcessInput((float)dt);
                game.Update((float)dt);
                return;
            }

            _splash?.Update((float)dt);

            if (_splash != null && _splash.IsFinished)
            {
                _logger.LogInformation("Splash finished, starting game");
                _splash.Dispose();
                _splash = null;
                _gameStarted = true;
                game.Initialize();
                game.LoadContent();
            }
        };

        _window.Render += (dt) =>
        {
            Time.Update((float)dt);

            if (!_gameStarted)
            {
                _splash?.Render();
                return;
            }

            game.Render(0f);
        };

        _window.Closing += () =>
        {
            game.Shutdown();
        };
    }
}
