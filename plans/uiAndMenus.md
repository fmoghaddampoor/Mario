# UI & Menus — Full Specification

## Overview
All user interface screens, HUD elements, menu flows, and interaction patterns. Custom-drawn UI (no system fonts), consistent with the game's high-quality modern art style.

---

## Design Principles
- Minimal — show only what the player needs
- Diegetic where possible (elements exist in game world)
- Semi-transparent backgrounds for readability
- Consistent button layout: Confirm on right, Cancel on left
- Controller navigation: D-pad/thumbstick moves selection, A confirms, B cancels

---

## HUD (In-Game)

### Layout
```
┌──────────────────────────────────────────────────────┐
│ [Coin Icon]  x 24    [Score]  012345    [Time]  256  │
│                                                        │
│                                                        │
│                                                        │
│                                                        │
│                                                        │
│                          [Power-Up Icon]               │
│ [Lives Icon]  x 3                                      │
└──────────────────────────────────────────────────────┘
```

### Elements
| Element | Position | Description |
|---|---|---|
| Coin counter | Top-left | Coin icon + number (max 99, wraps) |
| Score | Top-center | 6-digit number |
| Timer | Top-right | Countdown in seconds (if timed level) |
| Lives | Bottom-left | 1-UP icon + number |
| Power-up indicator | Bottom-right | Current power-up icon (empty if small) |
| Star Coin indicator | Top-right below timer | 3 dots, filled = collected |

### States
| State | HUD Visible |
|---|---|
| Normal gameplay | Yes |
| Paused | Yes (dimmed) |
| Menu screens | No |
| Cutscene | No (separate UI) |
| Death animation | No (fades out) |
| Level complete | No (celebration overlay) |

### HUD Animations
- Coin counter: number flips up on collect
- Score: numbers roll on increment
- Timer: flashes red when < 30s
- Lives: bounces on +1, shakes on death
- Power-up: icon slides in on collect, slides out on loss
- Star Coin: dot fills with sparkle

---

## Title Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                    [Full-bleed art]                     │
│                    Parallax background                  │
│                                                        │
│                                                        │
│                    SUPER MARIO GAME                     │
│                  (animated logo text)                   │
│                                                        │
│                    ┌─────────────┐                     │
│                    │  NEW GAME   │                     │
│                    ├─────────────┤                     │
│                    │  LOAD GAME  │                     │
│                    ├─────────────┤                     │
│                    │  OPTIONS    │                     │
│                    ├─────────────┤                     │
│                    │  CREDITS    │                     │
│                    ├─────────────┤                     │
│                    │   QUIT      │                     │
│                    └─────────────┘                     │
│                                                        │
│              "Press any key" (if idle)                  │
│                                                        │
└──────────────────────────────────────────────────────┘
```

### Behavior
- Background: painted illustration of World 1 with gentle parallax
- Logo: animated text with glow pulse
- Menu: buttons highlight on hover/selection, click/confirm to select
- "Press any key" appears after 5s of idle
- Transition: fade to black on selection (0.5s)

---

## World Map Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                WORLD 1 — GRASSLAND                     │
│                                                        │
│      [1-1] ★ --- [1-2] --- [1-3]                       │
│        |                    |                           │
│        |              [1-4] |                           │
│        |                    |                           │
│      [1-5] --- [1-6] --- [1-7] --- [1-8]               │
│        |                                |               │
│      [1-9] (secret)                 [1-10] 🏰          │
│                                                        │
│    [← Previous World]    [Next World →]                │
│                                                        │
│    Star Coins: 12 / 30  |  Lives: 3                    │
└──────────────────────────────────────────────────────┘
```

### Elements
- Path connects levels (solid line = unlocked, dotted = locked)
- Completed levels: checkmark overlay
- Current level: pulsing highlight
- Secret level: hidden until discovered, then shown with star icon
- Castle level: castle icon
- Star Coin progress per world displayed at bottom
- Previous/Next world arrows at bottom

