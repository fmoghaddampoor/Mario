# Development Workflow — Coding Rules, Builds, Tooling

## Overview
Standardized workflows, coding conventions, design patterns, build automation, and tooling for the Mario game engine project.

---

## Coding Conventions

### Language: C# (.NET 8)

#### Naming Rules

| Element | Convention | Example |
|---|---|---|
| Namespaces | PascalCase, no underscores | `MarioEngine.Graphics` |
| Classes | PascalCase | `SpriteBatcher` |
| Interfaces | IPascalCase | `IRenderable` |
| Methods | PascalCase | `DrawSprite()` |
| Properties | PascalCase | `MaxSpeed` |
| Fields (private) | `_camelCase` | `_spriteBatch` |
| Fields (private static) | `s_camelCase` | `s_instance` |
| Fields (private static readonly) | `s_camelCase` | `s_defaultShader` |
| Constants | PascalCase | `GravityConstant` |
| Local variables | camelCase | `currentFrame` |
| Parameters | camelCase | `textureId` |
| Enum values | PascalCase | `PlayerState.Running` |
| Event handlers | `OnXxx` | `OnCollision()` |
| Async methods | `XxxAsync` | `LoadLevelAsync()` |

#### File Organization

```
One class per file (exceptions: small records, enums, or extensions)
File name = class name + .cs
Folder structure mirrors namespace structure
```

#### File Header

```csharp
// <copyright file="SpriteBatcher.cs" company="MarioGame">
// Copyright (c) MarioGame. All rights reserved.
// </copyright>
```

#### Brackets
- Always use braces, even for single-line blocks
- Allman style (opening brace on new line)
- No regions (except for auto-generated code)

```csharp
public void Update(float deltaTime)
{
    if (_isActive)
    {
        UpdatePosition(deltaTime);
    }
}
```

#### var Usage
- Use `var` when the type is obvious: `var player = new Player();`
- Use explicit type when it improves readability: `Player player = GetCurrentPlayer();`

#### Nullable Reference Types
- Enabled globally (`<Nullable>enable</Nullable>`)
- Annotate all public APIs with `?` where nullable
- Use `ArgumentNullException.ThrowIfNull()` for guard clauses

#### String Formatting
- Use string interpolation: `$"Player {_id}: {_health} HP"`
- Use `StringBuilder` for heavy concatenation in loops
- Never use `+` for strings in hot paths

---

## Design Patterns

### Core Project Patterns

| Pattern | Where Used | Purpose |
|---|---|---|
| **Singleton** | `Game`, `AssetManager`, `AudioEngine`, `InputManager` | Single-instance global systems |
| **ECS-Lite** | `Scene → Entity → Component` | Game object composition |
| **Component** | `SpriteRenderer`, `RigidBody`, `Collider`, `Animator` | Reusable behavior modules |
| **Observer / Event** | `EventBus`, `OnCollision`, `OnScoreChanged` | Decoupled communication |
| **State Machine** | `PlayerController`, `EnemyAI`, `SceneManager` | Clean state transitions |
| **Object Pool** | `BulletPool`, `SFXSourcePool`, `ParticlePool` | Reduce allocations |
| **Strategy** | Enemy AI behaviors, power-up effects | Swappable algorithms |
| **Factory** | `EntityFactory`, `ItemFactory` | Centralized creation |
| **Command** | `InputBuffer`, replay system | Queue + replay inputs |
| **Service Locator** | Global services via `Game.Services.Get<T>()` | Access without singleton coupling |
| **Flyweight** | `Tile`, `SpriteFrame` | Share immutable data |
| **Mediator** | Scene coordinates between entities | Centralized entity communication |

### Component Pattern Example

```csharp
public class PlayerController : Component
{
    [SerializeField] private float _moveSpeed = 200f;
    [SerializeField] private float _jumpForce = 600f;

    public override void Update(float dt)
    {
        // Read input, apply forces, manage state transitions
    }
}
```

### Event Bus Usage

```csharp
// Subscribe
EventBus.Subscribe<EnemyStompedEvent>(OnEnemyStomped);

// Publish
EventBus.Publish(new EnemyStompedEvent { Enemy = this, Score = 100 });
```

---

## StyleCop & Code Analysis

### StyleCop (Enabled)

