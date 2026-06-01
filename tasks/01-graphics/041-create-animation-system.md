# Task 041 — Create Animation System

## Description
Implement frame-based animation controller.

## Steps
1. Create AnimationFrame struct: texture rect, duration
2. Create Animation class: list of frames, loop flag, name
3. Create Animator component: plays animations, crossfades
4. Support: Play(name), Stop(), Pause(), Resume()
5. Support animation events (callbacks at specific frames)
6. Support blend/crossfade between animations (0.1s)
7. Support animation speed multiplier
8. Load animations from JSON definition files

## Files to Create
- src/MarioEngine.Core/Graphics/Animation/AnimationFrame.cs
- src/MarioEngine.Core/Graphics/Animation/Animation.cs
- src/MarioEngine.Core/Graphics/Animation/Animator.cs
- src/MarioEngine.Core/Graphics/Animation/AnimationDefinition.cs

## Acceptance Criteria
- Animations play correctly from frame list
- Crossfade between animations works smoothly
- Animation events fire at correct frames
- Speed multiplier works (0.5x, 2x, etc.)
- Loading from JSON works
