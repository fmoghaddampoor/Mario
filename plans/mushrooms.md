# Mushrooms & Power-Up Items — Detailed Design

## Overview
Mushrooms are the primary power-up items in the game. They appear from ? blocks, hidden blocks, or as enemy drops. Each mushroom has distinct behavior, visual, and gameplay effect.

---

## Super Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Grows Mario from Small → Super |
| Color | Red cap with white spots, tan stem |
| Rarity | Common |

### Behavior
- Emerges from ? block and rises upward
- After full emergence, slides to the right at 80 px/s
- Falls off ledges (gravity affected)
- Bounces off solid walls (reverses direction)
- Despawns after 15 seconds if not collected
- Cannot be killed or destroyed

### Collection
- Touch = immediate effect
- If Small Mario: grows to Super (increase height by 48px)
- If Super/Fire Mario: grants 1000 points (no growth)
- If Star Mario: adds 1000 points, no override

### Animation
- Emerge: rises from block over 0.5s, wiggling
- Walk: wobbles side to side while moving
- Idle: gentle pulse (slight scale oscillation)

### Sound
- Emerge: ascending magical shimmer (C major arpeggio)
- Move: soft bouncy thuds
- Collect: triumphant ascending chime

---

## 1-Up Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Grants one extra life |
| Color | Green cap with white spots, tan stem |
| Rarity | Uncommon (1–3 per world) |

### Behavior
- Identical movement to Super Mushroom (emerges, slides, falls)
- Despawns after 12 seconds
- Same physics as Super Mushroom
- Appears from hidden blocks, ? blocks, or as rare enemy drop (1% chance from stomping)

### Collection
- Touch = +1 life, shown in HUD with animation
- Brief invincibility on collect (like a star, 1 second)

### Animation
- Same as Super Mushroom but with a sparkle particle effect
- Green shimmer aura

### Sound
- Collect: extended happy chime + "1-UP" voice sample (optional)
- Star counter increments with sparkle sound

### Placement Guidelines
- Hidden blocks in secret areas
- Rewards for risky exploration (over gaps, near enemies)
- Never placed in plain sight — always requires some discovery

---

## Poison Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Damages Mario (loses power-up level) |
| Color | Purple cap with skull icon, dark stem |
| Rarity | Rare (trap item) |

### Behavior
- Identical movement to Super Mushroom
- Emerges from ? blocks that look normal (no visual cue)
- Also appears from hidden blocks in trap locations

### Detection
- No visual difference until emerged (unavoidable trap)
- Experienced players can notice: slight purple tint on block before hitting
- Once emerged, skull icon clearly visible

### Effect on Collection
- Small Mario: dies (loses life)
- Super Mario: shrinks to Small
- Fire Mario: shrinks to Super
- Star Mario: no effect (star immunity)

### Animation
- Same wobble as Super Mushroom
- Dark purple smoke particles trail
- Skull icon on cap pulses slowly

### Sound
- Emerge: dark descending tone
- Move: heavy, wet squelch
- Collect: dissonant crash + damage grunt

### Placement Guidelines
- Maximum 1–2 per world
- Placed in areas that look like rewards (hidden blocks in tricky jumps)
- Always avoidable with careful play (can be seen after first encounter)
- Never in mandatory paths

---

## Mega Mushroom

| Property | Value |
|---|---|
| Size | 48 x 48 px (item form) |
| Effect | Mario grows to giant size temporarily |
| Color | Red cap with white spots, metallic gold outline |
| Rarity | Rare (1–2 per world) |

### Behavior
- Emerges from giant ? block (2x size, gold outline)
- Does not move — sits on block, pulsating
- Despawns after 10 seconds
- Glowing gold aura

### Effect on Collection
- Mario grows to 300+ px tall (3x normal size)
- Duration: 15 seconds
- During effect:
  - Invincible to all enemies and hazards
  - Destroys enemies on touch (no stomp needed)
  - Breaks brick blocks by walking into them
  - Stomping crushes enemies + ground pound creates shockwave
  - Speed: 70% of normal (feels heavy)
  - Camera zooms out to show giant Mario
- Expiration: Mario shrinks back with flash effect

### Animation
- Idle: slow pulse with sparkles
- Emerge: block shakes violently, bursts open
- Giant Mario: heavy footsteps screen shake, squash/stretch exaggerated

