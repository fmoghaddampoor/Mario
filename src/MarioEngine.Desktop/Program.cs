namespace MarioEngine.Desktop;

using System;
using System.IO;
using MarioEngine.Core;
using MarioEngine.Core.Config;
using MarioEngine.Core.DependencyInjection;
using MarioEngine.Core.Logging;
using MarioEngine.Desktop.Resources;
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
        var iniPath = Path.Combine(AppContext.BaseDirectory, "app.ini");
        var config = File.Exists(iniPath) ? IniConfig.Load(iniPath) : null;

        var seqUrl = config?.GetString("Logging", "SeqUrl");
        var lokiUrl = config?.GetString("Logging", "LokiUrl");
        var splashDuration = config?.GetFloat("Splash", "Duration", 3f) ?? 3f;

        using var logCloser = LogConfiguration.Initialize(
            logDirectory: "logs",
            seqUrl: string.IsNullOrEmpty(seqUrl) ? null : new Uri(seqUrl),
            lokiUrl: string.IsNullOrEmpty(lokiUrl) ? null : new Uri(lokiUrl),
            applicationName: "MarioGame");

        try
        {
            using var services = GameServiceProvider.CreateDefault();
            var game = services.Get<Game>();
            var logger = services.Get<ILogger<MarioWindow>>();

            using var marioWindow = MarioWindow.Create(args, logger, splashDuration);
            marioWindow.WireGameEvents(game);
            marioWindow.Run();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException)
        {
            Log.Fatal(ex, Resources.Strings.Fatal_UnhandledException);
            Console.Error.WriteLine(string.Format(Resources.Strings.Fatal_ErrorMessage, ex.Message));
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
