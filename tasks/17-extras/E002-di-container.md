# Task E002 — Set Up DI Container (Microsoft.Extensions.DependencyInjection)

## Description
Set up dependency injection for all game systems.

## Why This Was Done Ahead of Task List
The logging system needed DI to inject ILogger<T> into all classes, so we set up the container early.

## What Was Created
- `GameServiceProvider` — wraps IServiceCollection/ServiceProvider, singleton access, default service registration (AddLogging + AddSerilog)
- All future systems will be registered here

## Files
- `src/MarioEngine.Core/DependencyInjection/GameServiceProvider.cs`

## Acceptance Criteria
- Services can be resolved via `Get<T>()`
- ILogger<T> auto-injected for registered services
- `dotnet build` succeeds with 0 warnings, 0 errors
