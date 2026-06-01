# Task 007 — Create Application Entry Point

## Description
Implement the main entry point with command-line argument parsing.

## Steps
1. Create Program.Main() that parses command-line args
2. Support flags: --fullscreen, --width, --height, --level, --debug-overlay, --log-level, --no-audio
3. Initialize ConfigManager from args + config file
4. Create and run the Game instance
5. Handle unhandled exceptions with crash dialog
6. Save crash dump to crashes/ folder

## Files to Modify
- src/MarioEngine.Desktop/Program.cs

## Acceptance Criteria
- dotnet run starts the game
- dotnet run -- --fullscreen starts fullscreen
- Invalid args show error and usage
- Crash creates dump file
