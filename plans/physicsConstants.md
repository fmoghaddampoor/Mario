# Physics Constants — Centralized Reference

## Overview
Single source of truth for all physics and movement values. All engine code should reference these constants to prevent drift between design and implementation.

---

## Mario Movement

| Constant | Value | Notes |
|---|---|---|
| Small Mario height | 80 px | Collision box |
| Small Mario width | 40 px | Collision box |
| Super Mario height | 128 px | +48px from Small |
| Super Mario width | 40 px | Same as Small |
| Walk max speed | 200 px/s | |
| Run max speed | 400 px/s | Hold run button |
| Walk acceleration | 800 px/s² | |
| Run acceleration | 1200 px/s² | |
| Walk friction | 600 px/s² | Deceleration when releasing input |
| Run friction | 800 px/s² | |
| Skid threshold | 300 px/s | Run speed below this = no skid |
| Crouch height reduction | 50% | Small: 40px, Super: 64px |
| Crouch width | 40 px | Same as standing |
| Slide width | 80 px | |
| Slide height | 40 px | |

---

## Jump Physics

| Constant | Value | Notes |
|---|---|---|
| Jump initial velocity | -600 px/s | Upward (negative Y) |
| Jump hold velocity | -300 px/s | Reduced gravity while holding |
| Gravity | 1500 px/s² | |
| Fall gravity multiplier | 1.2x | Heavier fall feel (1800 px/s²) |
| Double jump velocity | -500 px/s | |
| Wall jump horizontal | 350 px/s | Away from wall |
| Wall jump vertical | -450 px/s | |
| Min jump height | 64 px | Tapping jump |
| Max jump height (Small) | 250 px | Holding jump |
| Max jump height (Super) | 250 px | Same as Small |
| Max double jump height | 400 px | |
| Coyote time | 100 ms | 6 frames at 60fps |
| Jump buffer | 100 ms | 6 frames at 60fps |
| Landing lag | 50 ms | 3 frames reduced speed |
| Air control multiplier | 0.6x | 60% of ground acceleration |

---

## Slope Physics

| Constant | Value | Notes |
|---|---|---|
| Max walkable slope | 45° | |
| Max runnable slope | 60° | |
| Slope speed reduction | 30% | Walking up slope |
| Auto-slide slope | 45° | Must be moving upward to not slide |
| Slide speed | 200 px/s | Sliding down steep slope |

---

## Enemy Movement

| Constant | Value |
|---|---|
| Goomba speed | 40 px/s |
| Koopa speed | 50 px/s |
| Koopa shell speed | 300 px/s |
| Paratroopa speed | 60 px/s |
| Buzzy Beetle speed | 35 px/s |
| Spiny speed | 30 px/s |
| Lakitu follow speed | Matches Mario |
| Pokey speed | 20 px/s |
| Cheep Cheep speed | 80 px/s |
| Blooper speed | 50 px/s |
| Monty Mole speed | 50 px/s |
| Wiggler speed (calm) | 40 px/s |
| Wiggler speed (angry) | 120 px/s |
| Freezeball speed | 70 px/s |
| Dry Bones speed | 35 px/s |
| Fire Bro speed | 40 px/s |
| Boo speed | 60 px/s |
| Hammer Bro speed | 30 px/s |
| Bowser speed (walk) | 80 px/s |
| Bowser speed (charge) | 150 px/s |
| Bowser speed (enraged) | 200 px/s |
| Banzai Bill speed (launch) | 200 px/s |

---

## Projectile Physics

| Constant | Value | Notes |
|---|---|---|
| Fireball speed | 300 px/s | Bounces off walls/floors |
| Fireball lifetime | 2s | |
| Fireball bounces | 3 | Before dissipating |
| Ice ball speed | 250 px/s | From Penguin suit |
| Ice ball freeze duration | 3s | |
| Ice Bro ice ball speed | 200 px/s | |
| Hammer Bro hammer speed | 180 px/s | Arc trajectory |
| Shell speed (kicked) | 300 px/s | |
| Shell lifetime | 20s | Despawns after |
| Spiny drop speed | 80 px/s | From Lakitu |

---

## Item Movement

