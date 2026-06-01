# Task 040 — Implement Camera2D

## Description
Implement a full-featured 2D camera system.

## Steps
1. Extend Camera2D class with position, zoom, rotation
2. Calculate view matrix from camera properties
3. Calculate projection matrix (orthographic, 1920x1080)
4. Support smooth follow: lerp to target position
5. Add camera dead zone (no movement within N pixels)
6. Add look-ahead (scroll ahead of player movement)
7. Implement parallax scaling (per-layer multipliers 0.1-1.0)
8. Add screen shake: random offset with decay
9. Support camera bounds (clamp to level boundaries)

## Files to Modify
- src/MarioEngine.Core/Graphics/Camera2D.cs

## Acceptance Criteria
- Camera follows player smoothly
- Parallax layers scroll at different rates
- Screen shake works with configurable intensity/duration
- Camera clamps to level boundaries
- Look-ahead anticipates player direction
