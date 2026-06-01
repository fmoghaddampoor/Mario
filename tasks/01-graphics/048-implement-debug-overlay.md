# Task 048 — Implement Debug Overlay

## Description
Create an in-game debug overlay for performance metrics.

## Steps
1. Create DebugOverlay renderer
2. Display: FPS, frame time, draw calls, physics objects, memory, audio channels
3. Color-code values: green (OK), yellow (warning), red (critical)
4. Toggle with F3 key
5. Show graph for frame time history (last 60 frames)
6. Show entity count per scene
7. Log to console on F3 toggle

## Files to Create
- src/MarioEngine.Core/Debug/DebugOverlay.cs
- src/MarioEngine.Core/Debug/PerformanceMetrics.cs

## Acceptance Criteria
- Overlay appears on F3 press
- FPS updates in real-time
- Values color-coded correctly
- Frame time graph shows history
- No performance impact when hidden
