# Task E003 — Set Up Serilog Logging with Seq + Grafana Loki

## Description
Set up structured logging pipeline with 4 simultaneous sinks.

## Why This Was Done Ahead of Task List
Logging is foundational — every system depends on it. Setting it up early prevents rewriting log calls later.

## What Was Created
- `LogConfiguration` — configures Serilog with Console, File, Seq, Loki sinks
- Console: colored output with `HH:mm:ss.fff` timestamps
- File: daily rolling logs at `logs/mario-YYYY-MM-DD.log`, 14-day retention, full datetime stamps
- Seq: structured log viewer at `http://localhost:5341` (docker run datalust/seq)
- Loki: Grafana Loki sink at `http://localhost:3100` for Grafana dashboards
- Environment enrichment: MachineName, ThreadId, Application, Environment
- Env var support: `MARIO_SEQ_KEY`, `DOTNET_ENVIRONMENT`

## Packages Added
| Package | Version |
|---|---|
| Serilog | 4.2.0 |
| Serilog.Sinks.Console | 6.0.0 |
| Serilog.Sinks.File | 6.0.0 |
| Serilog.Sinks.Seq | 9.0.0 |
| Serilog.Sinks.Grafana.Loki | 8.3.0 |
| Serilog.Extensions.Logging | 9.0.1 |
| Serilog.Enrichers.Environment | 3.0.1 |
| Serilog.Enrichers.Thread | 4.0.0 |

## Files
- `src/MarioEngine.Core/Logging/LogConfiguration.cs`
- `plans/loggingSystem.md` (full user guide)

## Acceptance Criteria
- Game logs to console, file, Seq, and Loki simultaneously
- Seq dashboard accessible at localhost:5341
- Log files contain full datetime: `2026-06-01 14:30:00.123`
- `dotnet build` succeeds with 0 warnings, 0 errors
