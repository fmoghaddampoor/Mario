# Enemies — Design & Behavior

## Overview
20+ unique enemy types across 10 worlds. Each enemy has distinct behavior, visual design, and sound. Enemies escalate in complexity and danger as worlds progress.

---

## Enemy List

### World 1 — Grassland

#### Goomba
| Property | Value |
|---|---|
| Size | 48 x 48 px |
| Health | 1 hit (stomp or fireball) |
| Speed | 40 px/s |
| Behavior | Walks back and forth. Turns at edges. Falls off ledges. |
| Kill method | Stomp: squishes flat, 0.5s death animation. Fireball: poof. |
| Score | 100 |
| Sound | Soft rhythmic pat (walk), squish + pop (death) |
| Variant | **Goomba (Para)** — Wings, hops up and down, follows Mario |

#### Koopa Troopa
| Property | Value |
|---|---|
| Size | 52 x 64 px |
| Health | 1 hit (stomp → shell) |
| Speed | 50 px/s |
| Behavior | Walks back and forth. Retreats into shell when stomped. |
| Kill method | Stomp → shell mode. Shell can be kicked to kill other enemies. |
| Shell | Slides at 300 px/s, bounces off walls, kills anything it hits. Mario can grab and carry shell. |
| Score | 200 (stomp), 100 per enemy hit by shell |
| Sound | Hollow thwack (shell kick), spin whoosh (shell sliding) |
| Variant | **Koopa (Red)** — Patrols a fixed area, won't fall off edges |

#### Piranha Plant
| Property | Value |
|---|---|
| Size | 48 x 64 px (varies by pipe size) |
| Health | 1 hit (fireball, star, or stomp when emerged) |
| Speed | N/A (stationary) |
| Behavior | Emerges from pipe when Mario is near. Retreats when Mario gets close or after 2s. Cycles: emerge → look → retreat → wait 2s → repeat. |
| Kill method | Fireball from below, stomp on head when emerged, star. |
| Score | 400 |
| Sound | Pop + wet slither (emerge), chomp + snap (bite) |
| Variant | **Venus Fire Trap** — Shoots fireballs in arc pattern |

#### Koopa Paratroopa
| Property | Value |
|---|---|
| Size | 52 x 72 px |
| Health | 1 hit (stomp → Koopa Troopa) |
| Speed | 60 px/s horizontal, 40 px/s vertical |
| Behavior | Flies in fixed pattern (sine wave, oval, or figure-8). Green = simple wave, Red = follows Mario horizontally. |
| Kill method | Stomp removes wings → becomes normal Koopa. Fireball kills outright. |
| Score | 400 |
| Sound | Wing flap loop, thud (wings removed) |

---

### World 2 — Underground

#### Buzzy Beetle
| Property | Value |
|---|---|
| Size | 48 x 44 px |
| Health | Immune to stomp (shell), 1 hit to fireball/star |
| Speed | 35 px/s |
| Behavior | Walks back and forth like Goomba. Hard shell — cannot be stomped. |
| Kill method | Fireball, star, or kicked shell. Stomping turns it into a shell (like Koopa). |
| Score | 200 |
| Sound | Click-clack walk, hard thud on stomp |
| Variant | **Buzzy Beetle (Para)** — Flies, drops down when Mario approaches |

#### Spiny
| Property | Value |
|---|---|
| Size | 48 x 48 px |
| Health | Immune to stomp, 1 hit to fireball/star |
| Speed | 30 px/s |
| Behavior | Walks on ceiling, drops when Mario is below. Spiked shell — stomping kills Mario. |
| Kill method | Fireball, star, kicked shell. Avoid touching from any angle except bottom. |
| Score | 400 |
| Sound | Scuttling claws, sharp spike sound |
| Variant | **Spiny Egg** — Spiny's egg form. Stationary, hatches after 2s. Can be destroyed before hatching. |

#### Lakitu
| Property | Value |
|---|---|
| Size | 48 x 64 px |
| Health | 1 hit (fireball, star, shell) |
| Speed | Follows Mario horizontally, fixed height |
| Behavior | Flies above Mario on a cloud. Follows Mario. Periodically drops Spinies below. |
| Kill method | Fireball, star, shell throw. Cloud remains (can be used as platform). |
| Score | 800 |
| Sound | Laughing chuckle, cloud puff, Spiny drop sound |
| Note | Re-appears if Mario dies and passes same area |

---

### World 3 — Desert

#### Pokey
| Property | Value |
|---|---|
| Size | 48 x 160 px (4 segments) |
| Health | 1 hit per segment (fireball, star) — loses top segment each hit |
| Speed | 20 px/s |
| Behavior | Walks slowly. Segmented body. Only the head (top) can be stomped. Each fireball removes one segment. |
| Kill method | Remove all segments with fireballs, or stomp the head 4 times, or star. |
| Score | 200 per segment |
| Sound | Dry rattle, sand rustle, thud (segment falls) |
| Variant | **Pokey (Snow)** — Ice variant in World 6, white palette |

