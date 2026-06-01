namespace MarioEngine.Core.Logging;

using System;
using Serilog;

/// <summary>
/// Disposable wrapper that flushes and closes the Serilog logger on dispose.
/// Returned by <see cref="LogConfiguration.Initialize"/> to ensure clean shutdown.
/// </summary>
internal sealed class LogCloser : IDisposable
{
    /// <summary>Flushes all log events and closes the logger.</summary>
    public void Dispose()
    {
        Log.CloseAndFlush();
    }
}
