# Task 003 — Create Game Loop

## Description
Implement the core game loop: init, update, render, shutdown.

## Steps
1. Create Game class in MarioEngine.Core
2. Add Run() method with loop: ProcessInput → Update → Render
3. Implement fixed timestep (60 FPS = 16.67ms) with delta time
4. Add Initialize(), LoadContent(), UnloadContent() lifecycle hooks
5. Add IsRunning flag for clean shutdown
6. Wire up Silk.NET window for OpenGL context
7. Measure frame time and expose Time.DeltaTime

## Files to Create
- src/MarioEngine.Core/Game.cs
- src/MarioEngine.Core/Time.cs

## Acceptance Criteria
- Game loop runs at 60 FPS
- Window appears with OpenGL context
- Time.DeltaTime returns correct values
- Close button exits cleanly