| Rule | Severity | Notes |
|---|---|---|
| SA1201 (ordering) | Warning | Elements in correct order inside class |
| SA1309 (field naming) | Error | `_camelCase` enforced |
| SA1401 (public fields) | Error | Use properties, not public fields |
| SA1503 (braces) | Warning | Braces required |
| SA1600 (public docs) | Warning | All public members must have XML doc |
| SA1633 (file header) | Warning | File header required |
| SA1649 (file name) | Error | File name must match type name |

### Running StyleCop

```
dotnet build -warnaserror
dotnet format stylecop
```

### Roslyn Analyzers

| Analyzer | Purpose |
|---|---|
| Microsoft.CodeAnalysis.NetAnalyzers | General .NET best practices |
| Microsoft.CodeAnalysis.BannedApiAnalyzers | Block banned APIs (e.g. `Console.WriteLine`) |
| IDisposableAnalysis | Track missing disposals |
| PerformanceSensitiveAttribute | Hot-path annotations |

### Suppression
- Only suppress in `.editorconfig` (not `#pragma` in code)
- Every suppression must have a comment explaining why
- No blanket suppression files

---

## Build Scripts

### Project Structure

```
MarioEngine/
 ├── src/
 │    ├── MarioEngine.Core/       # Engine library (class library)
 │    ├── MarioEngine.Game/       # Game logic (class library)
 │    └── MarioEngine.Desktop/    # Desktop entry point (executable)
 ├── tests/
 │    ├── MarioEngine.Core.Tests/ # Unit tests
 │    └── MarioEngine.Game.Tests/ # Integration tests
 ├── tools/
 │    ├── atlas-packer/           # Texture atlas builder
 │    └── level-validator/        # Level file checker
 ├── assets/                      # Game assets
 ├── scripts/                     # Build/deploy scripts
 ├── build/                       # Build output
 ├── .editorconfig
 ├── Directory.Build.props
 └── MarioEngine.sln
```

### Scripts

All scripts in `scripts/` — PowerShell (.ps1) on Windows, bash (.sh) on macOS/Linux.

| Script | Description |
|---|---|
| `build.ps1` / `build.sh` | Build entire solution |
| `run.ps1` / `run.sh` | Build + run game |
| `test.ps1` / `test.sh` | Run all tests |
| `lint.ps1` / `lint.sh` | StyleCop + analyzer check |
| `clean.ps1` / `clean.sh` | Clean all build artifacts |
| `pack.ps1` / `pack.sh` | Create release package |
| `new-level.ps1` | Scaffold new level template |

### Build.ps1

```powershell
param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug",
    [switch]$Run,
    [switch]$Test,
    [switch]$Lint,
    [switch]$Clean
)

if ($Clean) { & "$PSScriptRoot\clean.ps1" }
if ($Lint) { dotnet format MarioEngine.sln --verify-no-changes }
dotnet build MarioEngine.sln -c $Configuration -warnaserror
if ($Test) { dotnet test MarioEngine.sln -c $Configuration --no-build }
if ($Run) { & "$PSScriptRoot\run.ps1" -Configuration $Configuration }
```

---

## Build & Run Commands

### Development Workflow

```powershell
# Full cycle: lint → build → test → run
.\scripts\build.ps1 -Lint -Test -Run

# Quick rebuild
.\scripts\build.ps1 -Run

# Just tests
.\scripts\build.ps1 -Test
```

### Manual Commands

```powershell
# Build Debug
dotnet build src/MarioEngine.Desktop/ -c Debug

# Build Release
dotnet build src/MarioEngine.Desktop/ -c Release

# Run Debug
dotnet run --project src/MarioEngine.Desktop/ -c Debug

# Run Release
dotnet run --project src/MarioEngine.Desktop/ -c Release -- --fullscreen

# Run tests
dotnet test MarioEngine.sln -c Debug

# Run specific test
dotnet test --filter "FullyQualifiedName~JumpTests"

# Watch mode (auto-rebuild on changes)
dotnet watch run --project src/MarioEngine.Desktop/
```

### Flags (Command-Line)

