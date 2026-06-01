# Task 038 — Create Texture Manager

## Description
Implement texture loading, caching, and atlas management.

## Steps
1. Create Texture2D class wrapping OpenGL texture handle
2. Create TextureManager singleton with load/unload/cache
3. Support loading PNG via StbImageSharp
4. Generate mipmaps on load
5. Support texture atlases (load from JSON metadata)
6. Implement texture reference counting
7. Implement async loading on background thread

## Files to Create
- src/MarioEngine.Core/Graphics/Texture2D.cs
- src/MarioEngine.Core/Graphics/TextureManager.cs
- src/MarioEngine.Core/Graphics/TextureAtlas.cs

## Acceptance Criteria
- PNG files load and display correctly
- Mipmaps generate without errors
- Texture atlas loads with correct sub-rectangles
- Multiple loads of same texture return cached instance
- Async load doesn't block game thread
