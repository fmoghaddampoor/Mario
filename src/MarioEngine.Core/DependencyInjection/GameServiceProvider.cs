// <copyright file="GameServiceProvider.cs" company="MarioGame">
// Copyright (c) MarioGame. All rights reserved.
// </copyright>

namespace MarioEngine.Core.DependencyInjection
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

    public sealed class GameServiceProvider : IDisposable
    {
        private readonly ServiceProvider provider;

        public GameServiceProvider()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            this.provider = services.BuildServiceProvider();
        }

        public static GameServiceProvider CreateDefault()
        {
            var provider = new GameServiceProvider();
            Log.Information("DI container initialized");
            return provider;
        }

        public T Get<T>()
            where T : notnull
        {
            return this.provider.GetRequiredService<T>();
        }

        public T? TryGet<T>()
            where T : class
        {
            return this.provider.GetService<T>();
        }

        public void Dispose()
        {
            this.provider.Dispose();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog(dispose: true);
            });
        }
    }
}
