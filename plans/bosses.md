# Bosses — Design & Behavior

## Overview
10 unique boss fights, one per world. Each boss has multiple phases, a distinct arena, and unique attack patterns. Bosses escalate in complexity from World 1 to World 10.

---

## Boss Design Template

Each boss includes:
- **Arena**: Layout, hazards, walls, floor
- **Health**: Number of hits required
- **Phases**: Behavior changes at health thresholds
- **Attacks**: Patterned attacks with telegraphs
- **Weak point**: Where/how Mario deals damage
- **Reward**: What drops on defeat

---

## World 1 — Grassland: Brokk the Koopa King

| Property | Value |
|---|---|
| Size | 128 x 120 px |
| Health | 3 hits |
| Arena | Flat stone bridge over grass, 600 px wide, no pits |
| Theme | Introductory boss — simple patterns |

### Phase 1 (HP: 3-2)
- Walks back and forth, 60 px/s
- Stops and breathes fire straight ahead every 3s (2 tiles high)
- Weak point: stomp head when stopped

### Phase 2 (HP: 1)
- Enraged — speed 90 px/s
- Fire breath every 2s, wider arc
- Occasionally jumps (creates small shockwave)

### Defeat
- 3 stomps → collapse → poof
- Drops: Super Mushroom + checkpoint flag

### Arena
```
[Platform] ← 600px → [Platform]
    [Bridge over grass, no bottomless pits]
```

---

## World 2 — Underground: Crag the Drill Mole

| Property | Value |
|---|---|
| Size | 96 x 64 px |
| Health | 4 hits |
| Arena | Underground chamber, low ceiling, 800 px wide, breakable blocks on floor |
| Theme | Burrowing boss — attacks from below |

### Phase 1 (HP: 4-3)
- Burrows underground, leaves moving dirt trail
- Emerges after 2s at Mario's location
- Vulnerable for 1s after emerging

### Phase 2 (HP: 2)
- Burrows faster (1.5s underground)
- Throws 2 dirt clods on emerge
- Dirt clods break into 3 smaller projectiles

### Phase 3 (HP: 1)
- Stays above ground, charges at Mario (160 px/s)
- Digs to opposite side and repeats
- Throws dirt clods while charging

### Defeat
- 4 stomps → spins out → collapses
- Drops: Super Mushroom + key

### Arena
```
[Low ceiling with hanging stalactites]
[Breakable blocks scattered on floor]
[Floor — 800px wide]
```

---

## World 3 — Desert: Sundance the Fire Spirit

| Property | Value |
|---|---|
| Size | 80 x 80 px (core), flame aura extends to 120 px |
| Health | 5 hits |
| Arena | Large circular arena, 4 pillars, lava floor sections |
| Theme | Fast projectile boss — bullet hell-lite |

### Phase 1 (HP: 5-4)
- Floats above center, shoots 3 fireballs in spread pattern
- Fireballs bounce once, last 4s
- Vulnerable after firing (1.5s window)

### Phase 2 (HP: 3-2)
- Teleports to random pillar, shoots 5-fireball spread
- Summons 2 mini fire spirits (1 HP each) that chase Mario
- Pillars provide cover

### Phase 3 (HP: 1)
- Splits into 3 copies (2 are illusions)
- All 3 shoot fireballs simultaneously
- Real one flashes briefly before attack
- Lava floor sections rise and lower

### Defeat
- 5 hits → core expands → implodes + explosion
- Drops: Fire Flower + key

### Arena
```
       [Pillar]       [Pillar]
          [Lava]        [Lava]
       [Pillar]       [Pillar]
  [Walkable floor between lava pools]
```

---

## World 4 — Ocean: Marina the Kraken

| Property | Value |
|---|---|
| Size | 240 x 200 px (visible above water) |
| Health | 6 hits |
| Arena | Underwater arena with air pockets, 1000 px wide, rising water mechanic |
| Theme | Water boss — swimming combat |

### Phase 1 (HP: 6-5)
- Tentacles emerge from water (3 at a time), slam down in sequence
- Vulnerable: hit tentacle tips when they rise
- Water level rises slowly (reduces safe space)

### Phase 2 (HP: 4-3)
- Head emerges, shoots ink blasts (slow Mario 50% for 3s)
- Tentacles slam faster, 4 at a time
- Water level rises faster
- Air bubbles appear — collect to refill breath meter

### Phase 3 (HP: 2-1)
- Full body emerges, charges across arena
- Creates waves that push Mario
- Tentacles sweep horizontally
- Water at max height — full swim mode

### Defeat
- 6 hits → stunned → ink explosion → sinks
- Drops: Penguin Mushroom + key

### Arena
```
[Surface — air above]
[Water — rising during fight]
[Floor with air pockets / bubbles]
```

