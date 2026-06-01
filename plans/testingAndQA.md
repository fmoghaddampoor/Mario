# Testing & QA — Strategy

## Overview
Testing strategy for a 100-level platformer with complex physics, 20+ enemy types, 10 bosses, and 10 power-ups.

---

## Testing Levels

### Unit Tests

| System | Tests | Framework |
|---|---|---|
| Physics | Collision detection, gravity, jump arc, slope movement | NUnit / xUnit |
| Movement | Acceleration, friction, max speed, air control | NUnit / xUnit |
| Collision | AABB overlap, tile collision, one-way platforms | NUnit / xUnit |
| Scoring | Point calculation, combo multiplier, 1-Up at 100 coins | NUnit / xUnit |
| Save/Load | Serialization, corruption detection, version migration | NUnit / xUnit |
| Input | Input mapping, rebinding, key conflict detection | NUnit / xUnit |
| Audio | Bus mixing, priority system, ducking | NUnit / xUnit |

All unit tests run in CI on every commit. < 100ms total runtime.

### Integration Tests

| Scenario | Description |
|---|---|
| Full level load | Load every level, verify all entities spawn at correct positions |
| Full playthrough | Auto-play every level (scripted inputs), verify goal reachable |
| Power-up transitions | Test all power-up state transitions (Small → Super → Fire → damage → etc.) |
| Boss health phases | Verify all boss phase transitions trigger at correct HP thresholds |
| Checkpoint system | Die at 5 positions per level, verify respawn at correct checkpoint |
| Secret exits | Verify all secret exits lead to correct destination |
| Warp zones | Verify all warps lead to correct level |

Integration tests run nightly. ~30 min total runtime.

### Automated Play Testing

- Recorded input sequences for 10 representative levels
- Replay inputs, verify: level completes within time, score matches expected, no crashes
- Regression check: compare output log against baseline
- Run on every PR that touches gameplay code

---

## Manual Testing

### Regression Test Pass (Pre-Release)

| Area | Tester Hours | Focus |
|---|---|---|
| All 100 levels | 50 hours | Complete playthrough, note any issues |
| All 10 bosses | 10 hours | Kill each boss 3 times (different power-ups) |
| All power-ups | 5 hours | Collect and use each power-up in 3 different levels |
| All enemies | 5 hours | Kill each enemy type 5 times |
| All secret exits | 3 hours | Find and use each secret exit |
| All warp zones | 1 hour | Test each warp zone |
| All menus | 2 hours | Navigate every menu, test all options |
| Save/Load | 1 hour | Save at every possible point, load and verify |

### Edge Case Testing

| Category | Edge Cases |
|---|---|
| Physics | Frame drops (15-60 FPS), teleport through thin walls, edge-of-map behavior |
| Collision | Corner cases, overlapping entities, zero-width gaps |
| Spawning | Spawn at level start, checkpoint, pipe exit, vine climb |
| Death | Die at every possible frame (animation cancel, power-up loss timing) |
| Memory | Load 100 levels in one session, rapid save/load, alt-tab |
| Input | Alt-tab while holding keys, controller disconnect/reconnect |
| Audio | 100+ SFX in 1 second, music crossfade during level transition |
| Save | Corrupt save file, delete save mid-save, rapid save spam |

### Platform Testing

| Platform | What to Test |
|---|---|
| Windows 10 | Full test pass |
| Windows 11 | Full test pass |
| macOS (Intel) | Full test pass (different GPU driver) |
| macOS (Apple Silicon) | Rosetta 2 compatibility, native build |
| Linux (Ubuntu 22.04) | Full test pass (Mesa vs proprietary drivers) |
| Linux (Steam Deck) | Controller, performance, suspend/resume |

---

## Bug Tracking

### Severity Levels

