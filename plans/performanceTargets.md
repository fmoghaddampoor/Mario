# Performance Targets — Hardware & Optimization

## Overview
Target hardware specifications, performance budgets, and optimization strategy for a smooth 60 FPS experience across all target platforms.

---

## Target Hardware

### Minimum Specs (30 FPS, Low Settings)

| Component | Requirement |
|---|---|
| OS | Windows 10, macOS 11, Linux (Ubuntu 20.04+) |
| CPU | Intel Core i3-6100 / AMD Ryzen 3 1200 |
| GPU | Intel HD 630 / NVIDIA GT 730 / AMD Radeon R5 |
| RAM | 4 GB |
| VRAM | 512 MB |
| Storage | 2 GB available space |
| API | OpenGL 4.3+ support required |

### Recommended Specs (60 FPS, High Settings)

| Component | Requirement |
|---|---|
| OS | Windows 11, macOS 14, Linux (Ubuntu 22.04+) |
| CPU | Intel Core i5-8400 / AMD Ryzen 5 2600 |
| GPU | NVIDIA GTX 1060 / AMD RX 580 / Intel Arc A380 |
| RAM | 8 GB |
| VRAM | 2 GB |
| Storage | 2 GB SSD (for faster level loading) |
| API | OpenGL 4.6+ support |

---

## Performance Budgets

### Frame Budget (60 FPS target = 16.67ms per frame)

| System | Budget | % of frame |
|---|---|---|
| Rendering (GPU) | 6 ms | 36% |
| Physics | 3 ms | 18% |
| Game logic (AI, animation) | 2 ms | 12% |
| Audio | 1 ms | 6% |
| Asset loading (async) | 0 ms | 0% (background) |
| Buffer / overhead | 4.67 ms | 28% |

### Rendering Budgets

| Area | Target | Max |
|---|---|---|
| Draw calls per frame | ≤ 100 | 300 |
| Vertices per frame | ≤ 50,000 | 150,000 |
| Texture memory | 128 MB | 256 MB |
| Render targets | 2 (main + UI) | 4 |
| Post-processing passes | 1 (bloom) | 3 |
| Particle count | ≤ 500 | 2000 |
| Lights (2D) | ≤ 8 | 16 |

### Memory Budgets

| Area | Budget |
|---|---|
| Total RAM usage | ≤ 512 MB |
| VRAM usage | ≤ 200 MB |
| Textures (atlas) | 150 MB |
| Audio (SFX pre-loaded) | 20 MB (all SFX in RAM) |
| Audio (music streaming) | 2 MB buffer |
| Level data (loaded) | 5 MB per level |
| Save data (in memory) | 256 KB |
| Debug/editor tools | +100 MB (editor mode only) |

---

## Loading Targets

| Operation | Target | Max acceptable |
|---|---|---|
| Game boot (to title) | 3s | 5s |
| Level load (first time) | 2s | 4s |
| Level load (revisit, cached) | 0.5s | 1s |
| Level editor boot | 1s | 3s |
| Save game | 0.1s | 0.3s |
| Settings save | 0.05s | 0.1s |
| Asset reimport | 1s per 10 assets | 3s per 10 assets |

---

## Optimization Strategies

### Rendering
- **Texture atlasing**: All sprites packed into 4096x4096 atlases (≤2 per world)
- **Sprite batching**: Single draw call per layer per atlas (≤ 10 calls per frame)
- **Culling**: Frustum cull all sprites outside camera view + 1 tile margin
- **LOD**: No LOD for 2D sprites (same quality at any distance)
- **Post-processing**: Optional bloom only — disabled on low-spec hardware
- **VSync**: Optional, default on
- **Resolution scaling**: 1.0x, 0.75x, 0.5x options in settings

### Physics
- **Spatial hash**: 64px cell grid for broad-phase collision
- **Sleeping**: Static entities (tiles) never checked against each other
- **Island sleeping**: Off-screen physics bodies put to sleep
- **Fixed timestep**: 240Hz physics, decoupled from rendering

### Audio
- **Music streaming**: Decode MP3 in 4KB chunks, double-buffered
- **SFX pooling**: Pre-decode all SFX to PCM at boot, store in pooled OpenAL buffers
- **Channel limits**: Max 16 simultaneous SFX, 2 music streams
- **Priority**: Critical sounds (damage, death) always play — less important sounds steal channels

### Asset Management
- **Async loading**: All assets load on background thread (except GL resource creation)
- **Reference counting**: Assets unloaded when no scenes reference them
- **Level streaming**: Levels load incrementally — assets needed for first section load first
- **Atlas premultiplied**: Textures stored in GPU-ready format (DXT5 compression)

---

## Quality Settings

### Presets

| Setting | Low | Medium | High | Ultra |
|---|---|---|---|---|
| Resolution scale | 0.5x (960x540) | 0.75x (1440x810) | 1.0x (1920x1080) | 1.0x (1920x1080) |
| Texture quality | 50% atlas | 75% atlas | Full | Full |
| Anisotropic filtering | 2x | 4x | 8x | 16x |
| Anti-aliasing | Off | FXAA | FXAA | SMAA x2 |
| Shadows | Off | On (low res) | On | On + soft |
| Particles | 25% | 50% | 75% | 100% |
| Post-processing | Off | Bloom only | Bloom + glow | Full (bloom, glow, color grading) |
| VSync | Off | Off | On | On |
| Screen shake | Off | On | On | On |
| Ambient effects | Off | Low | On | On |
| FPS cap | 30 | 60 | 60 | 60 (unlocked in menu) |

### Auto-Detect
- On first boot, run quick benchmark:
  - Render 500 sprites at once, measure FPS
  - Apply preset based on result (> 55 FPS = High, > 35 FPS = Medium, else Low)
  - User can override in settings

---

## Profiling Tools

### In-Game Debug Overlay
- Toggle with F3
- Shows: FPS, frame time, draw calls, physics objects, memory usage, audio channels
- Color-coded: green (within budget), yellow (warning), red (over budget)

### Editor Profiling
- Per-frame breakdown in editor toolbar
- Frame capture: dump frame stats to CSV for analysis
- Memory snapshot: show all loaded assets with memory usage

---

## Known Bottlenecks & Mitigations

| Bottleneck | Mitigation |
|---|---|
| Draw calls | Sprite batching, texture atlasing |
| Fill rate (many transparent sprites) | Minimize overdraw, depth sort |
| Physics broad phase | Spatial hash grid |
| Audio decode (MP3) | Background thread decode |
| Level load time | Async load, load first section first |
| Save corruption | Write to temp file, rename on success |
| GC pressure (C#) | Object pooling, minimize allocations in hot paths |
