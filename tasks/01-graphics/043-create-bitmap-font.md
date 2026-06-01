# Task 043 — Create Bitmap Font Renderer

## Description
Implement bitmap font rendering for UI text.

## Steps
1. Create BitmapFont class loading font from texture atlas + JSON
2. Support ASCII character set (32-127)
3. Support kerning pairs from font definition
4. Implement text wrapping at max width
5. Implement text alignment: left, center, right
6. Support color tinting
7. Support drop shadow (offset + color)
8. Cache text measurements for layout calculations

## Files to Create
- src/MarioEngine.Core/Graphics/Font/BitmapFont.cs
- src/MarioEngine.Core/Graphics/Font/FontCharacter.cs
- src/MarioEngine.Core/Graphics/Font/TextLayout.cs

## Acceptance Criteria
- Text renders correctly at specified position
- Kerning pairs are applied
- Text wraps at specified width
- Alignment works (left, center, right)
- Color tint and drop shadow work
