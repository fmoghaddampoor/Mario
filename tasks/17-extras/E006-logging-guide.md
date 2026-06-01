# Task E006 — Create Logging System Guide

## Description
Write comprehensive documentation for the logging system.

## Why This Was Done Ahead of Task List
The logging system is complex (4 sinks, Seq, Loki, structured logging) and needed its own reference document.

## What Was Created
- `plans/loggingSystem.md` covering:
  - Architecture diagram
  - All 4 sinks with setup commands
  - Log file format and location
  - Log levels and thresholds
  - Structured logging rules (correct vs wrong examples)
  - Timestamp formats per sink
  - Seq query language reference
  - Grafana + Loki setup (Docker, data source, LogQL)
  - F4 in-game log viewer spec
  - Environment variables
  - Best practices and anti-patterns
  - Test logging integration
  - Crash dump process

## Files
- `plans/loggingSystem.md`

## Acceptance Criteria
- A developer can set up Seq/Loki/Grafana from the guide
- Structured logging rules are unambiguous