### Sound
- Emerge: deep rumbling + explosion
- Giant footsteps: low booms with screen shake
- Destroy: satisfying crunch
- Expiration: descending magical tone + poof

---

## Mini Mushroom

| Property | Value |
|---|---|
| Size | 28 x 28 px (item) |
| Effect | Mario shrinks to tiny size |
| Color | Blue cap with white spots, pale stem |
| Rarity | Uncommon |

### Behavior
- Emerges from ? block normally
- Moves faster than Super Mushroom (120 px/s)
- More bouncy (higher restitution)

### Effect on Collection
- Mario shrinks to 32 px tall (40% normal)
- Duration: Until damaged or level end
- During effect:
  - Jump height: 60% of normal
  - Speed: 120% of normal
  - Hitbox: tiny (can fit through small gaps)
  - Can walk on water surface (doesn't sink)
  - Lighter — floats downward slowly
  - Cannot break bricks (too light)
  - Enemies still kill on touch (except stomp works as normal)
- Taking damage reverts to Small Mario

### Animation
- Mini Mario: quicker, bouncier animations
- Squash/stretch more exaggerated
- Higher-pitched footstep sounds

### Sound
- Collect: high-pitched chime
- Movement: squeaky footsteps
- Land: tiny thud

### Placement
- Secret areas requiring small passages
- Paths over water (walk-on-water property)
- Speed-run optimal paths

---

## Propeller Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Mario gains propeller flight ability |
| Color | Yellow-green cap with propeller symbol, white stem |
| Rarity | Uncommon |

### Behavior
- Emerges from ? block normally
- Movement identical to Super Mushroom
- Spinning propeller animation on item

### Effect on Collection
- Mario gains propeller hat (visual: small propeller on cap)
- Duration: Until damaged or level end
- During effect:
  - Press Jump while in air: propeller spin, Mario rises slowly
  - Propeller duration: 2 seconds of lift per ground touch
  - Recharges: when touching ground
  - Can spin indefinitely over water (no ground needed)
  - Fall speed reduced while spinning
  - Can break bricks from above by spinning into them
  - Stomp works as normal

### Controls
- Jump + hold Jump mid-air: activate propeller
- Release: float down slowly
- Can also ground pound by pressing Down + Jump

### Animation
- Propeller spins on cap (rotation animation, 360°/frame)
- Mario: arms out, legs together, spinning slowly
- Landing: propeller stops spinning

### Sound
- Propeller spin: whirring sound (pitch increases with spin speed)
- Collect: windy whoosh
- Land: click (propeller stops)

---

## Penguin Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Mario gains ice abilities |
| Color | Light blue cap with snowflake pattern, white stem |
| Rarity | Uncommon |

### Behavior
- Emerges from ? block normally
- Movement identical to Super Mushroom
- Snowflake particles trail

### Effect on Collection
- Mario wears penguin suit (visual: white belly, flippers, beak)
- Duration: Until damaged or level end
- During effect:
  - Can slide on ground (Down + Jump): slides at 250 px/s
  - Sliding destroys enemies on contact
  - Can throw ice balls (Press Fire): freeze enemies solid for 3s
  - Frozen enemies can be used as platforms
  - Ice balls shatter on walls
  - Improved traction on ice surfaces
  - Swimming speed: 150% of normal
  - Cannot break bricks (penguin slide does not break blocks)

### Controls
- Fire: throw ice ball
- Down + Jump: belly slide
- While sliding: can jump to maintain momentum

### Animation
- Penguin walk: waddling, arms (flippers) out
- Slide: flat on belly, flippers back
- Ice throw: spinning toss motion
- Swim: smooth dolphin-like

### Sound
- Slide: swoosh on ice
- Ice throw: frost whoosh
- Enemy freeze: crackling ice
- Walk: squeaky footsteps

---

## Bee Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Mario gains flight and wall-cling ability |
| Color | Yellow/orange cap with bee stripes, black stem |
| Rarity | Rare |

### Behavior
- Emerges from ? block normally
- Floats instead of sliding (slow descent, moves horizontally)
- Buzzing sound while waiting

### Effect on Collection
- Mario wears bee suit (visual: bee stripes, wings on back, antennae)
- Duration: Until damaged or level end
- During effect:
  - Can fly freely for 3 seconds (hold Jump)
  - After 3s: wings tire — must touch ground to recharge
  - Can cling to walls and ceilings (press toward surface)
  - While clinging: can jump off walls
  - Wings regenerate after 1s on ground
  - Fall speed reduced (floaty)
  - Cannot stomp enemies (too light)

### Controls
- Hold Jump: fly upward
- Release: float down
- Move toward wall: cling
- Jump while clinging: wall jump

### Animation
- Wings: rapid flutter (4-frame cycle)
- Flight: arms out, legs dangling
- Cling: flat against wall, wings still
- Tired: wings droop, panting

### Sound
- Flight: buzzing wings
- Cling: sticky thud
- Tired: sad buzz decrease
- Collect: happy buzz

---

## Boo Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Mario becomes ghost-like, can phase through enemies |
| Color | White/grey cap with Boo face, translucent stem |
| Rarity | Rare (World 9 only) |

### Behavior
- Emerges from ? block with eerie glow
- Floats slowly, phases through blocks
- Bounces off walls but doesn't stop

### Effect on Collection
- Mario becomes translucent, ghostly
- Duration: 10 seconds
- During effect:
  - Phase through enemies (no damage, can't kill them)
  - Phase through certain walls (marked with Boo symbol)
  - Floating movement (reduced gravity)
  - Cannot collect coins or items (ghost hands)
  - Enemies can't see Mario (Boo enemies don't react)
  - Can pass through grates and bars
- Expiration: fade back to solid

### Animation
- Mario: translucent with slight wobble
- Trail: ghostly after-image

### Sound
- Collect: eerie ghost wail
- Movement: ethereal hum
- Expiration: fading whisper

---

## Gold Mushroom

| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Effect | Maxes out coin counter, grants invincibility for 5s |
| Color | Gold cap with jewel pattern, golden stem |
| Rarity | Extremely rare (1 per world) |

### Behavior
- Emerges from special golden ? block
- Does not move — floats in place, rotating slowly
- Golden sparkle particles
- Despawns after 8 seconds

### Effect on Collection
- Coin counter instantly jumps to 99 (or +50 coins if over 50)
- 5 seconds of star-like invincibility
- Adds gold shimmer to Mario's sprite
- 5000 points bonus

### Animation
- Item: slow rotation in place, sparks
- Collection: golden explosion of coins

### Sound
- Collect: fanfare + casino win jingle
- During effect: golden shimmer loop

---

## Mushroom Behavior Comparison

| Mushroom | Movement | Emerge | Despawn | Collect effect |
|---|---|---|---|---|
| Super | Slide 80 px/s | ? block | 15s | Grow to Super |
| 1-Up | Slide 80 px/s | Hidden/? block | 12s | +1 life |
| Poison | Slide 80 px/s | Trap ? block | 15s | Damage/kill |
| Mega | Stationary | Giant ? block | 10s | Giant 15s |
| Mini | Slide 120 px/s | ? block | 15s | Mini size |
| Propeller | Slide 80 px/s | ? block | 15s | Flight |
| Penguin | Slide 80 px/s | ? block | 15s | Ice powers |
| Bee | Float | ? block | 12s | Flight + cling |
| Boo | Float/phase | ? block (W9) | 10s | Ghost form |
| Gold | Stationary | Golden ? block | 8s | Coins + invincibility |

---

## Mushroom Spawning Rules

- Maximum 1 mushroom visible on screen at a time (per type)
- If a mushroom is active and another spawns, the older one despawns
- Super Mushroom from ? blocks only appears if Mario is Small (otherwise: coin or fire flower)
- 1-Up Mushroom has 10% chance from floor blocks, 50% from hidden blocks
- Poison Mushroom has 20% chance from certain trap blocks (flagged in level editor)
- Mega Mushroom from giant blocks only — no overlap with normal ? blocks

---

## Visual Quality Checklist (per mushroom type)

- [ ] High-res painted sprite (128+ px source)
- [ ] Smooth animation (emerge, idle, move, collect)
- [ ] Particle effects (where applicable)
- [ ] Consistent shading style with game world
- [ ] Readable silhouette
- [ ] Color palette fits the item's purpose (warm for good, cold for bad)
- [ ] Collect animation feels satisfying (screen shake on Mega, sparkle on Gold, etc.)
