# Graphify — Knowledge Graph Workflow

## Overview
This project uses [graphify](https://github.com/safishamsi/graphify) to maintain a queryable knowledge graph of all plans and code. The graph replaces raw file grepping for architecture and design questions.

---

## How It Works

graphify extracts:
- **Code files**: AST-based extraction (local, no API calls) via tree-sitter
- **Docs/plans**: Semantic extraction via LLM (inside OpenCode session)
- **Images/PDFs**: Semantic extraction via LLM

Output goes to `graphify-out/`:
| File | Purpose |
|---|---|
| `graph.html` | Interactive graph visualization (open in browser) |
| `GRAPH_REPORT.md` | God nodes, community structure, suggested questions |
| `graph.json` | Full graph data (query programmatically) |

---

## Workflow

### First-time setup

```powershell
graphify install --project --platform opencode
```

This installs:
- Skill file to `~/.config/opencode/skills/graphify/SKILL.md`
- `tool.execute.before` hook at `.opencode/plugins/graphify.js`
- Plugin registration in `opencode.json`

### Build the graph

Inside OpenCode, type:

```
/graphify .
```

This extracts all files, runs community detection, and generates the report + HTML visualization.

### Update after changes

After modifying code or plan files:

```
/graphify . --update
```

Or from CLI (AST-only, no API cost):

```powershell
graphify update .
```

### Query the graph

```
/graphify query "what blocks give fire flowers?"
/graphify path "Mario" "Bowser"
/graphify explain "PhysicsSystem"
```

### Auto-rebuild on commit

```powershell
graphify hook install
```

Installs a post-commit git hook that runs `graphify update .` automatically after every commit.

---

## AGENTS.md Instructions

The `AGENTS.md` file at the project root tells the AI assistant to:

1. Read `graphify-out/GRAPH_REPORT.md` before answering architecture questions
2. Navigate `graphify-out/wiki/index.md` instead of reading raw files when it exists
3. Run `graphify update .` after modifying code files (AST-only, no API cost)

---

## .graphifyignore

Patterns in `.graphifyignore` are excluded from the graph (same syntax as `.gitignore`):

```
*.git
node_modules/
dist/
build/
*.generated.*
```

---

## graphify-out in Git

`graphify-out/` **should be committed** to the repository so all team members start with the same graph.

Recommended `.gitignore` additions:

```
graphify-out/manifest.json
graphify-out/cost.json
# graphify-out/cache/  (optional)
```

---

## Reference

| Command | Description |
|---|---|
| `graphify install --project --platform opencode` | Install graphify for this project |
| `/graphify .` | Build full graph (inside OpenCode) |
| `/graphify . --update` | Re-extract changed files only |
| `graphify update .` | CLI: re-extract code files (AST only) |
| `graphify query "..."` | Query the graph from CLI |
| `graphify hook install` | Install git post-commit hook |
| `graphify uninstall` | Remove graphify from all platforms |

Source: https://github.com/safishamsi/graphify
