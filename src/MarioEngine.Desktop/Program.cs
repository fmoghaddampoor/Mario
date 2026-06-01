namespace MarioEngine.Desktop;

using System;
using MarioEngine.Core;
using MarioEngine.Core.DependencyInjection;
using MarioEngine.Core.Logging;
using Microsoft.Extensions.Logging;
using Serilog;

/// <summary>
/// Application entry point. Creates the game window, sets up logging,
/// initializes the DI container, and runs the game loop.
/// </summary>
internal static class Program
{
    /// <summary>Entry point.</summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        using var logCloser = LogConfiguration.Initialize(
            logDirectory: "logs",
            seqUrl: null,
            lokiUrl: null,
            applicationName: "MarioGame");

        try
        {
            using var services = GameServiceProvider.CreateDefault();
            var game = services.Get<Game>();
            var logger = services.Get<ILogger<MarioWindow>>();

            using var marioWindow = MarioWindow.Create(args, logger);
            marioWindow.WireGameEvents(game);
            marioWindow.Run();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Fatal(ex, "Unhandled exception \u2014 game crashed");
            Console.Error.WriteLine($"Fatal error: {ex.Message}");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
