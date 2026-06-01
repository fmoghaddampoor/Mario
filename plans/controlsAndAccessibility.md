# Controls & Accessibility — Full Specification

## Overview
Complete input system with full rebinding, controller support, and accessibility features.

---

## Default Key Bindings

### Keyboard

| Action | Primary | Secondary |
|---|---|---|
| Move Left | A | Left Arrow |
| Move Right | D | Right Arrow |
| Jump | Space | Up Arrow |
| Run (hold) | Left Shift | Z |
| Crouch / Slide | S | Down Arrow |
| Fire / Action | J | X |
| Pause | Escape | P |
| Interact / Grab | F | Enter |
| Ground Pound | Down + Jump (in air) | |
| Quick Restart | R | |

### Controller (Xbox Layout)

| Action | Button |
|---|---|
| Move | Left Stick / D-Pad |
| Jump | A |
| Run | Right Trigger |
| Crouch / Slide | B |
| Fire / Action | X |
| Pause | Start |
| Interact / Grab | Y |
| Ground Pound | Down + A (in air) |
| Quick Restart | Back + Start (hold 2s) |

### Controller (PlayStation Layout)

| Action | Button |
|---|---|
| Move | Left Stick / D-Pad |
| Jump | Cross |
| Run | R2 |
| Crouch / Slide | Circle |
| Fire / Action | Square |
| Pause | Options |
| Interact / Grab | Triangle |
| Ground Pound | Down + Cross (in air) |

### Controller (Switch Layout)

| Action | Button |
|---|---|
| Move | Left Stick / D-Pad |
| Jump | B |
| Run | R |
| Crouch / Slide | A |
| Fire / Action | Y |
| Pause | + |
| Interact / Grab | X |
| Ground Pound | Down + B (in air) |

---

## Rebind System

### Rebindable Actions

| Category | Actions |
|---|---|
| Movement | Move Left, Move Right, Jump, Run, Crouch |
| Combat | Fire/Action, Ground Pound |
| Interaction | Interact/Grab, Quick Restart |
| Menu | Pause, Confirm, Cancel, Navigate Up/Down/Left/Right |

### Rebinding UI

```
┌───────────────────────────────────────────────────┐
│                  KEY BINDINGS                       │
│                                                     │
│  MOVEMENT                                           │
│     Move Left ............... [A        ]          │
│     Move Right .............. [D        ]          │
│     Jump .................... [Space    ]          │
│     Run ...................... [Left Shift]          │
│     Crouch ................... [S        ]          │
│                                                     │
│  COMBAT                                             │
│     Fire / Action ........... [J        ]          │
│     Ground Pound ............ [Down+Jump]          │
│                                                     │
│                    [RESET TO DEFAULT]               │
└───────────────────────────────────────────────────┘
```

- Click/tap a binding → press the new key/button → binding updates
- Conflict detection: warns if key is already bound to another action
- Controller and keyboard bindings are separate profiles
- Gamepad detected → show controller layout by default

### Binding Rules
- Movement actions must be bound to directional inputs (WASD, arrows, stick)
- Cannot bind the same key to two actions in the same input mode
- Pause/Escape cannot be unbound
- Jump must always have a binding

---

## Controller Support

### Supported Controllers
| Controller | Platform |
|---|---|
| Xbox One/Series X|S | Windows |
| PlayStation 4/5 | Windows (via Steam) |
| Nintendo Switch Pro | Windows |
| Generic XInput | Windows |
| Keyboard + Mouse | All |

### Vibration
- Enabled by default (toggle in settings)
- Vibration events:
  - Landing: light rumble (0.2s)
  - Stomping enemy: medium rumble (0.3s)
  - Taking damage: heavy rumble (0.5s)
  - Death: long heavy rumble (1s)
  - Boss hit: medium rumble (0.3s)
  - Mega Mushroom footsteps: constant light rumble
  - Ground pound: heavy rumble (0.4s)

### Analog Stick
- Movement threshold: 15% dead zone
- Walk: 15-50% stick tilt
- Run: 50-100% stick tilt (no need to hold Run button)
- Run button overrides: if held, always run regardless of tilt
- Dead zone adjustable in settings (5-30%)

---

## Accessibility Features

### Visual Accessibility