| Constant | Value | Notes |
|---|---|---|
| Super Mushroom slide speed | 80 px/s | |
| 1-Up Mushroom slide speed | 80 px/s | |
| Poison Mushroom slide speed | 80 px/s | |
| Mini Mushroom slide speed | 120 px/s | Faster than normal |
| Propeller Mushroom speed | 80 px/s | |
| Penguin Mushroom speed | 80 px/s | |
| Item despawn time | 15s | General items |
| 1-Up despawn time | 12s | |
| Fire Flower despawn time | 10s | |
| Starman despawn time | 8s | |
| Coin despawn time | 5s | Loose coins |

---

## Power-Up Duration

| Constant | Value | Notes |
|---|---|---|
| Starman duration | 10s | Flashes at 3s remaining |
| Mega Mushroom duration | 15s | |
| Boo Mushroom duration | 10s | |
| Star invincibility | 10s | Same as Starman |
| Invincibility frames (damage) | 2s | Blinks every 150ms |
| Propeller flight time | 2s | Per ground touch |
| Bee flight time | 3s | Per ground touch |

---

## Power-Up Stats

| Constant | Value | Notes |
|---|---|---|
| Starman speed boost | 20% | Applied to max speed |
| Mini speed boost | 20% | Applied to max speed |
| Mega speed reduction | 30% | Slower |
| Penguin swim speed | 150% | Of normal swim |
| Penguin slide speed | 250 px/s | |
| Mega jump height | 250 px | Same as normal |
| Mini jump height | 60% | Of normal jump |

---

## Tile / Block Physics

| Constant | Value |
|---|---|
| Tile size | 64 x 64 px |
| ? block hit bump | 8px upward |
| Block bump animation | 0.2s |
| Brick debris lifetime | 2s |
| Hidden block shimmer | Barely visible outline |
| Donut block fall delay | 0.5s |
| Falling platform shake time | 1s |
| Ice block friction | 0.1x normal |
| Note block launch speed | 300 px/s |
| Semisolid platform height | 16px |
| Pipe segment height | 64px |

---

## Camera

| Constant | Value | Notes |
|---|---|---|
| Camera smooth follow | 0.1s | Lerp time |
| Camera dead zone | 48 px | No movement within zone |
| Camera look-ahead | 96 px | Scroll ahead of Mario |
| Parallax far layer | 0.1x | Skybox |
| Parallax mid layer | 0.3x | Distant terrain |
| Parallax near layer | 0.6x | Near background |
| Screen shake (max) | 8 px | On Mega stomp |
| Screen shake duration | 0.3s | |

---

## Physics Engine

| Constant | Value | Notes |
|---|---|---|
| Physics timestep | 240 Hz | Fixed timestep (4.16ms) |
| Physics sub-steps | 4 | 60fps game loop × 4 |
| Gravity constant | 1500 px/s² | |
| Maximum velocity | 2000 px/s | Clamp to prevent tunneling |
| Spatial hash cell size | 64 px | Broad phase |
| Max entities on screen | 20 | |
| Max projectiles | 10 | |
| Entity despawn distance | 2 screens | Behind Mario |
| Entity spawn distance | 1 screen | Ahead of Mario |

---

## Level Timers

| Constant | Value |
|---|---|
| Standard level time | 300s |
| Fortress/Tower time | 200s |
| Castle level time | 500s (no boss timer pressure) |
| Underwater breath time | 30s |

---

## Scoring

| Action | Points |
|---|---|
| Stomp Goomba | 100 |
| Stomp Koopa | 200 |
| Stomp other enemies | 400–1000 |
| Fireball kill | 200 |
| Star kill | 1000 |
| Coin | 200 |
| Brick break | 50 |
| Block hit (empty) | 100 |
| 1-Up | +1 life |
| Level clear flag | 1000 + (time × 10) |
| Secret area | 2000 |
| Star Coin | 3000 |

---

## Resolution & Display

| Constant | Value |
|---|---|
| Native render resolution | 1920 x 1080 |
| Sprite resolution | 128–256 px per object |
| Tile size | 64 x 64 px |
| UI safe zone margin | 100px |
| Aspect ratio | 16:9 |

---

## Multipliers & Conversions

| Constant | Value |
|---|---|
| Pixels per meter (for physics) | 64 px = 1 m |
| Frame time (60fps) | 16.67 ms |
| Frame time (30fps) | 33.33 ms |
| Max delta time | 50 ms (clamp to prevent spiral) |
| Time scale (normal) | 1.0 |
| Time scale (pause) | 0.0 |
