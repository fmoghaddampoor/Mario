# Task 037 — Create Renderer2D

## Description
Implement high-level 2D renderer with camera and effects.

## Steps
1. Create Renderer2D class wrapping SpriteBatcher
2. Add DrawTexture(Texture, pos, rot, scale, color, layer, flip)
3. Add DrawString(string, font, pos, color) for text
4. Add DrawRect(Rectangle, color, filled) for debug
5. Add DrawLine(Vector2, Vector2, color) for debug
6. Integrate with Camera for view/projection matrix
7. Implement begin/end pattern with auto-flush on end

## Files to Create
- src/MarioEngine.Core/Graphics/Renderer2D.cs
- src/MarioEngine.Core/Graphics/Camera2D.cs

## Acceptance Criteria
- Textures render at correct position/rotation/scale
- Camera movement affects rendered view
- Strings render with bitmap font
- Debug shapes display correctly