#### Angry Sun
| Property | Value |
|---|---|
| Size | 64 x 64 px |
| Health | Immune to all attacks |
| Speed | Sudden downward dive |
| Behavior | Sits in background sky. Occasionally dives at Mario when he lingers. Returns to sky after dive. |
| Kill method | Cannot be killed. Evade only. |
| Score | 0 |
| Sound | Whoosh (dive), sizzle, angry face |

#### Bandit
| Property | Value |
|---|---|
| Size | 40 x 44 px |
| Health | 1 hit (stomp, fireball, star) |
| Speed | 60 px/s |
| Behavior | Runs toward Mario. If carrying an item (coin, mushroom), drops it when stomped. |
| Kill method | Stomp, fireball, star. |
| Score | 200 |
| Sound | Sneaky footsteps, surprised yelp on death |

---

### World 4 — Ocean / Beach

#### Cheep Cheep
| Property | Value |
|---|---|
| Size | 48 x 36 px |
| Health | 1 hit (fireball, star, shell) |
| Speed | 80 px/s |
| Behavior | Swims in water. Green = horizontal swimmer. Red = jumps out of water in arc. |
| Kill method | Fireball, star, shell. Can be stomped when above water. |
| Score | 200 |
| Sound | Splash, fish flop |

#### Blooper
| Property | Value |
|---|---|
| Size | 56 x 48 px |
| Health | 1 hit (fireball, star) |
| Speed | 50 px/s |
| Behavior | Swims in sine wave pattern toward Mario. Ink attack: slows Mario for 1s on contact. |
| Kill method | Fireball, star. Stomp works but pushes Mario down. |
| Score | 400 |
| Sound | Squishy ink puff, splat |
| Variant | **Blooper Nanny** — Larger, surrounded by baby Bloopers |

#### Urchin
| Property | Value |
|---|---|
| Size | 36 x 36 px |
| Health | Immune to stomp, 1 hit to fireball/star |
| Speed | 30 px/s |
| Behavior | Stationary or slow-moving spiked ball in water. Pushes toward Mario. Spikes kill on contact. |
| Kill method | Fireball, star, shell. Avoid touching. |
| Score | 200 |
| Sound | Poking sound |

---

### World 5 — Forest

#### Monty Mole
| Property | Value |
|---|---|
| Size | 44 x 40 px |
| Health | 1 hit (stomp, fireball, star) |
| Speed | 50 px/s |
| Behavior | Burrows in ground. Emerges when Mario approaches. Throws dirt clods. |
| Kill method | Stomp when above ground. Fireball. Can't be hit while burrowed. |
| Score | 400 |
| Sound | Digging scuffle, dirt clod whoosh |

#### Wiggler
| Property | Value |
|---|---|
| Size | 64 x 48 px (walks), 128 x 48 px (angry) |
| Health | 5 hits (stomp, fireball, star) — gets angry at 3 hits |
| Speed | 40 px/s (calm), 120 px/s (angry) |
| Behavior | Walks back and forth. Calm and slow. When hit 3 times, turns red, speeds up, becomes aggressive. |
| Kill method | 5 stomps or fireballs. Star kills instantly. |
| Score | 2000 |
| Sound | Gentle hum (calm), angry buzz (angry), squeak on hit |

#### Paragoomba
| Property | Value |
|---|---|
| Size | 48 x 64 px |
| Health | 2 hits (first removes wings → Goomba) |
| Speed | 50 px/s horizontal |
| Behavior | Flies with wings. Higher jump than normal. After losing wings, behaves as normal Goomba. |
| Kill method | Stomp twice, or fireball/star. |
| Score | 400 |
| Sound | Wings flutter, thud on ground |

---

### World 6 — Ice / Mountain

#### Freezeball
| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Health | 1 hit (fireball, star, shell) |
| Speed | 70 px/s (rolls) |
| Behavior | Rolls across ground. Ice element. Freezes Mario on contact (1s stun, loses power-up). |
| Kill method | Fireball, star, shell. Fireball is super effective (vaporizes). |
| Score | 400 |
| Sound | Ice crunch, shatter (death) |

#### Penguin
| Property | Value |
|---|---|
| Size | 52 x 60 px |
| Health | 1 hit (stomp, fireball, star) |
| Speed | 30 px/s (walk), 200 px/s (belly slide) |
| Behavior | Walks slowly. Slips on ice (slides fast). On slopes, slides uncontrollably. |
| Kill method | Stomp, fireball, star. |
| Score | 300 |
| Sound | Slipping slide, honk |

