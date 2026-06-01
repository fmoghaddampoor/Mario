# Audio Guide — Super Mario Game

## Philosophy
Full orchestral / high-quality recorded music. No chiptune, no synthesized placeholder tracks. Every piece is original, performed by live musicians or high-end virtual instruments. Audio is treated as a core pillar of the experience, on par with the visuals.

---

## Music

### Composition Style
- Full orchestra (strings, brass, woodwinds, percussion) with occasional electronic elements
- Genre: Cinematic orchestral with whimsical/platformer energy
- Influences: Super Mario Galaxy, Ori, Rayman Legends, Dragon Quest
- Every track has melody, harmony, countermelody, and bass — no looping 4-bar phrases
- Key signatures and time signatures may shift per world to reflect mood

### Track List (per world)

| World | Theme | Mood | Instruments |
|---|---|---|---|
| 1 | Grassland | Bright, adventurous | Strings, trumpet, glockenspiel, light percussion |
| 2 | Underground | Mysterious, rhythmic | Low strings, marimba, reverb-heavy percussion |
| 3 | Desert | Grand, sweeping | Flamenco guitar, castanets, brass, oud |
| 4 | Ocean | Calm, flowing | Harp, flutes, vibraphone, gentle strings |
| 5 | Forest | Magical, lush | Woodwinds, celesta, soft strings, birdsong |
| 6 | Ice | Whimsical, cold | Music box, chimes, high strings, light glockenspiel |
| 7 | Volcano | Intense, driving | Heavy brass, taiko drums, distorted bass, electric guitar |
| 8 | Sky | Soaring, uplifting | Full orchestra, choir, bells, soaring strings |
| 9 | Ghost | Eerie, haunting | Theremin, eerie strings, piano, whispers, reverb |
| 10 | Final Castle | Epic, climactic | Full orchestra + choir, organ, heavy percussion |

### Additional Track Types
- **Title theme**: 90-second cinematic piece
- **Boss theme**: Upbeat, high-energy, changes phase as boss health drops
- **Starman theme**: Majestic heroic variation of the main melody (full orchestra blast)
- **Level clear**: 10-second triumphant stinger
- **World clear**: 30-second celebratory fanfare
- **Game over**: Somber 8-second piece
- **Credits**: 3-minute medley of all world themes

### Layered / Adaptive Music
- Each level track is authored in **stems** (layers) that can fade in/out:
  - **Stem 1 — Rhythm**: Percussion + bass (always present)
  - **Stem 2 — Harmony**: Strings / pads
  - **Stem 3 — Melody**: Lead instrument
  - **Stem 4 — Intensity**: Extra layers (brass, choir) for action moments
- Engine crossfades between stems based on gameplay:
  - Walking: Stems 1 + 2
  - Enemy nearby: Add Stem 3
  - Combat / power-up: Add Stem 4
  - Underwater: Low-pass filter + remove high-end stems
  - Underground: Remove high frequencies, add reverb
- Seamless crossfade between world themes during transitions

### Technical Specs
| Property | Music |
|---|---|
| Format | MP3 (320 kbps CBR) |
| Sample rate | 48 kHz |
| Channels | Stereo |
| Per-track length | 2–4 minutes (loopable) |
| File size per track | ~5–10 MB |
| Delivery | Streamed from disk, never fully in RAM |

---

## Sound Effects

### Design Philosophy
- High-fidelity recorded/produced SFX — no synthesized beeps
- Every interaction has a satisfying audio response
- Layered sounds for impact (e.g. stomp = squash + thud + starburst jingle)

### SFX Categories

#### Player
| Action | Sound Description |
|---|---|
| Jump | Quick whoosh + springy boing |
| Double jump | Higher pitched whoosh + sparkle |
| Landing | Soft thud (varies by surface) |
| Run (footstep) | Footstep per surface: grass, stone, sand, metal, snow |
| Slide | Scuff + whoosh |
| Stomp enemy | Squish + low thump + coin jingle |
| Hurt | Oof + quick descending tone |
| Death | Prolonged fall whoosh + thud + silence |
| Power-up mushroom | Ascending magical shimmer |
| Power-up fire flower | Flame burst + melodic chime |
| Star | Rapid sparkle shimmer loop |

#### Enemies
| Enemy | Sound |
|---|---|
| Goomba walk | Soft rhythmic pat |
| Goomba stomped | Squish + pop |
| Koopa shell kick | Hollow thwack + spin whoosh |
| Piranha Plant emerge | Pop + wet slither |
| Piranha Plant bite | Chomp + snap |
| Boss hit | Heavy thud + metallic clang |
| Boss death | Explosion + descending rumble + final crash |

#### Environment
| Sound | Description |
|---|---|
| Coin collect | Bright ding (pitched per coin, ascending scale for streaks) |
| Block hit | Low thud (wood/stone depending on block) |
| Block empty | Hollow thud |
| Pipe travel | Suction whoosh + slide + pop out |
| Checkpoint | Chime arpeggio + sparkle |
| Flagpole | Sliding clang + fanfare on capture |
| Lava | Constant bubbling + hiss on contact |
| Water splash | Full liquid splash (enter, exit, swim loop) |
| Breakable block shatter | Crack + debris scatter |
| Door / warp | Creak + slam + magical swirl |

