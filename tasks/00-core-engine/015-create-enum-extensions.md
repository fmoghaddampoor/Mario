# Task 015 — Create Enum Extensions

## Description
Implement utility extensions for enums used throughout the engine.

## Steps
1. Create EnumExtensions static class
2. Add GetName<T>(T value) caching
3. Add GetValues<T>() caching
4. Add Parse<T>(string value) with TryParse variant
5. Add HasFlagGeneric<T>() for .NET Standard compat
6. Create common game enums: GameState, PlayerState, EnemyType, PowerUpType

## Files to Create
- src/MarioEngine.Core/Extensions/EnumExtensions.cs

## Acceptance Criteria
- All enum operations are cached for performance
- No reflection in hot paths after caching
- Unit tests pass for all enum types
