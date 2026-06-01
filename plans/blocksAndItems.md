# Blocks & Items — Design & Rules

## Overview
All interactive blocks and items in the game. Blocks are the primary way Mario interacts with the world beyond movement and combat. Each block has a type, behavior, and drop table.

---

## Block Types

### ? Block (Question Block)
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid (can stand on, hit from below) |
| Hit animation | Bumps up 8px, shakes, returns |
| Empty animation | Turns dark grey, no longer interactive |
| Sound | Low thud (hit), hollow thud (empty) |

**Drop Table:**

| Condition | Item | Weight |
|---|---|---|
| Mario is Small | Super Mushroom | 100% |
| Mario is Super/Fire | Coin (10x streak counter) | 60% |
| Mario is Super/Fire | Fire Flower | 30% |
| Mario is Super/Fire | 1-Up Mushroom | 10% |
| In World 9+ | Boo Mushroom replaces 1-Up | 10% |
| Star block (flag) | Starman | 100% |

- Multi-coin blocks: some ? blocks give 3–10 coins (determined by level designer)
- Once empty, stays empty permanently

### Brick Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid (can stand on, hit from below) |
| Breakable | Yes — by Super/Fire Mario from below, or by Mega Mario on contact |
| Small Mario | Cannot break — bounces off (bonks head) |
| Content | May contain item (invisible, no visual difference) |

**Drop Table (if flagged as containing item):**

| Condition | Item | Weight |
|---|---|---|
| Mario is Small | Super Mushroom | 60% |
| Any | Coin | 30% |
| Any | 1-Up Mushroom | 10% |

- When broken: 4 debris pieces fly out, particles, 0.3s animation
- When contains item: item spawns above, brick remains intact
- Debris despawns after 2s or when off-screen

### Hidden Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Invisible until hit — then becomes visible solid block |
| Visual | Slightly shimmering outline (barely visible) |
| Contains | Always 1 item (determined by level designer) |

**Typical Contents:**
- Super Mushroom (common)
- 1-Up Mushroom (rare, secret areas)
- Coin (very common)
- Starman (very rare)
- Vine (if flagged)

### Floor Block (Ground)
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid (all sides) |
| Breakable | No |
| Visual | World-themed ground tile |

### Coin Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid |
| Content | 3–10 coins (dispensed one at a time) |
| Animation | Coin shoots upward, bounces, disappears |
| Empty | Stops dispensing, block turns grey |

### Pipe
| Property | Value |
|---|---|
| Size | 64 x N px (variable height) |
| Direction | Up, down, left, right (entrance direction) |
| Transport | Warps Mario to another pipe location |
| Collision | Solid from all sides except entrance direction |
| Enter | Press Down on top of upward pipe, or Walk into horizontal pipe |

### Donut Block
| Property | Value |
|---|---|
| Size | 64 x 16 px |
| Collision | Solid (stand on) |
| Behavior | Falls after 0.5s of standing on it |
| Respawn | Returns to original position after 2s |

### Falling Platform
| Property | Value |
|---|---|
| Size | 64 x 16 px |
| Collision | Solid (stand on) |
| Behavior | Shakes for 1s, then crumbles and falls |
| Respawn | Does not respawn (one-time) |

### Ice Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid |
| Surface | Slippery (reduced friction, 0.1x normal) |
| Breakable | Yes — by fireball or fire flower attack |

### Note Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid |
| Bounce | When hit from below or jumped on: launches Mario upward (300 px/s) |
| Sound | Musical note (pitch varies by color) |
| Multi-bounce | Can bounce unlimited times |

### Hard Block
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Collision | Solid |
| Breakable | No (indestructible) |
| Visual | Dark stone, metal, or world-themed unbreakable material |

### Semisolid Platform
| Property | Value |
|---|---|
| Size | 64 x 16 px (or wider) |
| Collision | Only from above (can jump through from below) |
| Solid | Can stand on top |
| Visual | Wood, cloud, or world-themed |

### Crumbling Bridge
| Property | Value |
|---|---|
| Size | 64 x 24 px per segment |
| Collision | Solid |
| Behavior | Each segment crumbles 0.5s after stepped on |
| Sequence | Crumbles left to right (chain reaction) |
| Respawn | No |

---

## Item Types

### Coin
| Property | Value |
|---|---|
| Size | 24 x 24 px |
| Behavior | Spinning animation, floats in place or spawns from blocks |
| Collect | Touch to collect |
| Effect | +200 points, +1 coin counter |
| 100 coins | +1 life |
| Sound | Bright ding (ascending pitch per coin in streak) |

### Star Coin (Special)
| Property | Value |
|---|---|
| Size | 32 x 32 px |
| Color | Gold with star emblem |
| Location | 3 per level (hidden or challenge areas) |
| Collect | Touch to collect |
| Effect | +1 star coin (unlockable content) |
| 100 star coins | Unlock bonus world / gallery / concept art |