### Navigation
- D-pad/thumbstick moves cursor along path
- Confirm on current level: enter level
- Can only select completed or current level
- Secret levels need a secret exit discovered first

---

## Level Complete / Score Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                    LEVEL CLEAR!                         │
│                    (animated text)                      │
│                                                        │
│                                                        │
│     Score:  012345                                      │
│     Time Bonus:  +2560                                 │
│     Coins:     +2000                                   │
│     Enemies:    +1200                                  │
│     Star Coins:  +3000                                 │
│     ─────────────────────                               │
│     Total:     021105                                  │
│                                                        │
│                                                        │
│     Star Coins:  ★ ★ ☆                                 │
│                                                        │
│              [CONTINUE →]                               │
└──────────────────────────────────────────────────────┘
```

- Numbers roll up with sound effects
- Star Coin display shows collected/filled
- "CONTINUE" returns to world map
- If castle level: "WORLD CLEAR!" with fireworks transition

---

## Pause Menu

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                     ⏸ PAUSED                          │
│                                                        │
│             ┌─────────────────────┐                    │
│             │     CONTINUE        │                    │
│             │     RETRY LEVEL     │                    │
│             │     OPTIONS         │                    │
│             │     QUIT TO MAP     │                    │
│             │     QUIT TO TITLE   │                    │
│             └─────────────────────┘                    │
│                                                        │
│           [Background: blurred game frame]              │
└──────────────────────────────────────────────────────┘
```

- Game frame is frozen and blurred behind menu
- "CONTINUE" also triggered by pressing Pause again

---

## Options / Settings Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                     OPTIONS                             │
│                                                        │
│     ┌─────────────────────────────────┐                │
│     │ MUSIC VOLUME      ████████░░ 70% │               │
│     │ SFX VOLUME        ██████░░░░ 60% │               │
│     │                    │               │               │
│     │ FULLSCREEN        [ON]  ●○       │               │
│     │ RESOLUTION        [1920x1080 ▼]  │               │
│     │ VSYNC             [ON]  ●○       │               │
│     │                    │               │               │
│     │ CONTROLLER VIBE   [ON]  ●○       │               │
│     │                    │               │               │
│     │ LANGUAGE          [ENGLISH ▼]    │               │
│     └─────────────────────────────────┘                │
│                                                        │
│              [BACK]   [DEFAULTS]                       │
└──────────────────────────────────────────────────────┘
```

### Controls
- Sliders: left/right to adjust, click/drag for mouse
- Toggles: click to toggle, or left/right to change
- Dropdown: select to open list, up/down to navigate
- "BACK" returns to previous menu
- "DEFAULTS" resets all to default

### Audio Preview
- Adjusting music volume plays a short music loop
- Adjusting SFX volume plays a coin collect sound
- Preview is non-intrusive (quiet, short)

### Settings Persistence
- All settings auto-saved to `settings.json` on change
- No "apply" button needed

---

## Game Over Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                                                        │
│                    GAME OVER                            │
│                                                        │
│              Final Score: 012345                        │
│                                                        │
│              ┌──────────────┐                          │
│              │   CONTINUE   │  (3 continues left)      │
│              ├──────────────┤                          │
│              │   QUIT       │                          │
│              └──────────────┘                          │
│                                                        │
│                                                        │
└──────────────────────────────────────────────────────┘
```

- CONTINUE: restart from current world's first level, lives reset to 3
- Each continue consumes 1 continue token (start with 3)
- 0 continues → forced quit to title screen
- Extra continues can be earned (rare reward)

---

