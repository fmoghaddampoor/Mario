# Task 036 — Create Sprite Batcher

## Description
Implement a batched sprite renderer to minimize draw calls.

## Steps
1. Create SpriteBatcher class in Graphics namespace
2. Implement vertex buffer (dynamic VBO) for batched quads
3. Implement index buffer for 6 indices per quad
4. Add Draw(Texture, Rectangle, Color, float layer) method
5. Sort by texture then layer to minimize state changes
6. Flush on texture change or at end of frame
7. Support 500+ sprites per batch (≤100 draw calls)

## Files to Create
- src/MarioEngine.Core/Graphics/SpriteBatcher.cs
- src/MarioEngine.Core/Graphics/VertexData.cs

## Acceptance Criteria
- 500 sprites render in ≤ 100 draw calls
- Sprites render in correct layer order
- Texture changes trigger flush
- No GL errors
