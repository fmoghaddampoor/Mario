# Task 005 — Implement Logging System

## Description
Set up structured logging using Microsoft.Extensions.Logging.

## Steps
1. Add Microsoft.Extensions.Logging NuGet package
2. Create LogManager class with logger factory
3. Configure console logging with color output
4. Configure file logging with rotation (max 10MB, 5 files)
5. Set log levels: Debug in dev, Info in release
6. Create extension methods for common log patterns
7. Integrate logging into Game class

## Files to Create
- src/MarioEngine.Core/Logging/LogManager.cs
- src/MarioEngine.Core/Logging/FileLogger.cs
- src/MarioEngine.Core/Logging/FileLoggerProvider.cs

## Acceptance Criteria
- Logs appear in console with color coding
- Logs are written to logs/mario.log
- Log files rotate at 10MB
- F4 key toggles in-game log overlay
