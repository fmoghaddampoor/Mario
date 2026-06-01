# Task 002 — Set Up Directory.Build.props

## Description
Create shared build configuration for all projects in the solution.

## Steps
1. Create Directory.Build.props at solution root
2. Set common properties: TargetFramework=net8.0, Nullable=enable, ImplicitUsings=enable
3. Configure versioning: VersionPrefix=0.1.0, VersionSuffix=alpha
4. Set up common analyzers: StyleCop, Roslyn analyzers
5. Add common usings (System, System.Collections.Generic, etc.)

## Files to Create
- Directory.Build.props
- .editorconfig

## Acceptance Criteria
- All projects inherit common properties
- dotnet build respects the shared config
- StyleCop warnings appear on violations
