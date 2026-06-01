# Scoring — Points, Combos, Bonuses

## Overview
Complete scoring system for all actions, combos, multipliers, and end-of-level bonuses.

---

## Points by Action

### Enemy Kills

| Action | Base Points | Notes |
|---|---|---|
| Stomp Goomba | 100 | |
| Stomp Koopa | 200 | |
| Stomp other ground enemy | 400 | Buzzy Beetle, Monty Mole, etc. |
| Stomp Paratroopa | 400 | First hit (removes wings) |
| Stomp flying enemy | 400 | Lakitu, Paragoomba |
| Stomp Dry Bones | 200 | First hit (crumbles, not dead) |
| Stomp Dry Bones (reviving) | 400 | Kill during revive animation |
| Stomp Wiggler | 400 | Per hit (5 hits total) |
| Stomp Pokey segment | 200 | Per segment |
| Fireball kill | 200 | Any enemy |
| Star kill | 1000 | Any enemy (invincibility) |
| Shell kill | 200 | Per enemy hit by a kicked shell |
| Stomp Bowser (phase hit) | 1000 | Per fireball hit on Bowser |
| Boss death | 5000 | Final boss kill (each world's boss) |
| Chain kill (2nd+) | Multiplied | See Combo System below |

### Items & Collectibles

| Item | Points |
|---|---|
| Coin | 200 |
| Star Coin | 3000 |
| Super Mushroom (already Super+) | 1000 |
| 1-Up Mushroom | +1 life (no points) |
| Fire Flower (already Fire+) | 1000 |
| Brick break | 50 |
| Hit ? block (item) | 100 |
| Hit ? block (empty) | 0 |
| Hit hidden block | 100 |

### Level Completion

| Action | Points |
|---|---|
| Touch flagpole | 1000 |
| Time bonus | Remaining seconds × 10 |
| Star Coin bonus | 3000 per Star Coin collected in level |
| Secret area bonus | 2000 (on discovery) |
| No-damage bonus | 5000 (complete level without taking damage) |
| All Star Coins bonus | 10000 (collect all 3 in a level) |

---

## Combo System

### Stomp Combos
- Stomp enemies in succession without touching the ground
- Each consecutive stomp increases multiplier

| Consecutive Stomps | Multiplier | Example (Goomba) |
|---|---|---|
| 1st | 1.0x | 100 |
| 2nd | 1.5x | 150 |
| 3rd | 2.0x | 200 |
| 4th | 2.5x | 250 |
| 5th+ | 3.0x | 300 |

- Resets when Mario touches the ground
- Resets when Mario takes damage
- Resets on death
- Stomp combo counter displayed on HUD during combo

### Shell Combos
- Kicked shell that kills multiple enemies
- Each enemy killed by the same shell kick increases multiplier

| Shell Kills | Multiplier | Points per kill |
|---|---|---|
| 1st | 1.0x | 200 |
| 2nd | 1.5x | 300 |
| 3rd | 2.0x | 400 |
| 4th | 3.0x | 600 |
| 5th+ | 4.0x | 800 |

- Resets when shell stops or despawns
- Does not reset when shell bounces off wall (chain continues)

### Fireball Combos
- Kill multiple enemies with consecutive fireballs without taking damage

| Consecutive Fireball Kills | Multiplier |
|---|---|
| 1-2 | 1.0x |
| 3-4 | 1.5x |
| 5-6 | 2.0x |
| 7+ | 2.5x |

- Resets on taking damage
- Resets on death

---

## Streak Bonuses

### Coin Streak
- Collect coins in quick succession (within 1s of each other)
- Each coin in the streak gives increasing points

| Coin # in Streak | Points |
|---|---|
| 1 | 200 |
| 2 | 250 |
| 3 | 300 |
| 4 | 350 |
| 5 | 400 |
| 6 | 450 |
| 7 | 500 |
| 8 | 550 |
| 9 | 600 |
| 10+ | 650 |

- Streak indicator on HUD shows current streak
- Resets if > 1s passes between coin collections
- Resets on taking damage

### Starman Kill Streak
- Each enemy killed during Starman invincibility

| Kills during Starman | Points per kill |
|---|---|
| 1-5 | 1000 |
| 6-10 | 1500 |
| 11-15 | 2000 |
| 16+ | 2500 |

---

## End-of-Level Score Tally

```
┌──────────────────────────────────────────┐
│              LEVEL COMPLETE                │
│                                            │
│  Flagpole:       1,000                    │
│  Time Bonus:     2,560  (256s × 10)      │
│  Coins:          4,000  (20 × 200)        │
│  Enemies:        2,400  (12 enemies)      │
│  Star Coins:     9,000  (3 × 3000)        │
│  No-Damage:      5,000  ✓                 │
│  All Star Coins: 10,000  ✓                │
│  ─────────────────────────────             │
│  TOTAL:         33,960                    │
└──────────────────────────────────────────┘
```

### Bonus Flags
- No-Damage bonus: only if Mario finished the level with full HP (no damage taken)
- All Star Coins bonus: only if all 3 Star Coins in the level were collected
- Speed bonus: complete level in under 60s = extra 5000 points
- Hidden area bonus: +2000 per secret area discovered in the level

---

## High Score

### Per-Player High Score
- Saved to save file
- High Score displayed on title screen
- Updated when current score exceeds previous high score

### Per-Level Score
- Best score per level saved
- Displayed in level select / world map
- Star indicator: ☆ = completed, ★ = completed with all Star Coins

### Leaderboard Scores (if online)
- Level time trial: fastest completion time
- Level score: highest score (with details visible on tap)

---

## Scoring Display Rules

| Element | Display |
|---|---|
| Points pop-up | Floating text near action, fades up over 1s |
| Combo multiplier | Shown next to points pop-up (x1.5, x2, etc.) |
| Coin streak | Counter on HUD during streak |
| End-of-level tally | Full breakdown, numbers roll up |
| Score in HUD | Always visible, top-center |
| High score | On title screen, save file |

### Pop-Up Styling
- Small action (coin): 16px font, white, fades in 0.5s
- Medium action (stomp): 20px font, yellow, fades in 1s
- Major action (Star Coin, boss hit): 28px font, gold, fades in 1.5s
- Combo indicator: 18px font, orange, shown below score pop-up
- Streak counter: 20px font, cyan, shown on HUD

---

## Score Summary

| Category | Max per level (estimate) |
|---|---|
| Enemies | ~5,000 |
| Coins | ~10,000 |
| Star Coins | 9,000 |
| Time bonus | ~3,000 |
| End bonuses | 17,000 |
| **Total per level** | **~44,000** |
| **Total per world** | **~440,000** |
| **Total game (100 levels)** | **~44,000,000** |
