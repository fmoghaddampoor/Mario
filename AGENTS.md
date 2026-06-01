## graphify

This project uses [graphify](https://github.com/safishamsi/graphify) to build a knowledge graph from all plans and code.

- Run `/graphify .` inside OpenCode to build/update the graph
- Read `graphify-out/GRAPH_REPORT.md` before answering architecture questions
- After modifying code files, run `graphify update .` to keep the AST graph current (no API cost)
- See `plans/graphify.md` for full workflow details

## tasks

The `tasks/` directory contains 1064 implementation tasks across 17 categories (core-engine, graphics, audio, physics, input, scene-entity, player, enemies, bosses, powerups, blocks-items, levels, ui-menus, save-load, editor, build-ci, polish). Each task is a numbered markdown file with description, steps, and acceptance criteria. Start from `tasks/00-core-engine/001-create-solution-structure.md` and work through sequentially.

## csharpStandards

Read `plans/csharpStandards.md` before writing any C# code. All code must follow those naming, style, logging, DI, performance, and error-handling rules.
