# Master Plan — Super Mario Game

## Concept
A very high quality 2D side-scrolling platformer inspired by classic Mario games. The player controls Mario as he runs, jumps, and stomps enemies across multiple themed worlds to liberate his love from prison.

## Core Features
- **Player**: Mario with run, jump, crouch, and power-up mechanics (Super, 1-Up, Poison, Mega, Mini, Propeller, Penguin, Bee, Boo, Gold mushrooms + Fire Flower + Starman).
- **Enemies**: At least 20 unique enemy types (Goombas, Koopa Troopas, Piranha Plants, etc.).
- **Bosses**: At least 10 unique boss fights, one per world.
- **Levels**: 10 worlds, 10 levels each (100 total), with a castle boss at the end of each world.
- **Power-Ups**: 10 mushroom types (Super, 1-Up, Poison, Mega, Mini, Propeller, Penguin, Bee, Boo, Gold) + Fire Flower + Starman.
- **Scoring**: Points for enemies, coins, and time bonuses. 1-Up at 100 coins.
- **Blocks**: Question blocks (?), brick blocks, hidden blocks. Coins, power-ups, and vines inside.

## Technical Stack
- **Engine**: Custom C# engine targetting OpenGL via Silk.NET. Full-screen display with resolution/window mode options.
- **Art**: High-quality modern 2D art (hand-drawn / painted style), high-resolution sprites and backgrounds.
- **Audio**: High-quality real music (MP3/WAV) with original soundtrack and SFX. Streamed playback, not chiptune.

## Milestones
1. **Prototype** — Player movement, basic physics, one test level.
2. **Core Systems** — Enemies, blocks, power-ups, HUD.
3. **Content** — All 100 levels, 10 worlds, boss fights.
4. **Polish** — Audio, UI menus, save/load, game over flow.
5. **Ship** — Build, test, release.

## Design Pillars
- Tight, responsive controls.
- Fair but challenging difficulty curve.
- Rewarding exploration (secrets, shortcuts, bonus areas).
