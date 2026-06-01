# Task 042 — Create Particle System

## Description
Implement a GPU-friendly particle system.

## Steps
1. Create Particle struct: position, velocity, color, size, lifetime
2. Create ParticleSystem class with particle pool
3. Support emission patterns: point, circle, rectangle, line
4. Support particle modifiers: gravity, color over lifetime, size over lifetime
5. Support one-shot and looping emitters
6. Implement particle sorting (back-to-front for transparency)
7. Batch all particles in single draw call
8. Create ParticleEffect presets: dust, sparkle, fire, smoke, explosion

## Files to Create
- src/MarioEngine.Core/Graphics/Particles/Particle.cs
- src/MarioEngine.Core/Graphics/Particles/ParticleSystem.cs
- src/MarioEngine.Core/Graphics/Particles/ParticleEmitter.cs
- src/MarioEngine.Core/Graphics/Particles/ParticleEffect.cs

## Acceptance Criteria
- 1000 particles render at 60 FPS
- Particles fade and shrink over lifetime
- Emitter presets produce correct visual effects
- Pool recycles dead particles
- No per-frame allocations
