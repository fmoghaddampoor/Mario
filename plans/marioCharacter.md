# Mario Character — Design & Mechanics

## Concept
Modern, expressive Mario with smooth hand-drawn 2D animation. Full-bodied (not chibi), with articulated limbs, fluid motion, and detailed facial expressions. Inspired by the look and feel of modern 3D Mario translated into 2D.

---

## Visual Design

### Appearance
- **Height**: ~128 px (idle standing)
- **Proportions**: Realistic head-to-body ratio (not oversized head)
- **Outfit**: Iconic red cap with M logo, red shirt, blue overalls, brown shoes, white gloves
- **Face**: Expressive eyes (blink, squint, wide), mouth (smile, open, determined, hurt)
- **Cap**: Fabric physics — brim flops during movement, flies off on death
- **Overall straps**: Slight bounce during jump/land

### Power-Up Visual Variants

| Power-Up | Visual Changes |
|---|---|
| Super Mushroom | Scales to 128 px height (1.6x small), slightly wider, clothes stretch |
| Fire Flower | Same as Super, white gloves with cuffs, fire emblem on back |
| Starman | Rainbow shimmer shader, sparkle particles, glowing trail |
| Mega Mushroom | Grows to 300+ px, cracks ground, squishes enemies by touch |

---

## Animations

### Full Animation List

| State | Frames | Looping | Notes |
|---|---|---|---|
| Idle | 30 (1s) | Yes | Breathing, slight bounce, blinks every 5s, looks around after 10s |
| Walk | 12 | Yes | Full stride, arms swing, cap bobs |
| Run | 8 | Yes | Lean forward, longer stride, cap flaps back |
| Skid / Stop | 6 | No | Slides to halt, dust particles, arms flail |
| Jump (squat) | 4 | No | Crouch anticipation |
| Jump (ascent) | 6 | No | Arms up, legs tucked |
| Jump (apex) | 4 | No | Mid-air suspended moment |
| Jump (descent) | 6 | No | Arms down, legs extended |
| Jump (land) | 4 | No | Impact squat, dust puff |
| Double jump | 10 | No | Flip / spin, arms tucked |
| Wall jump | 8 | No | Push-off animation, legs kick wall |
| Crouch | 6 | No (hold) | Squats down, hands on ground |
| Crouch walk | 8 | Yes | Shuffle while crouched |
| Slide | 12 | Yes | Sliding on ground, cap flaps |
| Swim | 16 | Yes | Freestyle stroke, kicks |
| Climb (vine) | 8 | Yes | Hand-over-hand, legs dangle |
| Climb (ladder) | 6 | Yes | Steady climb, head bob |
| Push | 10 | Yes | Leaning into block, straining |
| Hurt | 8 | No | Stumble back, cap flies off (returns) |
| Death | 20 | No | Cap flies up → boneless collapse → slide off screen |
| Fire throw | 8 | No | Wind up → throw → follow-through |
| Victory | 30 | No | Fist pump → jump → cap toss → catch (on level clear) |
| Sleep | 20 | Yes | (idle too long) Nod off, snore, wake with start |

### Animation Blending
- Crossfade between states (0.1s transition)
- Can interrupt most animations with higher-priority ones (e.g. jump cancels walk instantly)
- Death animation is un-interruptible

---

## Controls

### Input Mapping

| Action | Keyboard | Controller |
|---|---|---|
| Move Left | A / Left Arrow | Left stick / D-pad left |
| Move Right | D / Right Arrow | Left stick / D-pad right |
| Jump | Space / Up Arrow | A button (Xbox) / Cross (PS) |
| Run (hold) | Left Shift | Right Trigger |
| Crouch / Slide | S / Down Arrow | B button (Xbox) / Circle (PS) |
| Fire / Action | J / Z | X button (Xbox) / Square (PS) |
| Pause | Escape | Start / Menu |

### Control Feel Settings
- All bindings rebindable in Options menu
- Controller vibration supported
- Analog stick sensitivity adjustable

---

## Movement Mechanics

### Ground Movement

| Stat | Walk | Run |
|---|---|---|
| Max speed | 200 px/s | 400 px/s |
| Acceleration | 800 px/s² | 1200 px/s² |
| Friction / Deceleration | 600 px/s² | 800 px/s² |
| Skid threshold | N/A | Releasing direction while running triggers skid |

- Instant direction flip (no turn-around delay when walking)
- Skid animation + slower turn when reversing from run
- Crouch reduces hitbox height by 50%

### Jump Mechanics