| Flag | Description | Default |
|---|---|---|
| `--fullscreen` | Start in fullscreen | false |
| `--windowed` | Force windowed mode | true |
| `--width 1920` | Window width | 1920 |
| `--height 1080` | Window height | 1080 |
| `--level 1-1` | Load specific level on start | last saved |
| `--debug-overlay` | Show FPS/debug overlay | false |
| `--log-level debug` | Log level override | info |
| `--no-audio` | Disable audio | false |
| `--fixed-timestep` | Use fixed timestep | true |

### Build Profiles

#### Debug
- Optimizations: none
- Symbols: full PDB
- Define: `DEBUG`, `TRACE`
- Editor enabled: yes
- Log level: debug
- Build time: fast (~10s)

#### Release
- Optimizations: full
- Symbols: embedded PDB
- Define: `TRACE`
- Editor enabled: no (compile-time flag to exclude)
- Log level: info
- Build time: moderate (~30s)

#### Dist
- Optimizations: full
- Symbols: none (stripped)
- Define: `RELEASE`
- Editor enabled: no
- Log level: warning
- Single-file publish: `dotnet publish -p:PublishSingleFile=true`
- Build time: slow (~60s)

---

## Versioning

### Scheme: Semantic Versioning (SemVer 2.0)

```
MAJOR.MINOR.PATCH[-PRERELEASE][+BUILD]
```

| Component | When to Increment | Example |
|---|---|---|
| MAJOR | Breaking changes, full release | `1.0.0`, `2.0.0` |
| MINOR | New features, non-breaking | `1.1.0`, `1.2.0` |
| PATCH | Bug fixes, minor changes | `1.0.1`, `1.0.2` |
| PRERELEASE | Alpha/beta/rc builds | `0.5.0-alpha`, `1.0.0-beta.2` |
| BUILD | CI build number | `1.0.0+20260601` |

### Where Version Lives

**Directory.Build.props** (single source of truth):

```xml
<PropertyGroup>
    <VersionPrefix>0.1.0</VersionPrefix>
    <VersionSuffix>alpha</VersionSuffix>
    <AssemblyVersion>$(VersionPrefix).0</AssemblyVersion>
    <FileVersion>$(VersionPrefix).$(BuildNumber)</FileVersion>
    <InformationalVersion>$(VersionPrefix)-$(VersionSuffix)+$(BuildSHA)</InformationalVersion>
</PropertyGroup>
```

### Tagging

```
git tag v0.1.0-alpha
git tag v0.1.1-rc.1
git tag v1.0.0
```

### CI Auto-Versioning

- CI overrides `BuildNumber` with `$(Build.BuildId)` or `$(GITHUB_RUN_NUMBER)`
- CI sets `BuildSHA` to short commit hash
- Pre-release builds: `0.1.0-alpha.42`
- Release builds: `1.0.0.0` (AssemblyVersion stays stable)

---

## Logging

### Framework: `Microsoft.Extensions.Logging`

No `Console.WriteLine` anywhere in code (banned by analyzer).

### Configuration

```csharp
// Program.cs
using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddConsole()
        .AddFile("logs/mario.log", outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {SourceContext}: {Message}{NewLine}{Exception}")
        .SetMinimumLevel(GetLogLevel());
});

// Game.cs
private readonly ILogger<Game> _logger;

public Game(ILogger<Game> logger)
{
    _logger = logger;
}

public void Initialize()
{
    _logger.LogInformation("Game initializing (version {Version})", VersionInfo.Current);
}
```

### Log Levels

| Level | When to use | Color |
|---|---|---|
| `Trace` | Enter/exit methods, very detailed flow | Grey |
| `Debug` | Variable values, entity state, collision checks | Cyan |
| `Information` | Level loaded, save created, settings changed | Green |
| `Warning` | Missing asset fallback, minor performance issue | Yellow |
| `Error` | Failed to load asset, physics glitch, unhandled case | Red |
| `Critical` | Crash imminent, out of memory, fatal error | Red+white |

### Category Conventions

```csharp
// Per class via DI
private readonly ILogger<PlayerController> _logger;

// Per system for cross-cutting
private readonly ILogger _renderLogger = Log.ForContext("System", "Rendering");
```

### What to Log

**ALWAYS log:**
- Game startup/shutdown (info)
- Level load/unload (info)
- Save/load operations (info)
- Asset load failures (error)
- Unhandled exceptions (critical)
- Settings changes (debug)
- Performance warnings (> 16ms frame) (warning)

