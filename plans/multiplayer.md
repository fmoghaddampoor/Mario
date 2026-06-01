# Multiplayer — Design & Specification

## Decision
**v1.0 will be single-player only.** This document defines the architecture and design for potential future multiplayer support in a post-launch update.

---

## Scope for v1.0
- Single-player with one save slot active at a time
- No multiplayer, no co-op, no versus
- Engine architecture will not prevent future multiplayer but will not be built for it

---

## Future Multiplayer Modes (Post-v1.0)

### Mode 1: Local Co-op (2 Players)

#### Concept
- Player 1: Mario (keyboard)
- Player 2: Luigi (controller, or both on controllers)
- Both players on the same screen
- Split-screen only if players are far apart (> 2 screen widths)

#### Luigi Differences
| Stat | Mario | Luigi |
|---|---|---|
| Jump height | 100% | 110% |
| Walk speed | 100% | 95% |
| Acceleration | 100% | 90% |
| Friction | 100% | 85% (slipperier) |
| Hitbox | Standard | Slightly taller (136px Super) |

#### Co-op Mechanics
- Both players share lives pool (each death costs 1 life)
- If one player dies, the other can continue (no instant game over)
- Respawn: dead player respawns at checkpoint when living player touches a ? block
- If both die simultaneously → game over
- Can stand on each other's heads (co-op jump boost)
- Can pick up and throw the other player (teamwork puzzles)
- Star Coins collected count for both

#### Shared Resources
| Resource | Sharing |
|---|---|
| Lives | Shared pool |
| Coins | Shared counter |
| Star Coins | Shared progression |
| Score | Individual (shown side-by-side on HUD) |
| Power-ups | Individual (each player has own power-up state) |

### Mode 2: Versus Mode (2-4 Players)

#### Concept
- Competitive race through a level
- First to reach the flagpole wins
- Players can interact: stomp, grab, throw each other
- Items appear from blocks as normal, but can be stolen

#### Rules
- 2-4 players, each with controller
- All players start simultaneously
- Players can stomp each other (stomped player loses 3s + drops power-up)
- Shells and projectiles affect all players
- Star Coins: +500 bonus points each
- Power-ups spawn in random blocks (not predetermined)
- Score based on: finish position, coins, enemies stomped

#### Scoring
| Action | Points |
|---|---|
| 1st place finish | 5000 |
| 2nd place finish | 3000 |
| 3rd place finish | 2000 |
| 4th place finish | 1000 |
| Stomp another player | 500 |
| Coin collected | 200 |
| Enemy stomp | 200 |

### Mode 3: Online Leaderboards

#### Concept
- Time trial mode for each level
- Upload best time to global leaderboard
- Ghost data: replay of top players displayed as transparent ghost
- Filter: friends, global, region, by power-up type

#### Ghost System
- Record input data (not video) — frame-perfect replay
- Ghost stored as compressed input sequence (~10 KB per level run)
- Ghost playback: rerun physics with recorded inputs
- Ghost rendered as translucent Mario (50% opacity, blue tint)

---

## Technical Architecture for Multiplayer

### Network Model (if online)
- Peer-to-peer for versus (2-4 players, low latency)
- Client-server for leaderboards (REST API)
- Deterministic lockstep for game state sync (if P2P)
- Rollback netcode for responsive feel (if P2P)

### Local Multiplayer
- Same-process: each player is an `InputDevice` mapped to Player 1 / Player 2
- Scene handles two entities with independent inputs
- Camera: average of both players' positions, zoom out if far apart

### Engine Changes Needed
| System | Change |
|---|---|
| Input | Support multiple input devices simultaneously |
| Camera | Dynamic zoom/position for multiple targets |
| Scene | N entities with PlayerController components |
| HUD | Dual HUD for co-op, or per-player score overlay |
| Collision | Player-vs-player collision (versus only) |
| Items | Prevent item duplication on shared screens |
| Save | Co-op save tracks both players' progress |

---

## Decision Log

| Date | Decision | Rationale |
|---|---|---|
| v1.0 | Single-player only | Scope focus. 100 levels, 10 bosses, 20+ enemies is already a massive project. Multiplayer would delay release by 6+ months. |
| Future | Local co-op first | Most requested feature, lowest technical risk. No networking needed. |
| Future | Versus mode | Fun but niche. Lower priority than co-op. |
| Future | Online leaderboards | High value, low effort (REST API + ghost recording). Could ship in first post-launch update. |
