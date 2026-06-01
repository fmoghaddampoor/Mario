# C# Coding Standards — Mario Engine

> These rules are mandatory for all code written in this project. The AI agent must follow them on every code generation task.

---

## 1. Naming

| Element | Rule | Example |
|---|---|---|
| Namespaces | PascalCase, no underscores | `MarioEngine.Graphics` |
| Classes | PascalCase | `SpriteBatcher` |
| Interfaces | IPascalCase | `IRenderable` |
| Methods | PascalCase | `DrawSprite()` |
| Properties | PascalCase | `MaxSpeed` |
| Private fields | `_camelCase` | `_spriteBatch` |
| Constants | PascalCase | `GravityConstant` |
| Local variables | camelCase | `currentFrame` |
| Parameters | camelCase | `textureId` |
| Enum values | PascalCase | `PlayerState.Running` |
| Async methods | `XxxAsync` suffix | `LoadLevelAsync()` |
| File name | Must match class name | `SpriteBatcher.cs` |
| One class per file | Yes | No nested classes; each class/interface/struct in its own file |

## 2. Style

- **Braces**: Allman style (opening brace on new line)
- **Brace usage**: Always use braces, even for single-line blocks
- **Regions**: Never use regions
- **var**: Use when type is obvious (`var player = new Player()`); explicit type when ambiguous (`Player player = GetCurrentPlayer()`)
- **String formatting**: Use interpolation (`$"Health: {_health}"`); use StringBuilder in loops
- **Nullables**: Enabled globally; annotate all public APIs with `?` where nullable; use `ArgumentNullException.ThrowIfNull()` for guards

## 3. Documentation

- Every member must have an XML doc comment (`/// <summary>`), including private fields and constants
- Every method parameter must have a `<param>` tag
- Every return value must have a `<returns>` tag
- Every thrown exception must have an `<exception>` tag
- No copyright/file headers at the top of .cs files

```csharp
/// <summary>Maximum walk speed in pixels per second.</summary>
private const float MaxWalkSpeed = 200f;

/// <summary>Logger instance for this class.</summary>
private readonly ILogger<PlayerController> _logger;

/// <summary>Processes player input and updates movement.</summary>
/// <param name="dt">Delta time in seconds.</param>
/// <param name="input">Current input state snapshot.</param>
/// <returns>True if the player moved this frame.</returns>
private bool ProcessMovement(float dt, InputState input)
{
}
```

## 4. Usings

Place using directives inside the namespace block:
```csharp
namespace MarioEngine.Core.Graphics
{
    using System;
    using Serilog;
    ...
}
```

No `global using` in .cs files (defined in Directory.Build.props).

## 5. Logging

- Never use `Console.WriteLine` — banned by analyzer
- Always inject `ILogger<T>` via constructor DI
- Use structured properties, never string interpolation in log messages:
  ```csharp
  // CORRECT
  _logger.LogInformation("Player {Id} scored {Points}", id, score);
  
  // WRONG
  _logger.LogInformation($"Player {id} scored {score}");
  ```
- Log levels: Verbose (flow), Debug (state), Info (milestones), Warning (recoverable), Error (failures), Fatal (crashes)
- Rate-limit high-frequency logs (max 1 Hz)

## 6. DI (Dependency Injection)

- All services registered in `GameServiceProvider`
- Use constructor injection, never service locator pattern in application code
- Do not resolve from `GameServiceProvider` directly — inject dependencies
- Mark all services with their lifetime: Singleton, Transient, or Scoped

## 7. Error Handling

- Use exceptions for exceptional cases only, not for control flow
- Always log exceptions at Error or Fatal level when caught
- Use `Result<T>` or nullable returns for expected failure paths
- Never swallow exceptions silently
- Always dispose IDisposable (use `using` or `.Dispose()`)

## 8. Performance (Hot Path Rules)

Code that runs every frame must follow these:

```
DO:
- Use structs for small data (Vector2, Color, Rectangle)
- Use object pools (BulletPool, ParticlePool)
- Use ArrayPool<byte> for temporary buffers
- Use Span<T> for slice operations
- Prefer for/foreach over LINQ
- Prefer switch expressions over if-else chains
- Use StringBuilder for heavy string work

DON'T:
- No allocations in hot paths (new class, new string, boxing)
- No LINQ .Where().Select().ToList() chains
- No string concatenation (+=)
- No dynamic/reflection in update loops
- No try-catch in expected flow
- No lock contention (use double-buffered state)
```

## 9. Async

- `async void` only allowed for event handlers (fire-and-forget)
- All Task-returning methods must end with `Async` suffix
- Always await or explicitly fire-and-forget with error handling
- Prefer `ValueTask` for high-frequency async methods to reduce allocation

## 10. Tests

- Every public method should have unit tests: success path, failure path, edge cases
- Test class name: `{ClassName}Tests`
- Test method name: `{MethodName}_{Scenario}_Returns{Expected}`
- Use NUnit or xUnit (follow project convention)

## 11. Maximum Class Size

- **Soft limit**: 300 lines per class (including braces, comments, blank lines)
- **Hard limit**: 500 lines — anything over this must be refactored
- **Exception**: Generated code, large switch expressions, or data-heavy builder classes (max 800 lines)
- If a class exceeds 300 lines, split into smaller classes by responsibility

## 12. File Organization

```
File name = class name + .cs
Folder structure mirrors namespace
No copyright headers
Usings inside namespace
Members ordered: constants → fields → constructor → properties → methods → private methods
```

## 13. What NOT to Do

- No hardcoded paths (use AssetManager or config)
- No secrets/keys/credentials in code (use env vars)
- No regions
- No `#pragma warning disable` without comment explaining why
- No empty catch blocks
- No public fields (use properties)
- No `DateTime.Now` (use `DateTime.UtcNow` or `Time.GameTime`)
