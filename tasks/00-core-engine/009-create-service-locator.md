# Task 009 — Create Service Locator

## Description
Implement a service locator for global system access.

## Steps
1. Create Services static class
2. Add Register<T>(T service) and Get<T>() methods
3. Add IsRegistered<T>() check
4. Ensure thread safety with ReaderWriterLockSlim
5. Log service registration and resolution
6. Register core services: LogManager, ConfigManager, AssetManager, etc.

## Files to Create
- src/MarioEngine.Core/Services.cs

## Acceptance Criteria
- Services can be registered and resolved
- Missing service throws descriptive exception
- Thread-safe for concurrent access
