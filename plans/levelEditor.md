# Level Editor — Design & Specification

## Overview
Built-in level editor for designing and testing levels. Integrated into the game engine, not a separate tool. Uses Silk.NET + ImGui for UI overlay.

---

## Editor Modes

### Paint Mode
Place tiles by clicking/dragging on a tile grid. Left-click places tile, right-click removes tile. Tile palette on left panel.

### Entity Mode
Place enemies, items, and interactive objects. Select entity from palette, click to place. Click placed entity to edit properties.

### Zone Mode
Define zones: spawn point, checkpoint, goal, secret exit, warp trigger. Zones are invisible in-game but shown as colored overlays in editor.

### Test Mode
Play the level from the current spawn point. Press Escape to return to editor. All editor tools hidden, full game rendering.

---

## Editor UI Layout

```
┌────────┬──────────────────────────┬─────────┐
│ TOOLBAR │     CANVAS (Tile Grid)     │ PROPERTIES│
│ ─────── │                            │ ──────── │
│ Select  │   [Zoomable, pannable]     │ Tile:    │
│ Paint   │                            │ Grass    │
│ Entity  │                            │ Variant: │
│ Zone    │                            │ Type A   │
│ Test    │                            │ Layer:   │
│ ─────── │                            │ Ground   │
│ Undo    │                            │ ──────── │
│ Redo    │                            │ Entity:  │
│ Save    │                            │ Goomba   │
│ Load    │                            │ Speed: 40│
│ ─────── │                            │ Patrol:  │
│ Tools   │                            │ ──────── │
│ Grid    │                            │ Zone:    │
│ Snap    │                            │ Spawn    │
└────────┴──────────────────────────┘─────────┘
```

---

## Tile Painting

### Tile Layers

| Layer | Z-Order | Purpose | Collision |
|---|---|---|---|
| Background | 0 | Decorative sky/backdrop tiles | None |
| Ground | 1 | Solid terrain, walkable surfaces | Solid |
| Platform | 2 | One-way platforms, floating blocks | From above only |
| Decorations | 3 | Grass tufts, pebbles, wall cracks | None |
| Foreground | 4 | Overlay elements (vines, fog) | None |

### Painting Tools

| Tool | Description |
|---|---|
| Brush | Paint single tiles (left-click place, right-click erase) |
| Rectangle | Click-drag to fill rectangular area |
| Line | Click-drag to paint a straight line of tiles |
| Fill | Flood-fill connected same-tile area |
| Eyedropper | Click tile to select same tile type |
| Eraser | Remove tiles |

### Tile Properties (per tile)

| Property | Options |
|---|---|
| Type | Floor, brick, ? block, pipe, etc. |
| Variant | Different visual variant (cracked, mossy, etc.) |
| Rotation | 0°, 90°, 180°, 270° |
| Flip | Horizontal, vertical |
| Slope angle | None, 15°, 30°, 45°, 60° |
| Contents (if ? block) | Mushroom, coin, flower, star, vine, empty |
| Coin count (if coin block) | 3-10 |

---

## Entity Placement

### Entity Palette

| Category | Entities |
|---|---|
| Enemies | Goomba, Koopa, Piranha Plant, Paratroopa, Buzzy Beetle, Spiny, Lakitu, Cheep Cheep, Blooper, Monty Mole, Wiggler, Dry Bones, Hammer Bro, Boo, Bowser |
| Items | Mushroom, 1-Up, Fire Flower, Starman, Coin, Star Coin, Vine, Shell |
| Power-ups | Super Mushroom, Mega Mushroom, Mini Mushroom, Propeller, Penguin, Bee, Boo, Gold |
| Blocks | ? block, Brick, Hidden, Coin block, Note block, Donut block, Falling platform, Ice block, Hard block |

### Entity Properties (varies by type)

**Goomba**:
```
Speed: [40]
Patrol: [Both directions ▼]
Start direction: [Left ▼]
AI: [Normal ▼] (Normal, Para (wings), Micro)
```

**Koopa**:
```
Speed: [50]
Patrol: [Both directions ▼]
Shell on stomp: [Yes]
Start direction: [Left ▼]
Variant: [Green ▼] (Green, Red)
```

**Piranha Plant**:
```
Pipe direction: [Up ▼]
Emerge time: [2.0s]
Wait time: [2.0s]
Variant: [Normal ▼] (Normal, Venus Fire)
```