| Level | Label | Response Time | Example |
|---|---|---|---|
| Critical | P0 | Fix immediately, block release | Crash, save corruption, softlock, cannot complete level |
| High | P1 | Fix within 24 hours | Broken boss phase, wrong collision, wrong item drop |
| Medium | P2 | Fix before next build | Visual glitch, wrong sound, minor physics issue |
| Low | P3 | Fix before release | Typo in UI, wrong color, missing particle effect |
| Wishlist | P4 | Future release | Feature request, polish suggestion |

### Bug Report Template

```
## Title
[Short description]

## Severity
P0 / P1 / P2 / P3 / P4

## Environment
- Platform: [Windows/macOS/Linux]
- Build: [commit hash or version]
- Settings: [Low/Medium/High/Ultra]

## Steps to Reproduce
1. Go to [level/world/menu]
2. [Action]
3. [Action]
4. See error

## Expected Behavior
[What should happen]

## Actual Behavior
[What actually happens]

## Screenshots / Video
[Link or attachment]

## Notes
[Additional context]
```

---

## Playtesting Process

### Phases

| Phase | Participants | Focus |
|---|---|---|
| Alpha | Dev team only | Core mechanics, first 3 worlds, 5 bosses |
| Closed Beta | 20-50 external testers | Full game, bug hunting, balance feedback |
| Open Beta | 100-500 testers | All content, performance on varied hardware |
| Release Candidate | All testers | Final polish, no new bugs |

### Playtest Feedback Form

```
## Session Info
Levels played:
Time played:
Hardware specs:

## Rating (1-5)
- Fun: /5
- Difficulty: /5 (1=too easy, 5=too hard)
- Controls: /5
- Visuals: /5
- Audio: /5

## Open Questions
1. Did you encounter any bugs? (describe)
2. Which level was the most fun? Why?
3. Which level was the least fun? Why?
4. Did any section feel unfair?
5. Was the difficulty curve appropriate?
6. Anything you'd change?
7. Additional comments:
```

---

## Level Validation

### Automated Checks (on save)
- Spawn point exists within level bounds
- Goal exists and is reachable
- No floating tiles (unless intentional)
- All pipes have valid destinations
- All secret exits lead to valid levels
- At least minimum required coins and enemies
- No dead ends (unless intentional)
- Checkpoint before boss room (castle levels)

### Runtime Checks
- No entity spawns inside solid geometry
- Player can always reach goal (theoretical path exists)
- Star Coins are reachable (not inside walls)
- No infinite loops in warp/pipe chains

---

## Crash & Error Reporting

### In-Game Error Handler
- All exceptions caught at top level
- Error dialog: "Sorry! Something went wrong."
- Options: Restart Level / Return to Title / Quit
- Crash dump saved to `%APPDATA%/MarioGame/crashes/`
- Dump includes: stack trace, last 100 inputs, current level, FPS, memory

### Telemetry (Optional, opt-in)
- Anonymous usage data:
  - Levels played, completion rate
  - Average deaths per level
  - Power-up usage frequency
  - Settings/preferences
- No personal information collected
- No game performance data collected without consent
- Opt-in prompt on first launch

---

## Test Automation Infrastructure

| Tool | Purpose |
|---|---|
| NUnit / xUnit | Unit tests |
| CI pipeline | GitHub Actions / Jenkins |
| Automated level test | Custom tool (play level with scripted inputs) |
| Performance benchmark | Frame time capture + CSV export |
| Regression suite | Baseline + diff comparison |
| Memory profiler | Track allocations per scene |

---

## Quality Gates

### Pre-Merge (PR)
- [ ] All unit tests pass
- [ ] No new compiler warnings
- [ ] Automated playtest passes (10 representative levels)
- [ ] No regression in frame time (> 5% slower = flag for review)

### Pre-Build (Daily)
- [ ] All integration tests pass
- [ ] All 100 levels load without errors
- [ ] Memory stays within budget (±10%)
- [ ] FPS stays within target on reference hardware

### Pre-Release
- [ ] Full manual regression pass complete
- [ ] No P0/P1 bugs open
- [ ] All platforms tested (Windows, macOS, Linux)
- [ ] Performance meets targets on minimum spec hardware
- [ ] 100 levels playable start to finish without crash
