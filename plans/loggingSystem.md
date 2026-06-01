# Logging System — Complete Guide

## Overview
Structured logging pipeline using Serilog with 4 simultaneous sinks. Every log event carries timestamps, source context, and structured properties searchable across all sinks.

---

## Architecture

```
Game Code → ILogger<T> (DI-injected abstraction)
              ↓
         Serilog (provider)
              ↓
    ┌─────┬─────┬──────┬──────┐
    │     │     │      │      │
  Console  File  Seq  Grafana
                     Loki
```

All 4 sinks run simultaneously — every log event goes everywhere.

---

## Sinks

| Sink | Where | Retention | When to use |
|---|---|---|---|
| Console | Terminal/stdout | Session only | Real-time dev feedback |
| File | `logs/mario-2026-06-01.log` | 14 days rolling | Offline debugging, crash forensics |
| Seq | `http://localhost:5341` | Configurable in Seq | Local structured log browsing |
| Grafana Loki | `http://localhost:3100` | Configurable in Loki | Dashboards, alerts, production telemetry |

---

## Quick Start

### 1. Run Seq (local log viewer)

```powershell
docker run -d --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq
```

Open browser at **http://localhost:5341**.

### 2. Run Grafana + Loki (dashboards)

```powershell
docker run -d --name loki -p 3100:3100 grafana/loki
docker run -d --name grafana -p 3000:3000 grafana/grafana
```

Open browser at **http://localhost:3000** (admin/admin). Add Loki data source: `http://loki:3100`.

### 3. Game startup auto-connects

The game initializes logging on startup:
```csharp
LogConfiguration.Initialize(
    logDirectory: "logs",
    seqUrl: new Uri("http://localhost:5341"),
    lokiUrl: new Uri("http://localhost:3100"),
    applicationName: "MarioGame");
```

If Seq or Loki is not running, the game continues silently — those sinks are non-blocking.

---

## Log Files

### Location
```
<executable-directory>/logs/mario-2026-06-01.log
<executable-directory>/logs/mario-2026-06-02.log
...
```

### Format
```
2026-06-01 14:30:00.123 [INF] MarioEngine.Game.PlayerController: Player 42 scored 1000 points
2026-06-01 14:30:00.456 [WRN] MarioEngine.Core.Assets.AssetManager: Texture 'goomba.png' not found, using fallback
2026-06-01 14:30:01.789 [ERR] MarioEngine.Core.Physics.PhysicsSystem: Collision manifold overflow — 32 bodies touching
```

### Properties
| Column | Description |
|---|---|
| Timestamp | `yyyy-MM-dd HH:mm:ss.fff` |
| Level | `[INF]`, `[DBG]`, `[WRN]`, `[ERR]`, `[FTL]`, `[VRB]` |
| SourceContext | Full namespace and class name |
| Message | Structured message with properties |
| Exception | Full exception with stack trace (if applicable) |

---

## Log Levels

| Level | Short | Console Color | When to use |
|---|---|---|---|
| `Verbose` | VRB | Grey | Method enter/exit, every-fame diagnostics |
| `Debug` | DBG | Cyan | Variable values, entity state, collision checks |
| `Information` | INF | Green | Level loaded, save created, settings changed |
| `Warning` | WRN | Yellow | Missing asset fallback, minor performance issue |
| `Error` | ERR | Red | Failed asset load, physics glitch, unhandled case |
| `Fatal` | FTL | Red+white bold | Crash imminent, out of memory, unrecoverable error |

### Level Thresholds

| Build Config | Minimum Level |
|---|---|
| Debug | Verbose |
| Release | Information |
| Dist | Warning |

Override with `--log-level debug` flag.

---

## Structured Logging

Always use structured properties — never string interpolation in log messages.

### Correct
```csharp
_logger.LogInformation("Player {Id} scored {Points} points at position {Position}", 
    playerId, score, position);
```

### Wrong
```csharp
_logger.LogInformation($"Player {playerId} scored {score} points");  // DON'T
```

