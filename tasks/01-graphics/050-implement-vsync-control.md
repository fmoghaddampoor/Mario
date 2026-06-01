# Task 050 — Implement VSync Control

## Description
Add runtime vsync toggle and frame rate limiting.

## Steps
1. Query monitor refresh rate
2. Implement vsync on/off toggle via OpenGL swap interval
3. Implement FPS cap (30, 60, 120, unlimited)
4. Add vsync toggle to settings
5. Add FPS cap to settings
6. Save preference to config
7. Add option for adaptive vsync if available

## Files to Modify
- Silk.NET window swap interval
- Config system

## Acceptance Criteria
- VSync on/off works at runtime
- FPS cap limits frame rate correctly
- Settings persist between sessions
- No screen tearing with vsync on
