# Art Guide — Super Mario Game

## Visual Style
High-quality modern 2D art with a hand-drawn/painted aesthetic. Inspired by modern indie platformers (Ori, Rayman Legends, Cuphead) — rich colors, smooth animations, and detailed backgrounds. No pixel art.

---

## Resolution & Canvas

| Property | Value |
|---|---|
| Render resolution | 1920 x 1080 (native) |
| Sprite resolution | Variable, typically 128–256 px per object |
| Tile size | 64 x 64 px |
| UI scale | 1920x1080 canvas with safe-zone margins |
| Aspect ratio | 16:9 (letterbox on ultrawide) |

Scaling mode: **Bilinear filtering** (no nearest-neighbor). All sprites authored at high resolution and downscaled if needed.

---

## Character Art

### Mario
- Full body with articulated limbs (no chibi/proportioned like modern 3D Mario)
- Smooth frame-by-frame animation: idle, walk, run, jump, fall, crouch, slide, swim, death
- Power-up variants: Super (larger), Fire (white gloves/accents), Star (shimmer effect)
- Face rig with expressive eyes and mouth for reactions

### Enemies (20+ types)
Each enemy has:
- Idle, alert, attack, hurt, death animations
- Unique silhouette for instant readability
- Color palette distinct from background

### Bosses (10 unique)
- Multi-phase designs with visual transformation
- Each boss fills ~25–50% of screen height
- Elaborate death animation

---

## World & Backgrounds

### 10 Worlds, each with unique biome:

| World | Theme | Palette Key |
|---|---|---|
| 1 | Grassland | Warm greens, blue sky, brown earth |
| 2 | Underground / Cave | Deep blues, purples, glowing orange |
| 3 | Desert | Sandy yellows, terracotta, hot pink sky |
| 4 | Ocean / Beach | Aquamarine, coral, white foam |
| 5 | Forest | Deep greens, golden sunlight shafts |
| 6 | Ice / Mountain | Cyan, white, icy blue shadows |
| 7 | Volcano / Lava | Red, orange, black, glowing yellow |
| 8 | Sky / Cloud | Soft blues, white, gold highlights |
| 9 | Ghost / Haunted | Purple, grey, neon green glows |
| 10 | Final Castle | Dark crimson, black, gold trim |

### Background Layers
- **Far background**: Static painted skybox / panorama with parallax (0.1x)
- **Mid background**: Silhouetted terrain, distant buildings, trees (0.3x)
- **Near background**: Detailed elements just behind gameplay (0.6x)
- **Foreground**: Atmospheric overlays (leaves, dust, light rays) in front of gameplay

---

## Tiles & Level Art

### Tile Categories
- **Ground/Solid**: Natural terrain (dirt, stone, sand, ice, etc.)
- **Platforms**: Floating blocks, clouds, crumbling ledges
- **Decorative**: Grass tufts, pebbles, vines, wall cracks (non-collision)
- **Blocks**: Question block (animated ?, glowing), brick (crack variants), hidden (faded)
- **Pipes**: Modular pipe segments (entrance, body, bend, exit)

Each tile set has ~20–40 unique tiles per world. Tiles designed to tile seamlessly.

---

## UI & HUD

### In-Game HUD
- Top-left: Coin counter, score, lives
- Top-right: Timer
- Bottom-left: Power-up indicator (current power-up icon)
- Clean, minimal, semi-transparent background
- Custom-drawn UI (no system fonts) — hand-crafted numbers and icons

### Menus
- Title screen: Full-bleed painted illustration with parallax
- World map: Painted top-down map with animated character marker
- Pause menu: Blurred background overlay
- Settings: Sliders, toggle switches, all custom-styled

---

## Effects & Particles

### Visual Effects
| Effect | Technique |
|---|---|
| Fireball | Glowing sprite + additive bloom + trailing particles |
| Star invincibility | Rainbow tint shader + sparkle particles |
| Stomp enemy | Squash animation + dust poof + starburst |
| Coin block | Bounce animation + coin shoot-out + sparkle |
| Death | Flash white + dissolve + particle burst |
| Checkpoint flag | Flowing fabric animation + glow pulse |
| Level clear | Fireworks particle system |

### Lighting
- Pre-baked ambient lighting per level
- Dynamic point lights for fire, lava, glowing blocks
- Shadow sprites under characters and platforms (soft ellipses)
- No real-time shadow maps — all fake 2D occlusion

---

## Animation Pipeline

### Tools
- **Authoring**: Spine (skeletal animation) or Spritesheet (frame-by-frame)
- **Export**: PNG sequences + JSON metadata
- **In-engine**: Custom Animator component
  - Each animation is a list of frames with duration
  - Supports: looping, one-shot, crossfade between animations
  - Blend trees for directional/movement blending

### Animation Detail
- Mario idle: breathing, slight bounce, look-around
- Mario run: full body motion, cape physics, head bob
- Jump: anticipation squat → stretch ascent → tuck mid-air → land impact
- Enemy idle: Goomba wobbles, Koopa taps foot, Piranha Plant bobs
- Death animations: elaborate, 1–2 seconds, cannot skip

---

## Asset Pipeline

### Workflow
```
Concept Sketch → Line Art → Color Flat → Shading → Final Sprite
                                    ↓
                         Spine Rig / Spritesheet
                                    ↓
                         Texture Atlas Packing
                                    ↓
                         Import to Engine
```

### File Naming Convention
- Sprites: `player_mario_idle_001.png`, `enemy_goomba_walk_001.png`
- Atlases: `world_1_grassland_atlas.json`
- Backgrounds: `bg_world_1_far.png`, `bg_world_1_mid.png`
- UI: `ui_icon_coin.png`, `ui_btn_start.png`

### Quality Checklist (per asset)
- [ ] Correct resolution (128/256/512 px standard)
- [ ] Consistent palette with world theme
- [ ] Seamless tile edges
- [ ] Animation is smooth (no skipped frames)
- [ ] Silhouette readable at game distance
- [ ] No compression artifacts

---

## Technical Constraints for Artists

- Work at 2x or 3x final render size (downscale gives crisp edges)
- Export as PNG with transparent background
- Keep separate layers for character and props (for animation rigging)
- Max texture atlas size: 4096 x 4096 (GPU limit)
- Color space: sRGB
- Max animation frames: 30 fps equivalent (but engine interpolates)
