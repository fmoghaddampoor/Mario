# Levels & World Map — Structure & Progression

## Overview
10 worlds, 10 levels each = 100 total levels. Each world ends with a castle/boss level. Levels follow a difficulty curve across the game and within each world.

---

## World Map Structure

### Top-Down Overworld Map
- Each world has a connected map screen
- Path nodes represent levels
- Completed levels show a checkmark or star
- Current level pulses/highlights
- Paths can branch (secret exits unlock new paths)
- Animated character token moves along paths

### World List

| World | Theme | Difficulty | Levels |
|---|---|---|---|
| 1 | Grassland | Easy (tutorial) | 1-1 to 1-10 |
| 2 | Underground | Easy-Medium | 2-1 to 2-10 |
| 3 | Desert | Medium | 3-1 to 3-10 |
| 4 | Ocean / Beach | Medium | 4-1 to 4-10 |
| 5 | Forest | Medium-Hard | 5-1 to 5-10 |
| 6 | Ice / Mountain | Hard | 6-1 to 6-10 |
| 7 | Volcano / Lava | Hard | 7-1 to 7-10 |
| 8 | Sky / Cloud | Very Hard | 8-1 to 8-10 |
| 9 | Ghost / Haunted | Very Hard | 9-1 to 9-10 |
| 10 | Final Castle | Expert | 10-1 to 10-10 |

---

## Level Structure (Per World)

### Level Composition
| Level # | Type | Description |
|---|---|---|
| 1 | Intro | Easy introduction to world theme and new mechanics |
| 2 | Standard | Normal difficulty, introduces first new enemy |
| 3 | Standard | Slightly harder, combines mechanics |
| 4 | Underground/Sub-area | Cave, underwater, or alternate path |
| 5 | Standard | Mid-world difficulty spike |
| 6 | Tower / Fortress | Shorter, more enemies, tighter spaces |
| 7 | Standard | High difficulty, complex layouts |
| 8 | Standard | Pre-boss gauntlet |
| 9 | Secret / Bonus | Optional level (unlocked by secret exit) |
| 10 | Castle / Boss | Boss fight level |

### Level Numbering
- World 1: 1-1, 1-2, 1-3, 1-4, 1-5, 1-6, 1-7, 1-8, 1-9 (secret), 1-10 (castle)
- World 2: 2-1 through 2-10
- etc.

### Secret Levels
- Level 9 in each world is optional (unlocked via secret exit in another level)
- Harder than the world's castle level
- Rewards: Star Coins, 1-Ups, shortcut to next world

---

## Required Elements Per Level

### Every Level Must Have:
- [ ] Start point (Mario spawn)
- [ ] End point (flagpole or goal)
- [ ] At least 10 coins
- [ ] At least 1 ? block
- [ ] At least 5 enemies (varied per level)
- [ ] At least 1 platforming challenge (gap, jump sequence)
- [ ] Unique visual elements (world theme)

### Every Level Should Have (optional):
- [ ] Hidden block or secret area
- [ ] Star Coin (1-3 per level)
- [ ] 1-Up opportunity
- [ ] New enemy introduction or mechanic teaching moment
- [ ] Pipe or vine to bonus area

### Castle Levels (Level 10) Must Have:
- [ ] Boss arena
- [ ] Pre-boss challenge (gauntlet of enemies)
- [ ] Checkpoint before boss room
- [ ] Power-up before boss room (mushroom/fire flower)
- [ ] Axe or boss defeat mechanism
- [ ] No ? blocks after boss (all resources before)

---

## Difficulty Curve

### Across Worlds
```
World 1:  ██○○○○○○○○  (tutorial, gentle slopes and small gaps)
World 2:  ███○○○○○○○  (darker, tighter spaces)
World 3:  ████○○○○○○  (projectile enemies, larger gaps)
World 4:  █████○○○○○  (water sections, breath management)
World 5:  ██████○○○○  (complex enemy combinations)
World 6:  ███████○○○  (slippery physics, precision jumps)
World 7:  ████████○○  (hazards everywhere, fast enemies)
World 8:  █████████○  (bottomless pits, wind mechanics)
World 9:  ██████████  (puzzle + combat, limited visibility)
World 10: ██████████  (everything combined, no mercy)
```

### Within Each World
```
Level 1:  ██○○○○○○○○  (ease into theme)
Level 2:  ███○○○○○○○
Level 3:  ████○○○○○○
Level 4:  █████○○○○○  (mid-world variety)
Level 5:  ██████○○○○  (difficulty spike)
Level 6:  ███████○○○  (fortress — intense but short)
Level 7:  ████████○○
Level 8:  █████████○  (pre-castle gauntlet)
Level 9:  ██████████  (optional — hardest in world)
Level 10: ██████████  (boss fight — different difficulty type)
```

---

## Level Modifiers & Special Mechanics