---

## World 5 — Forest: Thornheart the Treant

| Property | Value |
|---|---|
| Size | 192 x 256 px |
| Health | 7 hits |
| Arena | Forest clearing, 900 x 600 px, roots and platforms |
| Theme | Big slow boss with AoE attacks |

### Phase 1 (HP: 7-5)
- Stands in center, slams arms down (ground pound AoE)
- Drops acorns from canopy (3 at a time, fall 1s after warning shadow)
- Vulnerable: hit the glowing heart on chest after arm slam

### Phase 2 (HP: 4-3)
- Roots emerge from ground — 4 root tentacles sweep arena
- Acorn rain — 5 acorns at a time
- Heart glows longer, vulnerable window bigger but less frequent
- Raises/lowers platform sections

### Phase 3 (HP: 2-1)
- Enraged: slam rate doubles
- Roots sweep faster
- Summons 2 Wiggler adds (1 HP each)
- Canopy blocks fall — destroy platforms

### Defeat
- 7 hits → heart shatters → tree crumbles
- Drops: Propeller Mushroom + key

### Arena
```
[Canopy above — dropping acorns]
[Root platforms — moving]
[Floor with root sweeps]
```

---

## World 6 — Ice: Glacia the Frost Queen

| Property | Value |
|---|---|
| Size | 136 x 180 px |
| Health | 6 hits |
| Arena | Ice palace, mirrored floor, 800 x 500 px, slippery surfaces |
| Theme | Precision platforming + boss |

### Phase 1 (HP: 6-5)
- Hovers above center, shoots ice shards (3 piercing projectiles)
- Freezes floor sections — creates slippery patches
- Vulnerable: jump and hit head after shard volley

### Phase 2 (HP: 4-3)
- Summons 2 Ice Stalagmites — rise from floor, stay 4s, then shatter
- Shoots ice beam that sweeps across arena (3s wind-up, jump over)
- Floor freezing covers 50% of arena

### Phase 3 (HP: 2-1)
- Arena-wide blizzard: reduced visibility, wind pushes Mario
- Ice beam sweeps faster with no wind-up
- Summons 4 Ice Stalagmites simultaneously
- Vulnerable only during blizzard cooldown (2s window)

### Defeat
- 6 hits → cracks → shatters into ice crystals
- Drops: Penguin Mushroom (if not owned) + key

### Arena
```
[Ice floor — slippery]
[Platforms — some frozen, some clear]
[Ceiling with icicles]
```

---

## World 7 — Volcano: Ignis the Lava Titan

| Property | Value |
|---|---|
| Size | 200 x 240 px |
| Health | 8 hits |
| Arena | Volcano crater, rising lava, collapsing platforms, 900 x 500 px |
| Theme | Endurance boss — arena shrinks over time |

### Phase 1 (HP: 8-6)
- Throws lava boulders (arc, 3 at a time, leave fire on ground 3s)
- Ground pound — creates shockwave (jump to avoid)
- Vulnerable: hit face after boulder throw recovery

