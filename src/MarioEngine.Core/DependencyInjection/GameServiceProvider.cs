namespace MarioEngine.Core.DependencyInjection;

using System;
using MarioEngine.Core.Resources;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

/// <summary>
/// Wraps the <see cref="ServiceProvider"/> and provides convenience methods
/// for resolving game services. All game systems are registered here.
/// </summary>
public sealed class GameServiceProvider : IDisposable
{
    /// <summary>Underlying service provider instance.</summary>
    private readonly ServiceProvider _provider;

    /// <summary>Initializes a new instance of the <see cref="GameServiceProvider"/> class.</summary>
    public GameServiceProvider()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _provider = services.BuildServiceProvider();
    }

    /// <summary>
    /// Creates a default service provider with logging and core services registered.
    /// </summary>
    /// <returns>A configured <see cref="GameServiceProvider"/> ready for use.</returns>
    public static GameServiceProvider CreateDefault()
    {
        var provider = new GameServiceProvider();
        Log.Information(Resources.Strings.DI_Container_Initialized);
        return provider;
    }

    /// <summary>Resolves a required service of type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <returns>The resolved service instance.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the service is not registered.</exception>
    public T Get<T>()
        where T : notnull
    {
        return _provider.GetRequiredService<T>();
    }

    /// <summary>Attempts to resolve an optional service of type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <returns>The resolved service instance, or null if not registered.</returns>
    public T? TryGet<T>()
        where T : class
    {
        return _provider.GetService<T>();
    }

    /// <summary>Disposes the underlying service provider.</summary>
    public void Dispose()
    {
        _provider.Dispose();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.AddSerilog(dispose: true);
        });

        services.AddSingleton<Game>();
    }
}