### Timed Levels
- Some levels have a countdown timer (100–300 seconds)
- Extra time = bonus points at end
- Common in tower/fortress levels

### Ghost House Levels
- No timer
- Puzzle-oriented: find hidden exit
- Rooms connect via doors
- False paths and looping rooms

### Underwater Levels
- Full swim physics (no gravity, 8-directional movement)
- Breath meter (30s before drowning)
- Air bubbles refill breath
- Slower movement

### Auto-Scroll Levels
- Screen forces scroll right
- Cannot go backward
- Common in sky/airship levels
- Precision platforming required

### Night / Dark Levels
- Reduced visibility (character has small light aura)
- Light sources: torches, glowing blocks, enemies
- Common in ghost world

---

## Secrets & Bonus Areas

### Secret Exit Types
- Hidden block path above main route
- Pipe in hidden location
- Vine from hidden block
- Invisible door in ghost house
- Defeat all enemies in a room

### Secret Rewards
| Reward | Rarity | Effect |
|---|---|---|
| Shortcut pipe | Common | Skips to next world |
| Extra Star Coin | Common | 1-UP at 100 |
| 1-Up Mushroom | Common | +1 life |
| Warp zone | Rare | Skip 2+ worlds |
| Bonus room access | Rare | Coin heaven (50+ coins) |
| Power-up room | Rare | Choose any mushroom |

### Warp Zones
- 3 warp zones in the game
- Located in specific levels (Worlds 1, 3, 5)
- Warp to later worlds (skip ahead)
- Cannons / pipes with world numbers
- One-way only (cannot warp backward)

---

## Star Coins

### Per Level
- 3 Star Coins per level (except castles: 1, secret levels: 5)
- Total: 3 × 90 + 1 × 10 + 5 × 10 = 330 Star Coins

### Placement Rules
- Star Coin 1: Visible but requires a simple detour
- Star Coin 2: Hidden or requires a challenge
- Star Coin 3: Well-hidden or requires skill (timed jump, enemy bounce chain)

### Unlocks
| Star Coins Collected | Unlock |
|---|---|
| 10 | Gallery: Concept Art 1 |
| 30 | Bonus Level: Coin Heaven 1 |
| 50 | Gallery: Concept Art 2 |
| 80 | Music Room (listen to any track) |
| 100 | Bonus World: Star World |
| 150 | Gallery: Concept Art 3 |
| 200 | Bonus World: Mushroom World |
| 250 | Developer Commentary Mode |
| 300 | Ultimate Challenge Level |
| 330 | 100% Completion Badge + Secret Ending |

---

## Secret World Unlocks

| World | Unlock Condition |
|---|---|
| Star World | Collect 100 Star Coins (accessed from world map) |
| Mushroom World | Collect 200 Star Coins (accessed from world map) |

### Star World
- 5 levels, ultra-hard
- No checkpoints
- No power-ups except what you find
- Star Coin rewards: 3 per level

### Mushroom World
- 5 levels, gimmick-focused
- Each level themed around one mushroom type
- Must use the mushroom power to solve puzzles
- Star Coin rewards: 3 per level

---

## Level File Format (Design Reference)

```json
{
  "id": "1-1",
  "name": "Grassland Hop",
  "world": 1,
  "theme": "grassland",
  "timeLimit": 300,
  "autoScroll": false,
  "underwater": false,
  "ghostHouse": false,
  "tiles": {
    "layers": ["ground", "decoration", "collision"],
    "width": 200,
    "height": 15
  },
  "entities": [
    { "type": "goomba", "x": 640, "y": 448 },
    { "type": "koopa", "x": 1280, "y": 320 }
  ],
  "blocks": [
    { "type": "question", "x": 320, "y": 192, "contents": "mushroom" },
    { "type": "brick", "x": 384, "y": 192, "contents": "coin" }
  ],
  "spawn": { "x": 64, "y": 384 },
  "flag": { "x": 6400, "y": 256 },
  "checkpoint": { "x": 3200, "y": 384 },
  "starCoins": [
    { "index": 1, "x": 960, "y": 256 },
    { "index": 2, "x": 2560, "y": 192 },
    { "index": 3, "x": 4480, "y": 128 }
  ],
  "secretExit": { "type": "pipe", "x": 5760, "y": 384, "dest": "1-9" }
}
```

---

## Quality Checklist (Per Level)

- [ ] Fun to play (playtested)
- [ ] No softlocks (player can always progress)
- [ ] Coins in reasonable positions
- [ ] Enemies are fair (not unavoidable damage)
- [ ] Power-ups before difficult sections
- [ ] At least one secret/optional area
- [ ] Clear visual guidance (where to go)
- [ ] Consistent theme
- [ ] No dead ends (unless intentional, with return path)
- [ ] Star Coins achievable
- [ ] Checkpoint before hard sections
- [ ] Boss level has power-up before fight