| Feature | Description | Default |
|---|---|---|
| Screen shake toggle | Disables all camera shake | On |
| Flashing effects toggle | Disables flashing/strobing effects | On |
| High contrast mode | Outlines all entities in bold color | Off |
| Colorblind mode | Filters/shifts palettes (Protanopia, Deuteranopia, Tritanopia) | Off |
| Large UI mode | 1.5x UI scale | Off |
| Enemy indicator | Arrow shows nearest enemy direction (off-screen) | Off |
| HUD opacity | Adjustable 0-100% | 80% |
| Star Coin indicator | Always show remaining Star Coin locations on screen | Off |

### Auditory Accessibility

| Feature | Description | Default |
|---|---|---|
| Visual cues for SFX | Subtle screen flash/icon for key sounds (coin, stomp, damage) | Off |
| Closed captions | Text overlay for all SFX descriptions | Off |
| Music volume | Independent slider | 70% |
| SFX volume | Independent slider | 60% |
| Mono audio | Force mono output | Off |

### Motor Accessibility

| Feature | Description | Default |
|---|---|---|
| Auto-run | Always run (no need to hold Run button) | Off |
| Hold to jump | Hold jump for variable height (default behavior) | On |
| Toggle run | Press Run to toggle, rather than hold | Off |
| Controller dead zone | Adjustable 5-30% | 15% |
| Button hold time | Adjust how long a button must be held (100-500ms) | 100ms |
| Quick restart confirm | Confirmation dialog before restart (prevents misclicks) | Off |
| Input assist | Auto-bumper — if Mario is about to fall, does a small corrective jump | Off |

### Input Assist (Auto-Bumper)
- Detects when Mario is about to walk off a ledge without jumping
- Performs a small corrective hop to prevent the fall
- Does NOT affect gameplay in any other way
- Disabled by default, enable in settings
- Only triggers on visible ground edges (not on hidden pits or intentional drops)

### Cognitive Accessibility

| Feature | Description | Default |
|---|---|---|
| Level timer toggle | Disable countdown timer on timed levels | Off |
| Invincibility mode | Mario takes no damage, cannot die | Off |
| Infinite lives | Lives never decrease | Off |
| Checkpoint frequency | Normal / Extra (every sub-section) | Normal |
| Enemy speed reduction | Enemies move 50% speed | Off |
| Puzzle skip | Ghost house puzzles can be auto-solved after 2 attempts | Off |

---

## Difficulty Settings

### Presets

| Setting | Easy | Normal | Hard |
|---|---|---|---|
| Enemy speed | 70% | 100% | 120% |
| Enemy density | 60% | 100% | 130% |
| Damage taken | −1 power-up level | −1 power-up level | Instant death |
| Falling damage | None | Normal | Normal |
| Timer | 400s | 300s | 200s |
| Star Coins | 2 per level | 3 per level | 3 per level |
| Checkpoints | Every section | Normal | No checkpoints |
| Power-ups | Abundant | Normal | Scarce |
| Lives start | 10 | 3 | 1 |

- Difficulty can be changed at any time from the pause menu
- No achievement/score penalty for changing difficulty
- Hard mode: bonus score multiplier (1.5x)

---

## Control Feel Customization

### Advanced Settings (Expandable Section)

| Setting | Range | Default |
|---|---|---|
| Horizontal sensitivity | 0.5x — 2.0x | 1.0x |
| Vertical sensitivity | 0.5x — 2.0x | 1.0x |
| Dead zone | 5% — 30% | 15% |
| Vibration intensity | 0% — 100% | 100% |
| Trigger dead zone | 5% — 30% | 10% |
| Invert camera | On/Off | Off |
| Swap A/B confirm | On/Off | Off |

---

## Context-Sensitive Input

### Near interactable objects
- When Mario stands near a shell, ? block, or switch
- "Interact" prompt appears on HUD
- Press Interact to: pick up shell, hit switch, enter pipe

### During cutscenes
- Skip: Press Confirm or Space
- Auto-advance: Toggle in settings

### Menus
- Confirm: Enter / A / Cross
- Cancel: Escape / B / Circle
- Navigate: Arrow keys / WASD / D-pad / Left stick
- Tab forward: Tab / Right bumper
- Tab backward: Shift+Tab / Left bumper
