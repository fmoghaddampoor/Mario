# Game Engine Architecture

## Overview
Custom 2D game engine built in C# on top of Silk.NET (OpenGL), targeting cross-platform desktop (Windows, macOS, Linux). Designed specifically for a high-quality 2D side-scrolling platformer.

---

## Rendering Pipeline (Silk.NET / OpenGL)

### Core Layers
| Layer | Responsibility |
|---|---|
| **Window / Context** | Silk.NET's Windowing (GLFW under the hood) — window creation, input, vsync |
| **OpenGL Bindings** | Silk.NET.OpenGL — direct OpenGL 4.6+ API calls |
| **Sprite Batcher** | Custom batcher — groups sprites by texture to minimize draw calls |
| **Renderer2D** | High-level API: `Draw(texture, position, rotation, scale, color, layer)` |
| **Camera** | 2D camera with smooth follow, parallax scrolling, shake effects |

### Render Flow (per frame)
1. Clear framebuffer
2. Camera computes view/projection matrix
3. Scene culls off-screen entities
4. Sprite batcher collects visible draw calls, sorted by layer
5. Batcher flushes — single `glDrawElements` per texture atlas
6. Particle system overlay
7. Post-processing (optional: bloom for power-ups, fade transitions)
8. Swap buffers

### Textures & Atlases
- High-res sprites packed into texture atlases (via build-time tool)
- Supported formats: PNG (import), compressed to DXT5/BC3 at runtime
- Mipmaps generated for smooth downscaling

---

## Scene / Entity System

### ECS-Lite
Not a full ECS — simpler **Scene → Entity → Component** hierarchy:

```
Scene
 ├── Camera
 ├── Background (parallax layers)
 ├── Entity
 │    ├── Transform (position, rotation, scale)
 │    ├── SpriteRenderer (texture, color, layer, flip)
 │    ├── Animator (frame-based animation controller)
 │    ├── RigidBody (velocity, gravity, collision shape)
 │    ├── Collider (AABB or polygon)
 │    ├── PlayerController / EnemyAI / etc.
 │    └── AudioSource (SFX triggers)
 └── UI Overlay (HUD, menus — rendered in screen space)
```

### Scene States
- **Loading** — stream assets, show progress bar
- **Playing** — game loop active
- **Paused** — freeze update, keep rendering
- **Transition** — fade in/out between levels

---

## Physics System

### Collision Detection
- **Broad phase**: Spatial hash grid (cell size = 64px)
- **Narrow phase**: AABB vs AABB, AABB vs tile map
- **Tile map collision**: Swept AABB against solid tiles (prevents tunneling)
- **Slopes**: Line-segment intersection for ramp tiles

### Movement
- Velocity-based with acceleration/friction
- Gravity: constant downward force
- Jump: variable-height (cut jump on release)
- Platformer-specific: coyote time, jump buffering, ledge grab

### Response
- Position correction (push out of overlap)
- Surface classification (ground, wall, ceiling, slope)
- One-way platforms (collide only from above)

---

## Audio System

### Architecture
- **Backend**: Silk.NET.OpenAL (cross-platform audio)
- **Decoding**: NAudio or NVorbis for MP3/WAV → PCM stream
- **Format**: WAV (SFX), MP3 (music tracks)

### Features
| Feature | Implementation |
|---|---|
| Music streaming | Decode MP3 in chunks, double-buffered OpenAL buffer queue |
| SFX pooling | Pre-load WAVs into OpenAL buffers, pool sources |
| 3D positioning | Mono SFX positioned in 2D world space (OpenAL distance model) |
| Bus system | Master → Music / SFX / Voice buses with volume sliders |
| Dynamic mixing | Duck SFX during dialogue, fade music on death |

### Music System
- Full orchestral / high-quality original soundtrack
- Crossfade between tracks (e.g. overworld → underground → boss)
- Layered stems: different instrument layers can fade in/out based on gameplay (e.g. add percussion when stomping enemies)

---

## Input System

- **Backend**: Silk.NET.Input (keyboard, mouse, gamepad)
- **Action mapping**: `InputAction` bound to keys/buttons (rebindable)
- **Actions**: `MoveLeft`, `MoveRight`, `Jump`, `Run`, `Crouch`, `Fire`, `Pause`
- **Controller**: Xbox/PS layout support with analog stick and D-pad

---

## Asset Pipeline

