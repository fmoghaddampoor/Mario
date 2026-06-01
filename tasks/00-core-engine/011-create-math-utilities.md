# Task 011 — Create Math Utilities

## Description
Implement common math structures and utilities for 2D game development.

## Steps
1. Create structs: Vector2, Vector2Int, Rectangle, Size
2. Include operators: +, -, *, /, ==, !=
3. Include methods: Lerp, Distance, Normalize, Dot, Cross, Clamp
4. Include helpers: Zero, One, UnitX, UnitY
5. Use System.Numerics where possible for SIMD
6. Add extension methods for common math operations
7. Create MathHelper with constants (Pi, Epsilon, etc.)

## Files to Create
- src/MarioEngine.Core/Math/Vector2.cs
- src/MarioEngine.Core/Math/Vector2Int.cs
- src/MarioEngine.Core/Math/Rectangle.cs
- src/MarioEngine.Core/Math/Size.cs
- src/MarioEngine.Core/Math/MathHelper.cs

## Acceptance Criteria
- All structs have complete operator overloads
- Unit tests pass for edge cases
- No boxing allocations
