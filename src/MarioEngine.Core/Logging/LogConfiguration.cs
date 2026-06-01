namespace MarioEngine.Core.Logging;

using System;
using System.IO;
using MarioEngine.Core.Resources;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

/// <summary>
/// Configures the Serilog logging pipeline with up to 4 simultaneous sinks:
/// Console, File, Seq, and Grafana Loki. Call <see cref="Initialize"/> once at startup.
/// </summary>
public static class LogConfiguration
{
    /// <summary>
    /// Initializes the Serilog logger with the specified configuration.
    /// </summary>
    /// <param name="logDirectory">Directory path for rolling log files.</param>
    /// <param name="seqUrl">Optional Seq server URL (e.g. http://localhost:5341). Pass null to disable.</param>
    /// <param name="lokiUrl">Optional Grafana Loki URL (e.g. http://localhost:3100). Pass null to disable.</param>
    /// <param name="applicationName">Application name label attached to all log events.</param>
    /// <returns>A disposable that flushes and closes the logger on dispose.</returns>
    public static IDisposable Initialize(string logDirectory, Uri? seqUrl, Uri? lokiUrl, string applicationName)
    {
        var config = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.WithProperty("Application", applicationName)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development")
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: Path.Combine(logDirectory, "mario-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}");

        if (seqUrl != null)
        {
            var seqKey = Environment.GetEnvironmentVariable("MARIO_SEQ_KEY");
            config = config.WriteTo.Seq(
                serverUrl: seqUrl.ToString(),
                apiKey: seqKey,
                period: TimeSpan.FromSeconds(2));
        }

        if (lokiUrl != null)
        {
            config = config.WriteTo.GrafanaLoki(
                uri: lokiUrl.ToString(),
                period: TimeSpan.FromSeconds(5),
                labels: new[]
                {
                    new LokiLabel { Key = "app", Value = applicationName },
                    new LokiLabel { Key = "env", Value = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "dev" },
                });
        }

        Log.Logger = config.CreateLogger();
        Log.Information(Resources.Strings.Logging_Initialized, seqUrl?.ToString() ?? "off", lokiUrl?.ToString() ?? "off");

        return new LogCloser();
    }
}
