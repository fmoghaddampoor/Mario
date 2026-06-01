# Task 045 — Create Render Layers

## Description
Implement a layered render system with correct Z-ordering.

## Steps
1. Create RenderLayer enum with ordered values
2. Layers: Background, FarParallax, MidParallax, NearParallax, Ground, Entities, Foreground, Particles, UI
3. Implement layer-based sorting in SpriteBatcher
4. Support per-layer parallax factors
5. Support per-layer visibility toggle (for debug)
6. Implement layer offset for Y-sorting within entity layer
7. Add debug overlay showing layer counts

## Files to Create
- src/MarioEngine.Core/Graphics/RenderLayer.cs
- src/MarioEngine.Core/Graphics/LayerManager.cs

## Acceptance Criteria
- Sprites render in correct layer order
- Parallax factors applied per layer
- Debug overlay shows sprite count per layer
- No sorting overhead when layer count is small