**Pipe**:
```
Direction: [Enters from top ▼]
Destination: [Same level ▼]
Target: [x: 0, y: 0]
Target level: [1-1 ▼]
```

**Spawn Point**:
```
Start direction: [Right ▼]
```

**Checkpoint**:
```
Flag direction: [Up ▼]
```

---

## Zone Editing

### Zone Types

| Zone | Color Overlay | Properties |
|---|---|---|
| Spawn | Green | Direction, power-up override |
| Checkpoint | Blue | None |
| Goal / Flagpole | Gold | Height, bonus |
| Secret exit | Purple | Destination level, exit type |
| Warp trigger | Cyan | Destination level, entrance position |
| Camera boundary | Light blue | Disable scroll beyond this line |
| Kill plane | Red | Death below this Y value |
| Water zone | Light blue | Water level, surface Y |
| No-enemy zone | Orange | Enemies despawn within this area |

---

## Level Properties

### Level Settings Panel

```
Level ID: [1-1]
Level Name: [Grassland Hop]
World: [1 ▼]
Theme: [Grassland ▼]

Dimensions:
  Width: [200] tiles
  Height: [15] tiles

Gameplay:
  Time Limit: [300] seconds (0 = no timer)
  Auto-scroll: [Off]
  Underwater: [Off]
  Ghost House: [Off]
  Music Track: [World 1 Theme ▼]

Visual:
  Background: [Grassland Sky ▼]
  Ambient Color: [██████]
  Fog: [Off ▼]
  Parallax: [Default]

Difficulty Rating: ★★★☆☆ (auto-calculated based on entities + hazards)
```

---

## Test Mode

- Press F5 or click Test button to enter play mode
- All editor UI hidden
- Full game rendering with physics, AI, collisions
- Spawn at current spawn point (or mouse cursor position if "Test from cursor" enabled)
- Press Escape to return to editor
- Hot-reload: edit tiles while paused, see changes on resume

### Test Controls

| Key | Action |
|---|---|
| F5 | Enter/exit test mode |
| F6 | Test from cursor position |
| F7 | Toggle slow motion (0.5x speed) |
| F8 | Toggle physics debug overlay |
| F9 | Toggle entity hitbox display |
| Ctrl+Z | Undo last edit (in editor) |
| Ctrl+S | Save level |
| Ctrl+Shift+S | Save as... |
| Ctrl+T | Test from start |

---

## Validation & Error Checking

On save, editor checks:

| Check | Error | Warning |
|---|---|---|
| Spawn point exists | ❌ | |
| Goal exists | ❌ | |
| Player can reach goal | | ⚠️ |
| All pipes have destination | ❌ | |
| All warp zones have destination | ❌ | |
| No floating tiles (unsupported) | | ⚠️ |
| At least 10 coins | | ⚠️ |
| At least 5 enemies | | ⚠️ |
| Boss present in castle level | ❌ | |
| Checkpoint before boss room | | ⚠️ |
| No unreachable Star Coins | | ⚠️ |
| No softlocks | | ⚠️ |

---

## Shortcuts

| Shortcut | Action |
|---|---|
| Ctrl+Z | Undo |
| Ctrl+Y | Redo |
| Ctrl+S | Save |
| Ctrl+O | Open level |
| Ctrl+N | New level |
| Ctrl+Shift+N | New level from template |
| F5 | Test |
| F6 | Test from cursor |
| G | Toggle grid |
| H | Toggle hidden block visibility |
| 1-5 | Switch layer |
| B | Brush tool |
| E | Eraser |
| I | Eyedropper |
| R | Rectangle fill |
| L | Line tool |
| Del | Delete selected entity |
| Mouse wheel | Zoom in/out |
| Middle mouse drag | Pan canvas |
| Space + drag | Pan canvas |

---

## File Format

Output: `.mario` JSON file (see `levelsAndWorldMap.md` for schema).

Auto-backup: `.mario.bak` created on each save (last 5 retained).

---

## Editor Requirements

| Requirement | Minimum |
|---|---|
| Resolution | 1280 x 720 (editor), 1920 x 1080 (recommended) |
| Input | Mouse + Keyboard required |
| Performance | Same as game (editor uses same renderer) |
| Multi-window | No (editor + game view in same window) |
| Localization | Editor UI in English only (v1.0) |
