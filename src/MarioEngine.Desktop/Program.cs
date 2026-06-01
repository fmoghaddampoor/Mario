namespace MarioEngine.Desktop;

using System;
using MarioEngine.Core;
using MarioEngine.Core.DependencyInjection;
using MarioEngine.Core.Logging;
using Serilog;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

/// <summary>
/// Application entry point. Creates the Silk.NET window, sets up logging,
/// initializes the DI container, and wires the game lifecycle to window events.
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

            var options = WindowOptions.Default;
            options.Title = "Super Mario \u2014 v" + VersionInfo.Current;
            options.Size = new Silk.NET.Maths.Vector2D<int>(1280, 720);
            options.WindowBorder = WindowBorder.Resizable;
            options.VSync = true;
            options.API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.ForwardCompatible, new APIVersion(4, 6));

            var window = Window.Create(options);

            window.Load += () =>
            {
                Log.Information("Window opened");
                game.Initialize();
                game.LoadContent();
            };

            window.Update += (dt) =>
            {
                game.ProcessInput((float)dt);
                game.Update((float)dt);
            };

            window.Render += (dt) =>
            {
                Time.Update((float)dt);
                game.Render(0f);
            };

            window.Closing += () =>
            {
                Log.Information("Window closing");
                game.Shutdown();
            };

            window.Run();
            window.Dispose();
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
