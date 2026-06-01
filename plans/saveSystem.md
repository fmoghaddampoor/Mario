# Save System — Save/Load & Persistence

## Overview
Save system for game progress, settings, and unlockables. Supports 3 save slots with auto-save and manual save/load.

---

## Save Slots

| Property | Value |
|---|---|
| Number of slots | 3 |
| Slot display | Level reached, World, Star Coin count, Lives, Play time, Timestamp |
| Empty slot | Marked as [EMPTY], shows "New Game" option |

---

## Save Triggers

### Auto-Save Events
| Event | Slot | Description |
|---|---|---|
| Level complete | Current slot | After score tally screen |
| Checkpoint touched | Current slot | When Mario touches a checkpoint flag |
| Boss defeated | Current slot | After boss death animation |
| World cleared | Current slot | Before world map transition |
| Power-up collected | Current slot | (optional, configurable) |
| Settings changed | N/A | Saved to `settings.json` immediately |

### Manual Save
- Available from pause menu → "Save"
- Available from world map → "Save"
- Overwrites current slot with confirmation prompt

### Manual Load
- Available from title screen → "Load Game"
- Shows 3 slots with preview data
- Confirmation before loading (unsaved progress lost)

---

## Save Data Structure

```json
{
  "version": 1,
  "timestamp": "2026-06-01T14:30:00Z",
  "playTime": 3720,
  "slot": 1,

  "player": {
    "lives": 7,
    "currentPowerUp": "super",
    "score": 123456,
    "coins": 67,
    "continues": 3
  },

  "progress": {
    "currentWorld": 5,
    "currentLevel": "5-3",
    "completedLevels": [
      "1-1", "1-2", "1-3", "1-4", "1-5", "1-6", "1-7", "1-8", "1-10",
      "2-1", "2-2", "2-3", "2-4", "2-5", "2-6", "2-7", "2-10",
      "3-1", "3-2", "3-3", "3-4", "3-5", "3-10",
      "4-1", "4-2", "4-3", "4-10",
      "5-1", "5-2"
    ],
    "unlockedWorlds": [1, 2, 3, 4, 5],
    "secretExitsFound": ["1-3→1-9"],
    "warpZonesUsed": []
  },

  "starCoins": {
    "total": 12,
    "perLevel": {
      "1-1": 2,
      "1-2": 1,
      "1-3": 3,
      "1-4": 1,
      "1-5": 2,
      "1-6": 0,
      "1-7": 1,
      "1-8": 0,
      "1-10": 0
    }
  },

  "checkpoints": {
    "5-3": { "x": 3200, "y": 384 }
  },

  "unlocks": {
    "conceptArt": [1],
    "musicRoom": false,
    "bonusWorldStar": false,
    "bonusWorldMushroom": false,
    "commentaryMode": false
  },

  "settings": {
    "musicVolume": 0.7,
    "sfxVolume": 0.6,
    "fullscreen": true,
    "resolution": "1920x1080",
    "vsync": true,
    "controllerVibration": true,
    "language": "en"
  }
}
```

---

## File Format & Location

