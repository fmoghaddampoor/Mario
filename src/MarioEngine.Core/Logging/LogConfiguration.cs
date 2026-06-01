// <copyright file="LogConfiguration.cs" company="MarioGame">
// Copyright (c) MarioGame. All rights reserved.
// </copyright>

namespace MarioEngine.Core.Logging
{
    using System;
    using System.IO;
    using Serilog;
    using Serilog.Sinks.Grafana.Loki;

    public static class LogConfiguration
    {
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
            Log.Information("Logging initialized. Seq: {Seq}, Loki: {Loki}", seqUrl?.ToString() ?? "off", lokiUrl?.ToString() ?? "off");

            return new LogCloser();
        }

        private sealed class LogCloser : IDisposable
        {
            public void Dispose()
            {
                Log.CloseAndFlush();
            }
        }
    }
}