### Phase 2 (HP: 5-3)
- Lava rises slowly — bottom 1/3 of arena becomes lethal
- Summons 2 Lava Bubbles (jump out of lava, unkillable)
- Lava boulders rain from sky (target Mario's position)
- Platform sections in center are the only safe floor

### Phase 3 (HP: 2-1)
- Lava rises to mid-point — only top platforms safe
- Breathes massive fire stream across arena (3s, sweep)
- Lava Bubbles spawn every 4s
- Platforms crumble 2s after standing on them (cycle)

### Defeat
- 8 hits → cracks → explosive eruption → collapses into lava
- Drops: Mega Mushroom + key

### Arena
```
[Top platforms — cycling crumble]
[Middle platforms]
[Lava — rising]
```

---

## World 8 — Sky: Zephyr the Storm Lord

| Property | Value |
|---|---|
| Size | 120 x 100 px |
| Health | 7 hits |
| Arena | Sky arena, floating cloud platforms, bottomless below, 1000 x 600 px |
| Theme | Mobility boss — constant movement, don't fall |

### Phase 1 (HP: 7-5)
- Flies around arena fast (zigzag pattern)
- Shoots lightning bolts at Mario's position (1s telegraph, dodge)
- Summons wind gusts that push Mario (telegraphed by particle effect)

### Phase 2 (HP: 4-3)
- Creates 3 tornadoes that patrol platforms (touch = damage + knockback)
- Lightning bolts: 2 at a time, faster telegraph (0.5s)
- Wind gusts reverse direction randomly

### Phase 3 (HP: 2-1)
- Arena-wide storm: platform visibility reduced, lightning everywhere
- Tornadoes: 5 active, faster
- Zephyr charges through arena at high speed (200 px/s)
- Vulnerable: stunned for 1.5s after charge misses

### Defeat
- 7 hits → dissipates → clear sky + rainbow
- Drops: Bee Mushroom + key

### Arena
```
[Cloud platforms — floating, some move]
[Bottomless below]
[Wind gusts visible as particle trails]
```

---

## World 9 — Ghost: Wraith the Soul Collector

| Property | Value |
|---|---|
| Size | 160 x 160 px (ethereal form) |
| Health | 8 hits |
| Arena | Haunted hall, multiple rooms, teleporting, 6 connected chambers |
| Theme | Puzzle + boss — must solve mechanics to damage |

### Phase 1 (HP: 8-6)
- Invisible — only visible as faint shimmer
- Attacks from shadows: sudden claw swipes
- Must collect 3 ghost flames in arena to reveal boss for 3s
- Vulnerable only when revealed

### Phase 2 (HP: 5-3)
- Summons 4 Boo minions (regenerate if not all killed)
- Creates shadow pools on floor (damage if standing in)
- Ghost flames spawn in random rooms — must navigate rooms to collect all 3
- Boss reveals for 2s after all flames collected

### Phase 3 (HP: 2-1)
- Boss constantly flickers visible/invisible (1s cycle)
- All rooms dark — ghost flames are only light source
- Shadow pools cover 50% of floor
- Summons 6 Boo minions
- Boss reveals for 1.5s windows, unpredictable timing

### Defeat
- 8 hits → screaming fade → light fills room
- Drops: Boo Mushroom (if not owned) + key

### Arena
```
[Room 1] ←→ [Room 2] ←→ [Room 3]
   ↕              ↕              ↕
[Room 4] ←→ [Room 5] ←→ [Room 6]
[Connected by doors — some lock/unlock]
```

---

## World 10 — Final Castle: Bowser

| Property | Value |
|---|---|
| Size | 256 x 200 px |
| Health | 3 hits (fireball/axe) |
| Arena | Castle bridge, 1000 x 400 px, lava below, bridge breaks progressively |
| Theme | Final boss — classic Bowser homage with modern mechanics |

### Phase 1 (HP: 3)
- Walks back and forth, 80 px/s
- Breathes fire: 3-shot bursts, spread pattern
- Vulnerable: hit with fireball 3 times to stun
- Bridge floor: solid

### Phase 2 (HP: 2)
- Charges at Mario, 150 px/s (1s wind-up, red flash)
- Fire breath: 5-shot spread, wider
- Creates shockwave on landing (if jumped)
- Bridge sections start breaking behind Bowser
- Vulnerable: fireball during charge recovery

### Phase 3 (HP: 1)
- Enraged: charge speed 200 px/s, fire breath 7-shot, no wind-up on some charges
- Jumps repeatedly — shockwaves on each landing
- Bridge crumbles progressively — arena shrinks
- Can also throw giant hammers (arc, 2 at a time)
- Vulnerable: 3 fireballs → stunned → axe grab opportunity

### Defeat
- Axe behind Bowser (appears after stun) → grab → bridge collapses → Bowser falls into lava
- Defeat animation: Bowser roars, falls, splash, smoke
- Drops: Key to Princess's chamber

### Arena
```
[Starting platform]
[Bridge — sections crumble as fight progresses]
[Lava below]
[Axe at the far end — appears after stun]
```

---

## Boss Music Reference

| World | Boss | Music Style |
|---|---|---|
| 1 | Brokk | Upbeat brass fanfare, simple melody |
| 2 | Crag | Dark percussive, low brass, timpani |
| 3 | Sundance | Fast flamenco/arabic strings, castanets |
| 4 | Marina | Orchestral swell, sea shanty undertones |
| 5 | Thornheart | Mystical forest drums, woodwinds |
| 6 | Glacia | Icy chimes, haunting strings |
| 7 | Ignis | Heavy metal brass, taiko drums |
| 8 | Zephyr | Soaring strings, rapid percussion |
| 9 | Wraith | Eerie choir, dissonant piano |
| 10 | Bowser | Epic full orchestra + choir + organ |

---

## Boss Difficulty Curve

```
World 1:  ●○○○○ (tutorial boss, 2 attack patterns)
World 2:  ●●○○○ (3 patterns, environmental hazard)
World 3:  ●●●○○ (projectile patterns, adds)
World 4:  ●●●●○ (water mechanic, breath meter)
World 5:  ●●●●● (AoE, arena changes, multiple adds)
World 6:  ●●●●● (slippery floor, precision timing)
World 7:  ●●●●● (shrinking arena, endurance)
World 8:  ●●●●● (bottomless arena, knockback)
World 9:  ●●●●● (puzzle mechanic, darkness)
World 10: ★★★★★ (final exam — everything combined)
```