#### UI
| Sound | Description |
|---|---|
| Menu navigate | Soft click / tap |
| Menu confirm | Rising chime |
| Menu cancel | Descending chime |
| Pause | Time-stop whoosh + click |
| Unpause | Reverse whoosh |
| Countdown beep | Sharp tick, last second higher pitch |
| Level start | Quick orchestral sting |
| Level fail | Descending sad tones |

### Technical Specs
| Property | SFX |
|---|---|
| Format | WAV (uncompressed, PCM 16-bit) |
| Sample rate | 48 kHz |
| Channels | Mono (positioned in world) or Stereo (UI / ambient) |
| Per-file size | 10–500 KB |
| Delivery | Pre-loaded into OpenAL buffers at startup |

---

## Audio System Architecture

### Engine Integration
```
Game Event (e.g. EnemyStomped)
    → AudioManager.PlaySFX("enemy_stomp")
        → SFXPool.GetSource()
            → Set OpenAL buffer, position, pitch, volume
            → alSourcePlay()

Music:
    → MusicStream.Update()
        → Stream next MP3 chunk → decode PCM → queue to OpenAL
        → Crossfade logic (if transitioning)
```

### Mixer / Bus System
```
Master Bus (0 dB)
 ├── Music Bus (-6 dB)
 │    ├── Stem_Rhythm
 │    ├── Stem_Harmony
 │    ├── Stem_Melody
 │    └── Stem_Intensity
 ├── SFX Bus (0 dB)
 │    ├── Player
 │    ├── Enemies
 │    ├── Environment
 │    └── UI
 └── Voice Bus (0 dB, unused in this game)
```

- Each bus has: volume, mute, solo, pitch
- Settings exposed in Options menu → persist to save file

### Dynamic Audio Features
- **Reverb zones**: Entering a cave/pipe applies OpenAL reverb effect
- **Occlusion**: Thick walls slightly low-pass SFX behind them
- **Doppler**: Fast-moving objects (shells, thrown items) get pitch shift
- **Pitch variation**: SFX play at random pitch 0.9–1.1x for organic feel
- **Priority system**: Critical SFX (damage, death) steal sources from less important ones
- **Ducking**: Music volume slightly reduced during SFX (sidechain compression style) for clarity

---

## Asset Pipeline

### Production Workflow
```
Composition (DAW: Cubase / Logic Pro)
    → Mix + Master (48 kHz / 24-bit WAV)
        → Music: Encode to MP3 320kbps (LAME)
        → SFX: Keep as WAV (uncompressed)
            → Name and categorize
                → Import into game assets folder
```

### File Naming Convention
- Music: `music_world_1_grassland.mp3`, `music_boss_fast.mp3`
- Music stems: `music_world_1_grassland_stem_rhythm.mp3`
- Player SFX: `sfx_player_jump.wav`, `sfx_player_stomp.wav`
- Enemy SFX: `sfx_enemy_goomba_stomp.wav`
- UI SFX: `sfx_ui_confirm.wav`, `sfx_ui_navigate.wav`
- Ambient: `sfx_ambient_cave_drip.wav`, `sfx_ambient_forest_birds.wav`

### Folder Structure
```
assets/audio/
 ├── music/
 │    ├── world_1_grassland/
 │    │    ├── music_world_1_grassland.mp3
 │    │    ├── music_world_1_grassland_stem_rhythm.mp3
 │    │    ├── music_world_1_grassland_stem_harmony.mp3
 │    │    ├── music_world_1_grassland_stem_melody.mp3
 │    │    └── music_world_1_grassland_stem_intensity.mp3
 │    ├── world_2_underground/
 │    ├── ...
 │    └── boss/
 │         ├── music_boss_normal.mp3
 │         └── music_boss_final_phase.mp3
 ├── sfx/
 │    ├── player/
 │    ├── enemies/
 │    ├── environment/
 │    └── ui/
 └── ambience/
      ├── amb_cave_drip.wav
      ├── amb_forest.wav
      └── amb_underwater.wav
```

### Quality Checklist (per asset)
- [ ] No clipping / distortion (peak ≤ -1 dB)
- [ ] Consistent loudness across tracks (-14 LUFS integrated)
- [ ] Music loops seamlessly (check loop point for clicks)
- [ ] SFX properly trimmed (no leading silence)
- [ ] Correct sample rate (48 kHz)
- [ ] Correct format (MP3 for music, WAV for SFX)
- [ ] Named per convention
---

## Audio Team Requirements
- **Composer**: Orchestral/cinematic composer with platformer experience
- **Sound Designer**: Foley + sound design for SFX
- **Implementation**: Handled by engine programmer (C# + OpenAL integration)