**NEVER log:**
- Every frame's FPS (trace only, and only when requested)
- Player position every frame (debug only, and rate-limited to 1Hz)
- Passwords, tokens, personal data
- Full stack traces for expected errors (log the message only)

### Log File

| Property | Value |
|---|---|
| Path | `logs/mario.log` (next to executable) |
| Max size | 10 MB |
| Retention | 5 rotated files (`mario.1.log`, `mario.2.log`, ...) |
| Format | `HH:mm:ss [LEVEL] Category: Message` |

### In-Game Log Viewer
- Press F4 to toggle log overlay (last 50 messages)
- Filter by level (click level tabs)
- Copy to clipboard button

---

## Git Workflow

### Branch Strategy

```
main          → Production-ready code
├── develop   → Integration branch
│    ├── feature/xxx  → New features
│    ├── fix/xxx      → Bug fixes
│    └── refactor/xxx → Refactoring
├── release/x.x.x  → Release preparation
└── hotfix/x.x.x   → Urgent production fixes
```

### Commit Messages

```
type(scope): brief description

Optional body with details.

- Use imperative mood ("Add", "Fix", not "Added", "Fixed")
- Scope: core, graphics, audio, physics, scene, input, ui, editor, tests, build
- Types: feat, fix, refactor, test, docs, chore, perf, style
```

Examples:
```
feat(core): add fixed timestep physics loop
fix(physics): correct slope collision normal calculation
refactor(audio): extract MP3 streaming into separate class
test(physics): add jump arc unit tests
docs(build): update build instructions
```

### PR Requirements
- [ ] Builds without errors
- [ ] All tests pass
- [ ] Lint passes (StyleCop + analyzers)
- [ ] At least 1 reviewer approval
- [ ] No merge conflicts with develop

---

## CI/CD Pipeline

### Stages (GitHub Actions)

```
[Lint] → [Build Debug] → [Test] → [Build Release] → [Package] → [Deploy]
```

### Triggers

| Event | Action |
|---|---|
| Push to `feature/*` | Lint + Build Debug + Test |
| PR to `develop` | Lint + Build Debug + Test |
| Push to `develop` | Full pipeline (except Deploy) |
| PR to `main` | Full pipeline (except Deploy) |
| Tag `v*` | Full pipeline + Package + Deploy (GitHub Release) |

### Artifacts

| Artifact | Retention |
|---|---|
| Debug build | 1 day |
| Test results | 30 days |
| Release package (.zip) | 90 days |
| Symbols (.pdb) | 90 days |

---

## Code Review Checklist

- [ ] Follows coding conventions (naming, braces, nullables)
- [ ] No `Console.WriteLine` or `Debug.WriteLine` (use logger)
- [ ] No magic numbers (use constants from `PhysicsConstants` or local `const`)
- [ ] All public APIs have XML doc comments
- [ ] No hardcoded paths (use `AssetManager` or config)
- [ ] IDisposable properly implemented (if holding unmanaged resources)
- [ ] No allocations in hot paths (use object pools, structs, `Span<T>`)
- [ ] Tests written for: success paths, failure paths, edge cases
- [ ] No secrets, keys, or credentials in code
- [ ] Performance: no boxing, no LINQ in hot paths, no repeated string concat
- [ ] Thread safety: `lock` or `ConcurrentDictionary` for shared state
- [ ] `async void` only for event handlers (fire-and-forget)
- [ ] All `Task`s properly awaited or fire-and-forget with error handling

---

## Hot Path Rules

Code that runs every frame must follow these rules:

```
DO:
- Use structs for small data (Vector2, Color, Rectangle)
- Use object pools (BulletPool, ParticlePool)
- Use ArrayPool<byte> for temporary buffers
- Use Span<T> for slice operations
- Prefer for/foreach over LINQ
- Prefer switch expressions over if-else chains

DON'T:
- No allocations (new class, new string, boxing)
- No LINQ .Where().Select().ToList() chains
- No string concatenation (+=)
- No dynamic/reflection
- No try-catch in expected flow (use return codes)
- No lock contention (use double-buffered state)
```

### Hot Path Detection
- CI runs PerformanceSensitive analyzer
- Methods marked `[PerformanceSensitive]` trigger warnings on violations
- Profile with `dotnet trace` before optimizing (never guess)
