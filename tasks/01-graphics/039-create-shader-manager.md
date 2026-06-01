# Task 039 — Create Shader Manager

## Description
Implement shader compilation, caching, and management.

## Steps
1. Create Shader class wrapping GL program handle
2. Support vertex + fragment shader from source files
3. Create ShaderManager for loading and caching shaders
4. Implement default shader: vertex transform + texture fragment
5. Support uniform setting (mat4, vec2, vec4, float, int, bool)
6. Add shader hot-reload (F5 reloads source files)
7. Log shader compilation errors

## Files to Create
- src/MarioEngine.Core/Graphics/Shader.cs
- src/MarioEngine.Core/Graphics/ShaderManager.cs
- src/MarioEngine.Core/Graphics/Shaders/default.vert
- src/MarioEngine.Core/Graphics/Shaders/default.frag

## Acceptance Criteria
- Default shader renders textured quads
- Shader compilation errors are logged with source line
- Uniforms set correctly
- Hot-reload works without restart
