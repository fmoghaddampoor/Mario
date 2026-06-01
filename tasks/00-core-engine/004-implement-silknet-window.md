# Task 004 — Implement Silk.NET Window

## Description
Create the window and OpenGL context using Silk.NET.

## Steps
1. Add Silk.NET NuGet packages: Silk.NET, Silk.NET.Windowing, Silk.NET.OpenGL
2. Create MarioWindow class wrapping Silk.NET's Window
3. Configure window: title "Super Mario", size 1280x720, resizable
4. Set up OpenGL context with version 4.6
5. Wire window events: OnLoad, OnUpdate, OnRender, OnClose, OnResize, OnFramebufferResize
6. Implement vsync toggle
7. Handle window close gracefully

## Files to Create
- src/MarioEngine.Desktop/MarioWindow.cs
- src/MarioEngine.Desktop/Program.cs (entry point)

## Acceptance Criteria
- Window opens at 1280x720 with correct title
- OpenGL 4.6 context is created
- Resize works, framebuffer resizes
- Close with X button works
- --fullscreen flag starts in fullscreen mode