| Property | Value |
|---|---|
| File format | JSON (human-readable, debuggable) |
| Extension | `.mariosave` |
| Per-slot file | `save_slot_1.mariosave` |
| Settings file | `settings.json` |
| Location (Windows) | `%APPDATA%\MarioGame\saves\` |
| Location (macOS) | `~/Library/Application Support/MarioGame/saves/` |
| Location (Linux) | `~/.local/share/MarioGame/saves/` |

### Encryption
- Save files are NOT encrypted (allows editing/backup)
- Checksum hash (SHA-256) appended to detect corruption
- If checksum fails: show "Save corrupted" message, offer to load backup

### Backup
- Auto-backup on save: `save_slot_1.mariosave.bak`
- Backup created before overwriting (3 most recent backups retained)

---

## Save Screen UI

### Load Menu
```
┌──────────────────────────────────────────────────────┐
│                    LOAD GAME                           │
│                                                        │
│     ┌───────────────────────────────────────────┐     │
│     │ SLOT 1                                    │     │
│     │ World 5 — Forest                    ★★☆  │     │
│     │ 7 Lives  |  01:02:03  |  Jun 1, 2026    │     │
│     │                              [LOAD] [DEL]│     │
│     ├───────────────────────────────────────────┤     │
│     │ SLOT 2                                    │     │
│     │ World 2 — Underground               ★☆☆  │     │
│     │ 12 Lives  |  00:32:15  |  May 28, 2026  │     │
│     │                              [LOAD] [DEL]│     │
│     ├───────────────────────────────────────────┤     │
│     │ SLOT 3                                    │     │
│     │ [EMPTY]                                   │     │
│     │                                   [NEW]   │     │
│     └───────────────────────────────────────────┘     │
│                                                        │
│                    [BACK]                               │
└──────────────────────────────────────────────────────┘
```

### Save Menu
```
┌──────────────────────────────────────────────────────┐
│                    SAVE GAME                            │
│                                                        │
│     ┌───────────────────────────────────────────┐     │
│     │ SLOT 1  (Current: World 5, 02:15:00)     │     │
│     │                              [SAVE]       │     │
│     ├───────────────────────────────────────────┤     │
│     │ SLOT 2  (World 2, 00:32:15)              │     │
│     │                              [SAVE] [DEL]│     │
│     ├───────────────────────────────────────────┤     │
│     │ SLOT 3  [EMPTY]                          │     │
│     │                              [SAVE]       │     │
│     └───────────────────────────────────────────┘     │
│                                                        │
│                  [BACK]                                 │
└──────────────────────────────────────────────────────┘
```

- Current slot highlighted with "Current" badge
- Save with no changes warns: "No progress since last save. Save anyway?"
- Save overwrite confirms: "Overwrite save slot X? Current progress: World Y"

---

## Save Slot Display Data

| Field | Source |
|---|---|
| World name | Derived from `currentWorld` |
| Star Coin progress | `starCoins.total` / total for worlds completed |
| Lives | `player.lives` |
| Play time | `playTime` (formatted as HH:MM:SS) |
| Timestamp | `timestamp` |
| Levels completed | Count of `completedLevels` |
| Completion % | (levels completed / 100) × 100 |

---

## Settings Persistence

### Settings File
```json
{
  "musicVolume": 0.7,
  "sfxVolume": 0.6,
  "fullscreen": true,
  "resolution": "1920x1080",
  "vsync": true,
  "controllerVibration": true,
  "language": "en",
  "showFps": false,
  "screenShake": true,
  "particleQuality": "high"
}
```

- Saved immediately on any setting change
- Separate from save slots (shared across all profiles)
- No confirmation needed

---

## Error Handling

| Scenario | Behavior |
|---|---|
| Save file not found | Treat as empty slot |
| Save file corrupted | Show warning, offer to load backup |
| Backup also corrupted | Reset slot to empty, log error |
| Disk full / write error | Show error message, do not lose current session |
| Read error | Show error, return to previous screen |
| Version mismatch | Attempt migration, if fails: show "Save from older version, may not be fully compatible" |

---

## Achievements / Trophy Data (Future)

Reserved fields for future achievement system:
```json
"achievements": {
  "first_stomp": true,
  "coin_100": true,
  "no_damage_clear": false,
  "speed_run_world_1": false,
  "all_star_coins_world_1": false,
  "secret_exit_finder": false
}
```

Not implemented in v1.0, but save structure reserves space.

---

## Summary of Save System Rules

| Rule | Detail |
|---|---|
| 3 save slots | Independent progress per slot |
| Auto-save | On level complete, checkpoint, boss defeat |
| Manual save | From pause menu or world map |
| Manual load | From title screen |
| Settings | Separate file, shared across slots |
| Format | JSON + SHA-256 checksum |
| Backup | 3 rotating backups per slot |
| Cross-platform | OS-appropriate save folder |
| No encryption | Allows editing and backup |
| Corruption handling | Graceful fallback with user notification |
