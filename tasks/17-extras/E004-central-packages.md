# Task E004 — Set Up Central NuGet Package Management

## Description
Centralize all NuGet package versions in one file so every project references the same version.

## Why This Was Done Ahead of Task List
Went hand-in-hand with Directory.Build.props. Prevents version conflicts between projects.

## What Was Created
- `Directory.Packages.props` with `ManagePackageVersionsCentrally=true`
- All current packages listed with explicit versions:
  - Silk.NET (2.22.0), NAudio (2.2.1), NVorbis (0.10.5)
  - StbImageSharp (2.27.14), ImGui.NET (1.91.6.1)
  - Microsoft.Extensions.Logging (10.0.0)
  - Microsoft.Extensions.DependencyInjection (10.0.0)
  - Serilog + sinks + enrichers
  - Analyzers: StyleCop (1.2.0-beta.556), NetAnalyzers (10.0.100)

## Files
- `Directory.Packages.props`

## Acceptance Criteria
- All projects use the same package versions automatically
- Adding a new package to any .csproj requires only the name, no version
- `dotnet build` succeeds with 0 warnings, 0 errors
