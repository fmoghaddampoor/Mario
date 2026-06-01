# Task 006 — Create Configuration System

## Description
Build a configuration system for game settings.

## Steps
1. Create GameConfig class with all configurable settings
2. Support JSON serialization/deserialization
3. Store config file at platform-appropriate path
4. Implement config sections: Video, Audio, Input, Gameplay
5. Add defaults for all settings
6. Create ConfigManager to load/save at startup/shutdown
7. Integrate with command-line argument parsing

## Files to Create
- src/MarioEngine.Core/Config/GameConfig.cs
- src/MarioEngine.Core/Config/ConfigManager.cs
- src/MarioEngine.Core/Config/ConfigSection.cs

## Acceptance Criteria
- Config loads from file on startup
- Config saves on shutdown
- Defaults used if no config file exists
- Command-line args override config values