### Build-Time
```
Source Art (PSD/Aseprite/PNG)
       → Texture Packer (atlas + JSON metadata)
       → Custom Importer → .marioasset (binary, GPU-ready)

Source Audio (WAV/MP3)
       → MP3 compression for music (320 kbps)
       → WAV for SFX (uncompressed, instant decode)
```

### Runtime
- `AssetManager` singleton — loads, caches, and references-counts assets
- Loading screen with progress (track total asset count)
- Async loading on background thread (except GL resources)

---

## Level System

### Level Format
- Custom `.mario` file format (JSON for readability, compressed at runtime)
- Tile layers: ground, decorations, collision
- Entity spawns with properties
- Zones: level start, end flag, secret areas, warp pipes

### Level Editor
- Tiled-like in-editor tool (or custom editor using ImGui via Silk.NET)
- Place tiles, entities, zones visually
- Export → `.mario` level file

---

## Logging System

### Architecture

```
Game Code → ILogger<T> (abstraction)
              → Serilog (provider)
                   ├── Console Sink (colored, dev)
                   ├── File Sink (rolling daily, 14 days)
                   ├── Seq Sink (http://localhost:5341)
                   └── Grafana Loki Sink (http://localhost:3100)
```

### Sequence
1. `GameServiceProvider.CreateDefault()` sets up DI with Serilog logging
2. `LogConfiguration.Initialize()` configures sinks from startup parameters
3. All game systems receive `ILogger<T>` via constructor injection
4. Logs flow to console, file, Seq, and Loki simultaneously
5. F4 toggles in-game log overlay (last 50 messages)

### Structured Properties
Every log call uses named properties (not string concat) so Seq and Loki can index and search them:

```csharp
_logger.LogInformation("Player {Id} scored {Points} points", playerId, score);
// Queryable in Seq:  Id=42 AND Points>1000
```

### Seq Dashboard
Single Docker container — `docker run datalust/seq` — web UI at `http://localhost:5341`.

### Grafana + Loki
Requires Loki server + Promtail/Alloy agent. Grafana dashboard visualizes logs with full structured search.

---

## Build & Deployment

### Cross-Platform
| Platform | Approach |
|---|---|
| Windows | Native build via .NET 8, P/Invoke OpenGL |
| macOS | Same .NET 8 build, Metal translation layer via MoltenVK (OpenGL) |
| Linux | Same build, native OpenGL |

### Distribution
- Single executable + `assets/` folder
- Optional: native bundle tool (likedotnet publish)

---

## Key Dependencies

| Package | Purpose |
|---|---|---|
| `Silk.NET` | Windowing, OpenGL, Input, OpenAL |
| `NAudio` / `NVorbis` | MP3 decoding |
| `StbImageSharp` | Image loading (PNG → raw pixel data) |
| `System.Text.Json` | Asset metadata, level files |
| `ImGui.NET` (optional) | Editor tooling |
| `Microsoft.Extensions.DependencyInjection` | DI container |
| `Serilog` | Structured logging |
| `Serilog.Sinks.Seq` | Seq log viewer sink |
| `Serilog.Sinks.Grafana.Loki` | Grafana Loki sink |
| `Serilog.Enrichers.Environment` | Machine name, OS enrichment |
| `Serilog.Enrichers.Thread` | Thread ID enrichment |

---

## Performance Targets

- **60 FPS** locked, drops below 50 trigger quality reduction
- **Sprite batch**: ≤ 100 draw calls per frame
- **Physics**: 240 Hz fixed timestep (sub-stepped)
- **Memory**: < 512 MB RAM, < 200 MB VRAM
- **Load times**: < 3 seconds per level (SSD target)

---

## Folder Structure

```
MarioEngine/
 ├── Core/                 # Application loop, Game class, Time
 ├── Graphics/             # Renderer, SpriteBatcher, Camera, Shaders
 ├── Audio/                # AudioEngine, SoundEffect, MusicStream
 ├── Physics/              # Collision detection, RigidBody, TileMapCollider
 ├── Scene/                # Entity, Component, Scene, SceneManager
 ├── Assets/               # AssetManager, TextureCache, AudioCache
 ├── Input/                # InputManager, InputAction, RebindUI
 ├── UI/                   # HUD, Menu, Button, TextRenderer
 ├── Game/                 # Player, Enemies, PowerUps, Blocks, Items
 ├── Level/                # LevelLoader, TileMap, SpawnPoint
 ├── Logging/              # LogConfiguration, Serilog setup
 ├── DependencyInjection/  # GameServiceProvider, DI registration
 └── Tools/                # Level editor, Texture packer, Atlas builder
```
