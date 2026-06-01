# Task 049 — Implement Fullscreen Toggle

## Description
Add runtime fullscreen toggle and resolution switching.

## Steps
1. Implement Alt+Enter fullscreen toggle
2. Support windowed, borderless, and exclusive fullscreen modes
3. Query available resolutions from monitor
4. Implement resolution switching in settings
5. Preserve aspect ratio on resize
6. Save fullscreen preference to config
7. Support startup command-line flags

## Files to Modify
- src/MarioEngine.Desktop/MarioWindow.cs
- Config system

## Acceptance Criteria
- Alt+Enter toggles fullscreen
- Resolution changes apply without restart
- Fullscreen preference persists
- Aspect ratio maintained (letterbox on ultra-wide)