| Stat | Value |
|---|---|
| Jump velocity (initial) | -600 px/s |
| Jump velocity (hold) | -300 px/s (gravity reduced while holding jump) |
| Gravity | 1500 px/s² |
| Double jump velocity | -500 px/s |
| Wall jump velocity | 350 px/s horizontal, -450 px/s vertical |
| Variable height | Release jump early → lower arc (min height ~64 px) |
| Max height (full jump) | ~250 px |
| Max height (double jump) | ~400 px |

### Jump Feel Enhancements
- **Coyote time**: 6 frames (100ms) after leaving ground — jump still works
- **Jump buffer**: 6 frames (100ms) before landing — jump input queued
- **Variable height**: Hold jump = go higher, release = cut short
- **Gravity while falling**: Slightly higher than while ascending (heavier fall feel)
- **Landing lag**: 3 frames of reduced speed on landing (weight)

### Air Control
- 60% of ground acceleration while airborne
- Full horizontal speed retained (no momentum kill on jump)
- Can change direction mid-air

### Slope Interaction
- Walk up slopes up to 45° (speed reduced by 30%)
- Run up slopes up to 60°
- Slide down steep slopes (≥45°) if not moving upward
- Jump on slope = jump perpendicular to slope surface

---

## Power-Up System

### Mushroom (Super Mario)
- **Acquisition**: Hit ? block, mushroom rises and scrolls
- **Effect**: Grows Mario to 128 px (1.6x small size)
- **Stats**: Same speed, larger collision box, can break bricks from below
- **Hit while Super**: Shrink to Small Mario (invincibility frames 2s)
- **Visual**: Size change, clothes stretch

### Fire Flower
- **Acquisition**: Hit ? block as Super Mario
- **Effect**: Can shoot fireballs
- **Fireball**: 300 px/s, bounces off walls/floors, lasts 2s, kills most enemies
- **Hit while Fire**: Shrink to Super Mario (invincibility frames 2s)
- **Visual**: White gloves, fire emblem

### Starman
- **Acquisition**: Hit ? block (rare)
- **Effect**: 10 seconds of invincibility
- **Movement**: 20% speed boost, enemies die on touch
- **Visual**: Rainbow shader, sparkle particles, music changes
- **Expiration**: Flashing warning at 3 seconds, then normal

### Power-Up Stacking
- Only one power-up active at a time
- Mushroom → Fire = upgrade (keep fire)
- Fire → Mushroom = downgrade (to super)
- Taking damage goes down one level: Fire → Super → Small → Death
- Star overrides all other power-ups visually; after star expires, previous power-up returns

---

## Collision

### Hitbox Sizes

| State | Width | Height |
|---|---|---|
| Small idle | 40 px | 80 px |
| Small crouch | 40 px | 40 px |
| Super / Fire idle | 40 px | 128 px |
| Super / Fire crouch | 40 px | 64 px |
| Slide | 80 px | 40 px |

- Hurtbox matches collision box
- Invincibility frames: 2 seconds after taking damage
- During i-frames: Mario blinks (150ms interval), enemies don't damage

### Interaction Hitboxes
- **Head stomp**: 16px wide, 8px tall, at top of Mario — triggers enemy stomp
- **Bottom stomp**: 16px wide, 8px tall, at bottom — blocks break from below
- **Grab box**: 60px wide, 40px tall, in front — for picking up shells / items

---

## Lives & Health

| Property | Value |
|---|---|
| Max health levels | 3 (Small, Super, Fire) |
| Lives start | 3 |
| 1-Up per | 100 coins |
| 1-Up from | Green mushrooms, hidden blocks |
| Death | All power-ups lost, restart from checkpoint |
| Game over | 0 lives → continue screen (3 retries) → title screen |

### Checkpoint System
- Midway flag in each level
- Activates on touch (animates flag, plays fanfare)
- Death returns to last checkpoint (not start)
- Checkpoints persist per level until completed

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
| Block hit | 100 |
| 1-Up mushroom | +1 life |
| Level clear flag | 1000 + (time remaining × 10) |
| Secret area bonus | 2000 |

- Score displayed in HUD
- High score saved to file
- Combo multiplier: stomp multiple enemies in succession without touching ground → 1.5x, 2x, 3x...

---

## Visual Polish Details

- **Cap physics**: Mario's cap has 2 bones that simulate fabric/flopping independent of head
- **Overall straps**: Small physics bones — bounce during movement
- **Shoes**: Squash on landing, stretch on jump
- **Blink timer**: Blinks every 4-6 seconds (randomized)
- **Look-around**: If idle > 8 seconds, looks at screen then direction of nearest enemy
- **Breath mist**: In ice world, breath visible
- **Water droplets**: Drip particles after swimming
- **Dust particles**: On landing, skidding, breaking blocks