#### Ice Bro
| Property | Value |
|---|---|
| Size | 52 x 64 px |
| Health | 1 hit (stomp, fireball, star) |
| Speed | 40 px/s |
| Behavior | Walks and throws ice balls in arc. Ice balls freeze Mario on contact. |
| Kill method | Stomp, fireball, star. Fireball melts ice balls. |
| Score | 1000 |
| Sound | Ice throw whoosh, freeze crystal sound |

---

### World 7 — Volcano / Lava

#### Lava Bubble
| Property | Value |
|---|---|
| Size | 36 x 36 px |
| Health | Immune to all attacks |
| Speed | 60 px/s |
| Behavior | Jumps out of lava in regular arc pattern. Cannot be killed. |
| Kill method | Cannot be killed. Jump over or time passage. |
| Score | 0 |
| Sound | Lava gurgle, sizzle, pop |

#### Fire Bro
| Property | Value |
|---|---|
| Size | 52 x 64 px |
| Health | 1 hit (stomp, star) — immune to fireball |
| Speed | 40 px/s |
| Behavior | Walks and throws fireballs in arc. Fireballs bounce 3 times. Immune to fire damage. |
| Kill method | Stomp, star, shell. Fireball passes through. |
| Score | 1000 |
| Sound | Fire whoosh, flame crackle, flash |

#### Dry Bones
| Property | Value |
|---|---|
| Size | 48 x 60 px |
| Health | 1 hit (stomp → crumbles → revives after 3s) |
| Speed | 35 px/s |
| Behavior | Walks slowly. When stomped, crumbles into bone pile. Revives after 3s unless stomped again while reviving. |
| Kill method | Stomp while reviving, fireball, star, shell. Ground pound. |
| Score | 400 (first stomp), 400 (kill on revive) |
| Sound | Bone rattle, clatter (crumble), crackle (revive) |

---

### World 8 — Sky / Cloud

#### Banzai Bill
| Property | Value |
|---|---|
| Size | 128 x 80 px |
| Health | Immune to all attacks |
| Speed | 200 px/s (launch), accelerates |
| Behavior | Giant bullet bill. Appears on screen edge, locks on Mario's position, then fires in straight line. Cannot be killed. |
| Kill method | Evade only. Duck or jump over. |
| Score | 0 |
| Sound | Distant boom → rising whistle → roar, explosion on impact |

#### Swooper
| Property | Value |
|---|---|
| Size | 40 x 56 px |
| Health | 1 hit (fireball, star, shell) |
| Speed | 100 px/s |
| Behavior | Hangs from ceiling. Drops and dives when Mario passes below. Follows Mario for 2s then returns to ceiling. |
| Kill method | Fireball, star, shell. Stomp possible but tricky. |
| Score | 300 |
| Sound | Bat screech, wing flutter |

#### Lakitu (Sky)
| Property | Value |
|---|---|
| Size | 48 x 64 px |
| Health | 1 hit (fireball, star, shell) |
| Speed | Follows Mario horizontally |
| Behavior | Same as Lakitu but drops Spinies twice as fast. Appears in cloud-themed levels. |
| Kill method | Fireball, star, shell throw. |
| Score | 800 |
| Sound | Laugh, Spiny drop |

---

### World 9 — Ghost / Haunted

#### Boo
| Property | Value |
|---|---|
| Size | 52 x 52 px |
| Health | 1 hit (star, fireball) — immune to stomp |
| Speed | 60 px/s |
| Behavior | Classic Boo. Faces away when Mario looks at it. Chases when Mario turns away. Moves through walls. |
| Kill method | Star, fireball. Stomp does nothing (phases through). |
| Score | 800 |
| Sound | Spooky whoosh, giggle, fade |

#### Peepa
| Property | Value |
|---|---|
| Size | 40 x 40 px |
| Health | 1 hit (star) — immune to fireball and stomp |
| Speed | 40 px/s |
| Behavior | Floats in fixed circular or figure-8 pattern. Phases through everything. Cannot be touched while glowing (attacking). |
| Kill method | Star only. Fireball and stomp pass through. |
| Score | 600 |
| Sound | Ghostly hum, shimmer |

#### Stretch
| Property | Value |
|---|---|
| Size | 48 x 128 px (fully extended) |
| Health | 1 hit (fireball, star, stomp on head) |
| Speed | N/A |
| Behavior | Hides in block or pipe. Stretches upward when Mario approaches. Retracts when Mario moves away. Can only be hit on the head. |
| Kill method | Stomp head when extended, fireball, star. |
| Score | 1000 |
| Sound | Creaking stretch, slither |

---

### World 10 — Final Castle