### Fire Flower
| Property | Value |
|---|---|
| Size | 32 x 32 px (item), 40 x 40 px (flower form) |
| Behavior | Idles in ? block, rises when block hit |
| Effect | Mario can shoot fireballs |
| Fireball | 300 px/s, bounces 3 times, kills most enemies |
| Collect | Touch to collect |

### Starman
| Property | Value |
|---|---|
| Size | 32 x 32 px |
| Behavior | Spinning, shimmering rainbow |
| Effect | 10s invincibility + 20% speed boost |
| Collect | Touch to collect |
| Sound | Starman theme plays during effect |

### Vine
| Property | Value |
|---|---|
| Size | 16 x 16 px per segment |
| Behavior | Grows upward from block when hit (tall) |
| Climb | Press Up on vine to climb |
| Max height | 400–800 px (varies by level) |
| End | Usually leads to secret area or bonus room |

### Trampoline / POW Block
| Property | Value |
|---|---|
| Size | 64 x 16 px |
| Behavior | Jump on it: bounces Mario high (2x jump height) |
| Ground pound | Activates screen-wide shockwave (stuns all grounded enemies) |
| Uses | 3 uses then disappears |

### Checkpoint Flag
| Property | Value |
|---|---|
| Size | 32 x 128 px |
| Behavior | Touch to activate checkpoint |
| Effect | Saves progress, flag raises, fanfare |
| Death respawn | Returns to last checkpoint |
| Visual | Flag goes from bottom to top of pole |

### Goal Flag / Axe
| Property | Value |
|---|---|
| Size | 32 x 200 px (flagpole), 32 x 32 px (axe) |
| Behavior | Touch to complete level |
| Effect | Score bonus based on height of touch + time remaining |
| Level complete | Triggers victory animation, next level unlock |

### Springs (Green / Red)
| Property | Value |
|---|---|
| Size | 40 x 32 px |
| Green | Bounces Mario 2x jump height |
| Red | Bounces Mario 4x jump height |
| Launch | Can charge by holding Down before bouncing |

---

## Block Interaction Rules

### Hitting Blocks From Below
- Mario must be touching the block's underside
- Only works when moving upward (jumping)
- Small Mario cannot break bricks (bounces off)
- Super/Fire Mario breaks bricks on upward hit
- All item blocks dispense on upward hit
- Hidden blocks only work from below or side

### Standing On Blocks
- Mario can stand on any solid block
- Slopes: Mario can stand on slopes up to 60°
- One-way platforms: only from above

### Block States
| State | Description |
|---|---|
| Active | Default state, full interaction |
| Hit | Brief animation (0.2s), item dispensing |
| Empty | Already used, no item, visual change (dark/empty) |
| Broken | Destroyed (bricks only), gone permanently |
| Hidden | Invisible until first interaction |

---

## Item Spawning Rules

- Items from blocks have priority: if Mario is Small, ? blocks always give Super Mushroom
- If multiple items would spawn (e.g. coin and item), coin is skipped
- Items despawn after 15s (except checkpoints and goal items)
- Maximum 5 loose items on screen at once (prevents overflow)
- Items cannot spawn inside solid geometry (check against tilemap)
- Items pushed into walls reverse direction

---

## Item Despawn Rules

| Item | Despawn Time |
|---|---|
| Super Mushroom | 15s |
| 1-Up Mushroom | 12s |
| Poison Mushroom | 15s |
| Fire Flower | 10s |
| Starman | 8s (if not collected) |
| Coin | 5s (if spawned, not from block) |
| Vine | Persists until level ends |
| Shell | 20s |
| Loose items on ground | 10s |

---

## Level Editor Block Palette

| Block | Category | Notes |
|---|---|---|
| ? Block | Interactive | Configure item drop, coin count |
| Brick | Interactive | Configure contains item or empty |
| Hidden Block | Interactive | Configure item, visibility toggle |
| Coin Block | Interactive | Configure coin count (3-10) |
| Floor | Solid | World-themed ground |
| Hard Block | Solid | Indestructible |
| Ice Block | Solid | Slippery surface |
| Semisolid | Solid | One-way platform |
| Donut Block | Moving | Timed fall |
| Falling Platform | Moving | One-time crumble |
| Note Block | Interactive | Musical bounce |
| Pipe | Transport | Configure destination |
| Crumbling Bridge | Moving | Chain crumble |
| Vine | Item | Grows from block |
| Checkpoint | Special | Mid-level save |
| Goal Flag | Special | Level end |
| Trampoline | Interactive | Configure bounce height |
| Spring | Interactive | Green or red |