### Why
Serilog captures the property names and values as structured data. In Seq, you can search:

```
Id=42 AND Points>1000
```

With string interpolation, the values are baked into the text — not searchable as fields.

### Common Properties

Every log event carries these automatically:
| Property | Source | Example |
|---|---|---|
| `Application` | Static | `"MarioGame"` |
| `Environment` | `DOTNET_ENVIRONMENT` | `"Development"` |
| `MachineName` | Enricher | `"DEV-PC-01"` |
| `ThreadId` | Enricher | `7` |
| `SourceContext` | Serilog class | `"MarioEngine.Game.PlayerController"` |
| `Timestamp` | Serilog | `2026-06-01T14:30:00.123Z` |

---

## Timestamps

| Sink | Format | Timezone |
|---|---|---|
| Console | `HH:mm:ss.fff` | Local |
| File | `yyyy-MM-dd HH:mm:ss.fff` | Local |
| Seq | `yyyy-MM-ddTHH:mm:ss.fffZ` | UTC (ISO 8601) |
| Loki | `yyyy-MM-ddTHH:mm:ss.fffZ` | UTC (Unix ns timestamp) |

All timestamps include millisecond precision (`.fff`).

---

## Seq Log Viewer

### URL
**http://localhost:5341**

### Setup
```powershell
docker run -d --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq
```

### Features
- Real-time log streaming
- Full-text search across all properties
- Structured query language (signals/filters by property, level, time range)
- Dashboarding with charts and pinned queries
- Alerts when error rate exceeds threshold
- User management for team sharing

### Common Queries in Seq

| Query | What it does |
|---|---|
| `Level = "Error"` | All errors |
| `Application = "MarioGame" and Level = "Fatal"` | Only fatal crashes |
| `SourceContext like "PlayerController"` | All player-related logs |
| `@Duration > 1000` | Operations taking > 1 second |
| `Has:Exception` | All exceptions |
| `Id = 42` | Find specific player session |
| `@Timestamp > Now - 1h` | Last hour only |

### Seq API Key
Optional, set via env var for secured Seq instances:
```powershell
$env:MARIO_SEQ_KEY = "your-api-key-here"
```

---

## Grafana + Loki

### URLs

| Service | URL |
|---|---|
| Loki | `http://localhost:3100` |
| Grafana | `http://localhost:3000` |

### Setup

```powershell
# Start Loki
docker run -d --name loki -p 3100:3100 grafana/loki

# Start Grafana
docker run -d --name grafana -p 3000:3000 grafana/grafana
```

### Configure Grafana

1. Open http://localhost:3000 (login: admin/admin)
2. Go to **Configuration → Data Sources → Add data source**
3. Select **Loki**
4. Set URL to `http://localhost:3100`
5. Click **Save & Test**

### Loki LogQL Queries

| Query | What it does |
|---|---|
| `{app="MarioGame"} \|= "Error"` | All errors in MarioGame |
| `{app="MarioGame"} \|= "Player"` | All player-related logs |
| `rate({app="MarioGame"}[\5m])` | Log rate over 5 minutes |
| `{app="MarioGame"} \| json \| Level = "Fatal"` | Structured property filter |

### Sample Dashboard Panel (JSON)

```json
{
  "title": "Error Rate",
  "expr": "rate({app=\"MarioGame\"}[5m])",
  "type": "graph",
  "description": "Log error rate per 5 minutes"
}
```

---

## In-Game Log Viewer (F4)

Press **F4** during gameplay to toggle the overlay:

```
┌─────────────────────────────────────────────┐
│ [VRB] [DBG] [INF] [WRN] [ERR] [FTL]  [✕]  │
├─────────────────────────────────────────────┤
│ 14:30:00.123 [INF] PlayerController: ...    │
│ 14:30:00.456 [WRN] AssetManager: ...        │
│ 14:30:01.789 [ERR] PhysicsSystem: ...       │
│ ... (last 50 messages)                       │
├─────────────────────────────────────────────┤
│ 🔍 Filter...                                 │
└─────────────────────────────────────────────┘
```