#### Hammer Bro
| Property | Value |
|---|---|
| Size | 52 x 64 px |
| Health | 1 hit (stomp, fireball, star, shell) |
| Speed | 30 px/s |
| Behavior | Walks slowly. Throws hammers in high arc. Hammers bounce twice. High accuracy. |
| Kill method | Stomp, fireball, star, shell. Harder than standard Bro because of hammer spread. |
| Score | 2000 |
| Sound | Hammer swing whoosh, metal clang (miss), thud (hit) |

#### Magikoopa / Kamek
| Property | Value |
|---|---|
| Size | 52 x 72 px |
| Health | 2 hits (fireball, star, shell) — teleports after first hit |
| Speed | N/A (teleports) |
| Behavior | Teleports around screen. Casts spells: turns blocks into enemies, shoots magic bolts, creates barriers. Spells are random. |
| Kill method | 2 hits with fireball, star, or shell. Stomp is difficult due to teleport. |
| Score | 3000 |
| Sound | Magic zap, teleport poof, evil laugh |

#### Bowser (Final Boss)
| Property | Value |
|---|---|
| Size | 256 x 200 px |
| Health | 3 hits (fireball, axe, star) |
| Speed | 80 px/s (walk), 150 px/s (charge) |
| Behavior | **Phase 1**: Walks back and forth, breathes fire in 3-shot bursts. **Phase 2**: Charges at Mario, jumps creating shockwave. **Phase 3**: Enraged — fires 5-shot fire bursts, charges faster, jumps repeatedly. Defeated by grabbing axe behind him to destroy bridge. |
| Kill method | Hit 3 times with fireballs to stun → grab axe → bridge collapses → Bowser falls. |
| Score | 10000 |
| Sound | Roar, heavy footsteps, fire breath, chain rattle (axe), crash (bridge) |

---

## Enemy Spawning & Limits

| Property | Value |
|---|---|
| Max enemies on screen | 20 |
| Max projectile enemies | 10 projectiles |
| Respawn | Enemies respawn when scrolling off-screen and coming back |
| Despawn | Enemies despawn when > 2 screens away from Mario |
| Spawn trigger | Enemy spawns when within 1 screen of Mario (prevent pop-in) |

---

## Enemy Stats Reference

| Enemy | HP | Speed | Stomp? | Fire? | Star? | Score |
|---|---|---|---|---|---|---|
| Goomba | 1 | 40 | ✅ | ✅ | ✅ | 100 |
| Koopa Troopa | 1 | 50 | ✅ (shell) | ✅ | ✅ | 200 |
| Piranha Plant | 1 | 0 | ✅ (emerged) | ✅ | ✅ | 400 |
| Paratroopa | 1 | 60 | ✅ (→Koopa) | ✅ | ✅ | 400 |
| Buzzy Beetle | 1 | 35 | ❌ (→shell) | ✅ | ✅ | 200 |
| Spiny | 1 | 30 | ❌ | ✅ | ✅ | 400 |
| Lakitu | 1 | follow | ❌ | ✅ | ✅ | 800 |
| Pokey | 4 | 20 | ✅ (head) | ✅ | ✅ | 800 |
| Cheep Cheep | 1 | 80 | ✅ (above) | ✅ | ✅ | 200 |
| Blooper | 1 | 50 | ✅ (risky) | ✅ | ✅ | 400 |
| Monty Mole | 1 | 50 | ✅ | ✅ | ✅ | 400 |
| Wiggler | 5 | 40→120 | ✅ | ✅ | ✅ | 2000 |
| Freezeball | 1 | 70 | ❌ | ✅ | ✅ | 400 |
| Dry Bones | 1 (revives) | 35 | ✅ (timing) | ✅ | ✅ | 400 |
| Fire Bro | 1 | 40 | ✅ | ❌ | ✅ | 1000 |
| Boo | 1 | 60 | ❌ | ✅ | ✅ | 800 |
| Hammer Bro | 1 | 30 | ✅ | ✅ | ✅ | 2000 |
| Bowser | 3 | 80→150 | ❌ | ✅ | ✅ | 10000 |

---

## Miscellaneous Enemy Mechanics

- **Stun**: Fireballs stun most enemies for 0.5s before killing
- **Chain reaction**: A kicked shell can kill unlimited enemies in one path
- **Enemy stacking**: Some levels stack enemies on top of each other (e.g. Paragoomba on Koopa)
- **Micro-goombas**: Some levels have mini Goombas (24 px) — same stats, smaller hitbox
- **Color variants**: Different colors = different behavior (e.g. Red Koopa doesn't fall off edges)
- **Shell color coding**: Green = walks off edges, Red = patrols platform edges carefully
- **Frozen enemies**: Ice Bro can freeze other enemies, making them solid platforms
