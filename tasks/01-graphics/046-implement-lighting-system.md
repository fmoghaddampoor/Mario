# Task 046 — Implement Lighting System

## Description
Create a 2D lighting system with dynamic and baked lights.

## Steps
1. Create Light2D class: position, radius, color, intensity
2. Support dynamic point lights (player torch, fire, lava)
3. Support baked ambient light (per-level)
4. Implement additive blending for light sprites
5. Create shadow sprites (soft ellipses under characters)
6. Support light flicker animation
7. Limit to 16 dynamic lights max

## Files to Create
- src/MarioEngine.Core/Graphics/Lighting/Light2D.cs
- src/MarioEngine.Core/Graphics/Lighting/LightManager.cs

## Acceptance Criteria
- Point lights illuminate surrounding area
- Multiple lights blend correctly
- Light flicker animation looks natural
- Shadow sprites display under characters
- Performance stays at 60 FPS with 16 lights
