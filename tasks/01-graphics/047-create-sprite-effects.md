# Task 047 — Create Sprite Effects

## Description
Implement common sprite effects: flash, tint, dissolve, shimmer.

## Steps
1. Create SpriteEffect base class
2. Implement FlashEffect (white flash on damage)
3. Implement TintEffect (color overlay during power-up)
4. Implement DissolveEffect (death animation dissolve)
5. Implement ShimmerEffect (Starman rainbow)
6. Implement PulseEffect (block idle animation)
7. Support effect stacking with alpha blending
8. All effects implemented as shader uniforms + C# controllers

## Files to Create
- src/MarioEngine.Core/Graphics/Effects/SpriteEffect.cs
- src/MarioEngine.Core/Graphics/Effects/FlashEffect.cs
- src/MarioEngine.Core/Graphics/Effects/TintEffect.cs
- src/MarioEngine.Core/Graphics/Effects/DissolveEffect.cs
- src/MarioEngine.Core/Graphics/Effects/ShimmerEffect.cs
- src/MarioEngine.Core/Graphics/Effects/PulseEffect.cs
- src/MarioEngine.Core/Graphics/Shaders/effect.vert
- src/MarioEngine.Core/Graphics/Shaders/effect.frag

## Acceptance Criteria
- Flash effect shows white overlay for 0.1s
- Dissolve effect fades sprite to transparency
- Shimmer cycles through rainbow colors
- Effects stack and combine correctly
- No visible artifacts at edges