Features:
- Color-coded by level (matches console colors)
- Filter bar to search within captured logs
- Level tabs to show/hide specific levels
- Copy button per line (click to copy raw JSON)
- Auto-scroll to latest

Not implemented yet — see task `tasks/12-ui-menus/961-implement-...`.

---

## Environment Variables

| Variable | Required | Default | Purpose |
|---|---|---|---|
| `MARIO_SEQ_KEY` | No | (none) | Seq API key for authenticated instances |
| `DOTNET_ENVIRONMENT` | No | `Development` | Environment label in log properties |
| `MARIO_LOKI_URL` | No | `http://localhost:3100` | Override Loki endpoint |

---

## Best Practices

### DO
```csharp
// Use structured properties
_logger.LogInformation("Player {Id} entered level {Level}", playerId, levelName);

// Log at appropriate level
_logger.LogDebug("Physics bodies: {Count}", physicsBodyCount);

// Include exception as first arg after message
_logger.LogError(ex, "Failed to load texture {Path}", texturePath);

// Use source-generated logging for hot paths
[LoggerMessage(Level = LogLevel.Debug, Message = "Player position: {Position}")]
partial void LogPlayerPosition(Vector2 position);
```

### DON'T
```csharp
// No string interpolation
_logger.LogInformation($"Player {x} scored");  // DON'T

// No Console.WriteLine
Console.WriteLine("hello");  // DON'T — banned by analyzer

// No catching and logging without rethrow (unless handling)
catch (Exception ex) { _logger.LogError(ex, "error"); throw; }  // DON'T — rethrow loses stack
catch (Exception ex) { _logger.LogError(ex, "error"); }  // OK if truly handled

// No logging every frame (rate-limit)
_logger.LogVerbose("Frame {Frame}", frame++);  // DON'T — 60 logs/second is too much
```

### Rate Limiting
For high-frequency logging (physics, rendering), rate-limit to 1 Hz:
```csharp
private int _logCounter;
private const int LogIntervalFrames = 60;

public void Update()
{
    if (++_logCounter % LogIntervalFrames == 0)
        _logger.LogDebug("Physics bodies: {Count}", count);
}
```

---

## Logging in Tests

```csharp
// Tests capture logs via ITestOutputHelper
public class JumpTests
{
    private readonly ITestOutputHelper _output;

    public JumpTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Jump_Arc_MatchesExpected()
    {
        using var log = new LoggerConfiguration()
            .WriteTo.TestOutput(_output)
            .CreateLogger();

        // Test code...
    }
}
```

---

## Crash Dumps

On unhandled exception:
1. Log is flushed immediately (all sinks)
2. Crash dump written to `crashes/crash-20260601-143000.dmp`
3. Error dialog shown to user with option to restart level
4. All recent logs (last 1000 lines) embedded in crash report

---

## Summary

```
                     ┌──────────┐
                     │  Game    │
                     │  Code    │
                     └────┬─────┘
                          │ ILogger<T>
                          ▼
                     ┌──────────┐
                     │ Serilog  │
                     └────┬─────┘
                          │
          ┌───────────────┼───────────────────┐
          │               │                   │
          ▼               ▼                   ▼
    ┌─────────┐    ┌──────────┐    ┌──────────────────┐
    │ Console │    │   File   │    │    Seq/Loki      │
    │  stdout │    │  .log    │    │  (Docker)        │
    └─────────┘    └──────────┘    └──────────────────┘
                              Grafana dashboards
                              http://localhost:3000
```

Quick commands:
```powershell
# Start Seq
docker run -d --name seq -e ACCEPT_EULA=Y -p 5341:80 datalust/seq

# Start Loki + Grafana
docker run -d --name loki -p 3100:3100 grafana/loki
docker run -d --name grafana -p 3000:3000 grafana/grafana

# View logs live
Get-Content logs/mario-*.log -Wait

# Search logs for errors
Select-String "ERR" logs/mario-*.log
```