## Save / Load Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                    LOAD / SAVE                          │
│                                                        │
│     ┌─────────────────────────────────┐                │
│     │ SLOT 1:  World 5   ★★☆  3 Lives │   [LOAD] [DEL]│
│     ├─────────────────────────────────┤                │
│     │ SLOT 2:  World 2   ★☆☆  7 Lives │   [LOAD] [DEL]│
│     ├─────────────────────────────────┤                │
│     │ SLOT 3:  [EMPTY]                │   [SAVE]      │
│     └─────────────────────────────────┘                │
│                                                        │
│              [BACK]                                    │
└──────────────────────────────────────────────────────┘
```

### Save Data (per slot)
- Current world + level
- Lives remaining
- Star Coins collected (total)
- Star Coins per level
- Completed levels list
- Unlocked worlds
- Settings (volume, etc.)
- Play time
- Timestamp

### Auto-Save
- Game auto-saves on: level complete, checkpoint touch, boss defeat, world clear
- Auto-save goes to the slot that was last loaded/manually saved

---

## Credits Screen

### Layout
```
┌──────────────────────────────────────────────────────┐
│                                                        │
│                    THANK YOU FOR PLAYING!               │
│                                                        │
│     ┌─────────────────────────────────┐                │
│     │                                  │                │
│     │   [Scrolling credits text]       │                │
│     │                                  │                │
│     │   Design & Programming           │                │
│     │   [Name]                        │                │
│     │                                  │                │
│     │   Art                            │                │
│     │   [Name]                        │                │
│     │                                  │                │
│     │   Music & Sound                  │                │
│     │   [Name]                        │                │
│     │                                  │                │
│     │   Special Thanks                 │                │
│     │   ...                            │                │
│     │                                  │                │
│     └─────────────────────────────────┘                │
│                                                        │
│              [SKIP]                                     │
└──────────────────────────────────────────────────────┘
```

- Auto-scrolls upward (slow), press any key to speed up
- Background: gameplay montage (level clear, boss fights, secrets)
- Skip returns to title screen

---

## Notification System

### Toast Notifications
```
┌─────────────────────────────┐
│  ★ Star Coin Collected!     │  (top-center, 2s, fades)
└─────────────────────────────┘

┌─────────────────────────────┐
│  1-UP!                      │  (center, large, 1.5s)
└─────────────────────────────┘

┌─────────────────────────────┐
│  New World Unlocked!        │  (center, 2s, with fanfare)
└─────────────────────────────┘
```

- Non-intrusive, does not pause gameplay
- Queue system: if multiple notifications, they stack with offset
- Can be disabled in settings

---

## UI Font & Styling

### Font
- Custom-drawn bitmap font (not a system font)
- Character set: A-Z, a-z, 0-9, punctuation, special symbols (★, ♥, ☠, etc.)
- Sizes: 16px (HUD), 24px (menus), 48px (titles)
- Color: white with black outline for readability
- Style: rounded, friendly, consistent with art style

### Button Styling
```
[Normal]   ┌─────────────┐  White border, dark fill
           │  CONTINUE   │
           └─────────────┘

[Hover]    ┌─────────────┐  Gold border, brighter fill
           │  CONTINUE   │
           └─────────────┘

[Pressed]  ┌─────────────┐  Darker fill, shifted 1px down
           │  CONTINUE   │
           └─────────────┘

[Disabled] ┌─────────────┐  Grey border, dimmed text
           │  LOCKED     │
           └─────────────┘
```

### Transitions
- Screen fade: 0.3s (fast)
- Menu slide: 0.2s (buttons slide up into position)
- Pause blur: 0.15s
- Death to game over: 1s fade + 0.5s wait

---

## UI Technical Specs

| Property | Value |
|---|---|
| Render resolution | 1920 x 1080 |
| UI canvas | Same as game (orthographic overlay) |
| Safe zone | 100px margin on all sides |
| Button min size | 200 x 48 px |
| Touch target min | 48 x 48 px |
| Animation | UI elements animate in/out with easing (cubic bezier) |
| Sound | Each UI interaction has a subtle SFX |
| Controller | Full controller navigation (no mouse required) |
